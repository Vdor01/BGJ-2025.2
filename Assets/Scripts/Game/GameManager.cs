using BGJ_2025_2.Game.Leaderboards;
using BGJ_2025_2.Game.Levels;
using BGJ_2025_2.Game.Players;
using BGJ_2025_2.Game.Tasks;
using BGJ_2025_2.GUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BGJ_2025_2.Game
{
    /// <summary>
    /// A játék modell-view (MV) architektúrájában a modell rész hierarchiájának tetején áll.
    /// </summary>
    /// <see cref="GUIManager"/>
    [AddComponentMenu("BGJ 2025.2/Game/Game manager")]
    public class GameManager : MonoBehaviour
    {
        // Fields
        private const int _TargetFrameRate = 60;

        [SerializeField] private GUIManager _gui;
        [SerializeField] private EventSystem _eventSystem;

        [SerializeField] private GameObject _menuRoom;
        [SerializeField] private Player _player;
        [SerializeField] private Office _office;
        [SerializeField] private TaskHandler _tasks;
        [SerializeField] private LeaderboardHandler _leaderboards;
        private float _elapsedSecond;
        private int _elapsedFrames;
        private int _framesPerSecond;


        // Properties
        public GUIManager GUI => _gui;
        public EventSystem EventSystem => _eventSystem;

        public int FramesPerSecond => _framesPerSecond;
        public Player Player => _player;
        public Office Office => _office;
        public TaskHandler Tasks => _tasks;
        public LeaderboardHandler Leaderboards => _leaderboards;


        // Methods
        private void Awake()
        {
            Application.targetFrameRate = _TargetFrameRate;
        }

        private void Start()
        {
            _player.gameObject.SetActive(false);

            EnableMenuRoom();
        }

        private void Update()
        {
            _elapsedSecond += Time.deltaTime;

            if (_elapsedSecond >= 1f)
            {
                _framesPerSecond = _elapsedFrames;
                _elapsedFrames = 0;
                _elapsedSecond -= 1f;
            }

            ++_elapsedFrames;
        }

        public void Play()
        {
            DisableMenuCamera();

            _player.gameObject.SetActive(true);
        }

        public void EnableMenuRoom()
        {
            _menuRoom.gameObject.SetActive(true);
        }

        public void DisableMenuCamera()
        {
            _menuRoom.gameObject.SetActive(false);
        }
    }
}