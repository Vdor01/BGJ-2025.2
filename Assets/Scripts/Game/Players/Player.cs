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
        private Room _currentRoom;


        // Properties
        public GameManager Game => _game;
        public Office Office => _game.Office;

        public PlayerInput Input => _input;
        public PlayerMovement Movement => _movement;
        public PlayerView View => _view;
        public PlayerInteraction Interaction => _interaction;
        public PlayerAppearance Appearance => _appearance;
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

        public void Fire()
        {
            _game.End();
        }
    }
}