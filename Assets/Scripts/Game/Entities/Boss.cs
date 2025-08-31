using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Levels;
using BGJ_2025_2.Game.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BGJ_2025_2.Game.Entities
{
    [AddComponentMenu("BGJ 2025.2/Game/Entities/Boss")]
    public class Boss : Human, IDescriptable
    {
        // Fields
        private const string _PlayerTag = "Player";
        private const string _RoomTag = "Room";
        private const string _AnimatorStateName = "IsWandering";
        private const float _TargetDistanceMargin = 0.1f;
        private const float _StuckVelocityMargin = 0.1f;
        private const float _StuckDuration = 0.8f;
        private const float _DefaultFieldOfView = 90f;
        private const float _DefaultWalkingSpeed = 4.5f;
        private const float _DefaultRunningSpeed = 7f;
        private const float _DefaultDominanceAssertionDuration = 1.5f;
        private const float _DefaultDominanceAssertionSpeed = 100f;
        private const float _DefaultNervousness = 0.05f;
        private const int _DefaultRoomMemorySize = 4;
        private const float _DefaultMinWanderDistance = 2.25f;

        [SerializeField] private Office _office;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _frontSpriteRenderer;
        [SerializeField] private SpriteRenderer _backSpriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _rayOrigin;
        [SerializeField] private SabotageDetector _sabotageDetector;
        [SerializeField] private float _fieldOfView = _DefaultFieldOfView;
        [SerializeField] private float _walkingSpeed = _DefaultWalkingSpeed;
        [SerializeField] private float _runningSpeed = _DefaultRunningSpeed;
        [SerializeField] private float _dominanceAssertionDuration = _DefaultDominanceAssertionDuration;
        [SerializeField] private float _dominanceAssertionSpeed = _DefaultDominanceAssertionSpeed;
        [SerializeField] private Color _chasingSpriteTint = Color.white;
        [SerializeField] private float _nervousness = _DefaultNervousness;
        [SerializeField] private int _roomMemorySize = _DefaultRoomMemorySize;
        [SerializeField] private float _minWanderDistance = _DefaultMinWanderDistance;
        RaycastHit _raycastHit;
        private bool _isWandering;
        private bool _isChasing;
        private float _alertness;
        private float _elapsedStuckDuration;
        private Queue<Room> _previousRooms;
        private Room _currentRoom;
        private int _currentRoomWanderCount;
        Vector3 _dominaceAssertionDirection;
        private bool _isRunning;
        private bool _isPaused;
#if UNITY_EDITOR
        [Header("Development options")]
        [SerializeField] private bool _stayStill;
#endif


        // Properties
        public bool IsMoving => _agent.velocity != Vector3.zero;
        public bool IsIdle => !IsMoving;
        public bool IsWandering => _isWandering;
        public bool IsChasing => _isChasing;
        public float Alertness => _alertness;
        public float ElapsedStuckDuration => _elapsedStuckDuration;
        public Queue<Room> PreviousRooms => _previousRooms;
        public Room CurrentRoom => _currentRoom;
        public int CurrentRoomWonderCount => _currentRoomWanderCount;
        public string Name => "Gilbert Johnson";
        public string Description => "(Your boss)";


        // Methods
        private void Awake()
        {
            _dominaceAssertionDirection = Vector3.up * _dominanceAssertionSpeed;
            _previousRooms = new(_roomMemorySize);
        }

        private void Update()
        {
            if (_isChasing)
            {
                _agent.SetDestination(_office.Player.transform.position);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_PlayerTag))
            {
                if (_isChasing && !(_office.Game.Tasks.FinishedTaskCount == TaskHandler.MinTaskCount && _office.CookieJar.IsEmpty))
                {
                    _office.Player.Fire();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_RoomTag) && !_sabotageDetector.IsTriggered)
            {
                Room enteredRoom = other.gameObject.GetComponent<RoomZone>().Room;
                if (enteredRoom != null && (enteredRoom != _currentRoom || _currentRoom == null))
                {
                    EnterRoom(enteredRoom);
                }
            }
        }

        public void EnterRoom(Room room)
        {
            _currentRoom = room;

            if (_previousRooms.Count == _roomMemorySize)
            {
                _previousRooms.Dequeue();
            }
            _previousRooms.Enqueue(room);
            _currentRoomWanderCount = 0;
#if UNITY_EDITOR
            Log($"Entered room: {_currentRoom.Name}");
#endif
        }

        public void Warp(Vector3 position)
        {
            _agent.Warp(position);
        }

        public void WanderToPosition(Vector3 position, Action actionAfter = null)
        {
            StartCoroutine(WanderToPositionCoroutine(position, actionAfter));
        }

        private IEnumerator WanderToPositionCoroutine(Vector3 position, Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log($"Wandering to position: {position}");
#endif
            _agent.isStopped = false;
            _agent.SetDestination(position);
            _isWandering = true;
            _animator.SetBool(_AnimatorStateName, true);

            _elapsedStuckDuration = 0f;
            while (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), position) > _TargetDistanceMargin)
            {
                if (_agent.velocity.magnitude <= _StuckVelocityMargin)
                {
                    _elapsedStuckDuration += Time.deltaTime;

                    // A fõnök beragadt valahol útközben (túl sok volt a sütibõl, I guess)
                    if (_elapsedStuckDuration >= _StuckDuration)
                    {
#if UNITY_EDITOR
                        Log($"Got stuck :,( (distance: {Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), position)})");
#endif
                        AssertDominance(() =>
                        {
                            if (actionAfter == null)
                            {
                                DecideNextAction();
                            }
                            else
                            {
                                actionAfter.Invoke();
                            }
                        });

                        yield break;
                    }

                    _animator.SetBool(_AnimatorStateName, false);
                }
                else if (_elapsedStuckDuration != 0f)
                {
                    _elapsedStuckDuration = 0f;

                    _animator.SetBool(_AnimatorStateName, true);
                }

                yield return null;
            }

