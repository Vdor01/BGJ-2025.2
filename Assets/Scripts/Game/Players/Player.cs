using BGJ_2025_2.Game.Levels;
using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékot játszó játékost reprezentálja. Elsõsorban összefogja a fõ részegységeket, amik a különbözõ <br/>
    /// aspektusokért felelnek.
    /// </summary>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player")]
    public class Player : MonoBehaviour
    {
        // Fields
        private const string RoomTag = "Room";

        [SerializeField] private GameManager _game;

        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerView _view;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerAppearance _appearance;
        [SerializeField] private PlayerAudio _audio;
        [SerializeField] private PlayerData _data;
        private Room _currentRoom;
        private bool _isFired;
        private bool _isRunning;
        private bool _isPaused;


        // Properties
        public GameManager Game => _game;
        public Office Office => _game.Office;

        public PlayerInput Input => _input;
        public PlayerMovement Movement => _movement;
        public PlayerView View => _view;
        public PlayerInteraction Interaction => _interaction;
        public PlayerAppearance Appearance => _appearance;
        public PlayerAudio Audio => _audio;
        public PlayerData Data => _data;
        public Room CurrentRoom => _currentRoom;


        // Methods
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(RoomTag))
            {
                Room enteredRoom = other.gameObject.GetComponent<RoomZone>().Room;
                if (enteredRoom != _currentRoom || _currentRoom == null)
                {
                    _currentRoom = enteredRoom;
#if UNITY_EDITOR
                    Debug.Log($"[PLAYER] Entered room: {_currentRoom.Name}");
#endif
                }
            }
        }

        public void Play()
        {
            _data.Reload();

            Reload();
        }

        public void End()
        {

        }

        public void Pause()
        {

        }

        public void Unpause()
        {

        }

        public void Reload()
        {
            _isFired = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.SetPositionAndRotation(_game.Office.Kitchen.Center + Vector3.up, Quaternion.Euler(0f, 180f, 0f));
        }

        public void NextDay()
        {
            Reload();
        }

        public void Fire()
        {
            if (_isFired) return;

            _data.Days = _game.Day;
            _data.Tasks += _game.Tasks.FinishedTaskCount;
            _isFired = true;

            _game.End();
        }
    }
}