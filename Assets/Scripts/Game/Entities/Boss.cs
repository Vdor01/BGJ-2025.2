using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Levels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BGJ_2025_2.Game.Entities
{
    [AddComponentMenu("BGJ 2025.2/Game/Entities/Boss")]
    public class Boss : Human, IDescriptable
    {
        // Fields
        private const float _TargetDistanceMargin = 0.1f;
        private const float _DefaultDominanceAssertionDuration = 2f;
        private const float _DefaultDominanceAssertionSpeed = 100f;

        [SerializeField] private Office _office;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _frontSpriteRenderer;
        [SerializeField] private SpriteRenderer _backSpriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _dominanceAssertionDuration = _DefaultDominanceAssertionDuration;
        [SerializeField] private float _dominanceAssertionSpeed = _DefaultDominanceAssertionSpeed;
        private bool _isWandering;
        private bool _isChasing;
        private float _alertness;
        private Room _previousRoom;
        private Room _currentRoom;
        private int _currentRoomWanderCount;
        Vector3 _previousWanderPosition = Vector3.zero;
        Vector3 _dominaceAssertionDirection;


        // Properties
        public bool IsMoving => _agent.velocity != Vector3.zero;
        public bool IsIdle => !IsMoving;
        public bool IsWandering => _isWandering;
        public bool IsChasing => _isChasing;
        public float Alertness => _alertness;
        public Room PreviousRoom => _previousRoom;
        public Room CurrentRoom => _currentRoom;
        public int CurrentRoomWonderCount => _currentRoomWanderCount;
        public string Name => "Gilbert Johnson";
        public string Description => "(Your boss)";


        // Methods
        private void Awake()
        {
            _dominaceAssertionDirection = Vector3.up * _dominanceAssertionSpeed;
        }

        private void Start()
        {
            Reload();
        }

        private void Update()
        {
            if (_isChasing)
            {
                _agent.SetDestination(_office.Player.transform.position);
            }
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

            yield return new WaitUntil(() =>
            Vector3.Distance(transform.position, position) <= _TargetDistanceMargin
            && _agent.velocity.normalized == Vector3.zero);
#if UNITY_EDITOR
            Log($"Wandered to position: {position}");
#endif
            _isWandering = false;
            actionAfter?.Invoke();
        }

        public void WanderToRoom(Room room, Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log($"Wandering to room: {room.Name}");
#endif
            WanderToPosition(room.GetRandomPosition(), () =>
            {
                _currentRoom = room;
                _currentRoomWanderCount = 0;
#if UNITY_EDITOR
                Log($"Wandered to room: {room.Name}");
#endif
                actionAfter?.Invoke();
            });
        }

        public void WanderInRoom()
        {

        }

        public void AssertDominance(Action actionAfter = null)
        {
            StartCoroutine(AssertDominanceCoroutine(actionAfter));
        }

        private IEnumerator AssertDominanceCoroutine(Action actionAfter = null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _dominanceAssertionDuration)
            {
                elapsedTime += Time.deltaTime;

                transform.Rotate(_dominaceAssertionDirection * Time.deltaTime);

                yield return null;
            }

            actionAfter?.Invoke();
        }

        public void StartChasingPlayer()
        {
            _isChasing = true;
            _agent.isStopped = false;
        }

        public void StopChasingPlayer()
        {
            _isChasing = false;
            _agent.isStopped = true;

            _frontSpriteRenderer.color = Color.white;
            _backSpriteRenderer.color = Color.white;
        }

        public void DecideNextAction()
        {

        }

        public void Reload()
        {
            _previousRoom = _office.GetRandomRoom();
            Warp(_previousRoom.Center);

            StopChasingPlayer();
            StartChasingPlayer();
        }

        private static void Log(string message)
        {
            Debug.Log($"[BOSS] {message}");
        }
    }
}