#if UNITY_EDITOR
            Log($"Wandered to position: {position}");
#endif
            _isWandering = false;
            _animator.SetBool(_AnimatorStateName, false);

            if (actionAfter == null)
            {
                DecideNextAction();
            }
            else
            {
                actionAfter.Invoke();
            }
        }

        public void WanderToRoom(Room room, Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log($"Wandering to room: {room.Name}");
#endif
            WanderToPosition(room.GetRandomPosition(), () =>
            {
                if (room == _office.Kitchen)
                {
                    _alertness = 0f;
#if UNITY_EDITOR
                    Log("I like the kitchen... that's where the cookies are! >:D (alertness reset)");
#endif
                }
                else
                {
                    _alertness += _nervousness + _office.CookieJar.Emptiness / 2.5f;
#if UNITY_EDITOR
                    Log($"Alertness increased: {_alertness}, jar cookie count: {_office.CookieJar.CookieCount}, jar emptiness: {_office.CookieJar.Emptiness}");
#endif
                }
#if UNITY_EDITOR
                Log($"Wandered to room: {room.Name}");
#endif
                if (actionAfter == null)
                {
                    DecideNextAction();
                }
                else
                {
                    actionAfter.Invoke();
                }
            });
        }

        public void WanderInRoom(Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log($"Wandering in room: {_currentRoom.Name}");
#endif
            Vector3 randomPosition;
            do
            {
                randomPosition = _currentRoom.GetRandomPosition();
            } while (Vector3.Distance(randomPosition, new Vector3(transform.position.x, 0f, transform.position.y))
            < _minWanderDistance * _currentRoom.WanderCount);

            WanderToPosition(_currentRoom.GetRandomPosition(), () =>
            {
                ++_currentRoomWanderCount;

                if (actionAfter == null)
                {
                    DecideNextAction();
                }
                else
                {
                    actionAfter.Invoke();
                }
#if UNITY_EDITOR
                Log($"Wandered in room: {_currentRoom.Name}");
#endif
            });
        }

        public void AssertDominance(Action actionAfter = null)
        {
            StartCoroutine(AssertDominanceCoroutine(actionAfter));
        }

        private IEnumerator AssertDominanceCoroutine(Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log($"Asserting dominance! >:D");
#endif
            _animator.SetBool(_AnimatorStateName, false);

            float elapsedTime = 0f;
            while (elapsedTime < _dominanceAssertionDuration)
            {
                elapsedTime += Time.deltaTime;

                transform.Rotate(_dominaceAssertionDirection * Time.deltaTime);

                yield return null;
            }
#if UNITY_EDITOR
            Log($"Asserted dominance! >:D");
#endif
            if (actionAfter == null)
            {
                DecideNextAction();
            }
            else
            {
                actionAfter.Invoke();
            }
        }

        public void StartChasingPlayer()
        {
            if (_isChasing) return;

#if UNITY_EDITOR
            Log("Started chasing player! >:(");
#endif
            _office.Player.Audio.Alert();

            _isWandering = false;
            _isChasing = true;
            _agent.isStopped = false;
            _agent.speed = _runningSpeed;
            _animator.SetBool(_AnimatorStateName, true);

            _frontSpriteRenderer.color = _chasingSpriteTint;
            _backSpriteRenderer.color = _chasingSpriteTint;
        }

        public void StopChasingPlayer()
        {
#if UNITY_EDITOR
            Log("Stopped chasing player! |:)");
#endif
            _isWandering = false;
            _isChasing = false;
            _agent.isStopped = true;
            _agent.speed = _walkingSpeed;
            _animator.SetBool(_AnimatorStateName, false);

            _frontSpriteRenderer.color = Color.white;
            _backSpriteRenderer.color = Color.white;
        }

        public bool IsSeeingPlayer()
        {
            Vector3 ownPosition = transform.position;
            Vector3 playerPosition = _office.Player.transform.position;

            float angle = (Mathf.Atan2(playerPosition.z - ownPosition.z, playerPosition.x - ownPosition.x) * 180f / Mathf.PI
                + transform.eulerAngles.y) % 360;
            float halfFieldOfView = _fieldOfView / 2f;

            if (angle >= 90f - halfFieldOfView && angle <= 90f + halfFieldOfView)
            {
                return Physics.Raycast(_rayOrigin.position, (playerPosition - _rayOrigin.position).normalized,
                    out _raycastHit, float.PositiveInfinity, Physics.AllLayers)
                    && _raycastHit.collider.gameObject.CompareTag(_PlayerTag);
            }

            return false;
        }

        public bool IsKindaSeeingPlayer()
        {
            Vector3 playerPosition = _office.Player.transform.position;
            int layerMask = (1 << 2) | (1 << 6) | (1 << 7);

            return Physics.Raycast(_rayOrigin.position, (playerPosition - _rayOrigin.position).normalized,
                out _raycastHit, float.PositiveInfinity, layerMask, QueryTriggerInteraction.Ignore)
                && _raycastHit.collider.gameObject.CompareTag(_PlayerTag);
        }

        public bool IsInSameRoomAsPlayer()
        {
            return _currentRoom == _office.Player.CurrentRoom;
        }

        public void Reload()
        {
            _isWandering = false;
            _alertness = 0f;
            _elapsedStuckDuration = 0f;
            _animator.SetBool(_AnimatorStateName, false);
            StopChasingPlayer();

#if UNITY_EDITOR
            if (_stayStill)
            {
                Room randomRoom = _office.GetRandomRoom();
                _previousRooms.Clear();
                EnterRoom(_office.Kitchen);
            }
            else
            {
                Room randomRoom = _office.GetRandomRoom();
                _previousRooms.Clear();
                Warp(randomRoom.Center);
                EnterRoom(randomRoom);

                DecideNextAction();
            }
#else
            Room randomRoom = _office.GetRandomRoom();
            _previousRooms.Clear();
            Warp(randomRoom.Center);
            EnterRoom(randomRoom);

            DecideNextAction();
#endif
        }

        public void NotifyFromCookieJar()
        {
            if (IsKindaSeeingPlayer() || IsInSameRoomAsPlayer())
            {
#if UNITY_EDITOR
                Log("Noticed player taking cookie from jar! >:(");
#endif
                StopAllCoroutines();
                StartChasingPlayer();
            }
        }

        public void NotifyFromSabotage()
        {
            if (IsKindaSeeingPlayer() || IsInSameRoomAsPlayer())
            {
#if UNITY_EDITOR
                Log("Noticed player sabotaging the office! >:(");
#endif
                StopAllCoroutines();
                StartChasingPlayer();
            }
        }

        public void NotifyOfPotentialSabotage(Room room)
        {

        }

        public void DecideNextAction()
        {
            if (_isChasing) return;

            // Sütis bödön megvizsgálása adott idõ után
            if (UnityEngine.Random.Range(0f, 1f) <= _alertness)
            {
#if UNITY_EDITOR
                Log($"Alertness at {_alertness}, time to check on the jar");
#endif
                WanderToPosition(_office.CookieJarViewing.position, () =>
                {
                    _alertness = 0f;

                    AssertDominance();
#if UNITY_EDITOR
                    Log($"Checked on the jar");
#endif
                });
            }
            else
            {
                // Vándorlás a szobán belül
                if (_currentRoomWanderCount < _currentRoom.WanderCount)
                {
#if UNITY_EDITOR
                    Log($"Finding a new position in the current room to wander to! :D");
#endif
                    WanderInRoom(() => AssertDominance());
                }
                // Vándorlás másik szobába
                else
                {
#if UNITY_EDITOR
                    Log($"Time to wander to a new room! :D");
#endif
                    Room newRoom;
                    do
                    {
                        newRoom = _office.GetRandomRoom();
                    } while (_previousRooms.Contains(newRoom));

                    WanderToRoom(newRoom, () => AssertDominance());
                }
            }
        }

        public void Play()
        {
            _agent.isStopped = false;

            Reload();
        }

        public void End()
        {
            StopChasingPlayer();
        }

        public void Pause()
        {
            _agent.isStopped = true;
        }

        public void Unpause()
        {
            _agent.isStopped = false;
        }

        private static void Log(string message)
        {
            //Debug.Log($"[BOSS] {message}");
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_rayOrigin.position, (_office.Player.transform.position - _rayOrigin.transform.position),
                Color.red);
        }
    }
}