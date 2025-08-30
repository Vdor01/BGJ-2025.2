using BGJ_2025_2.Game;
using BGJ_2025_2.GUI.Credits;
using BGJ_2025_2.GUI.Finish;
using BGJ_2025_2.GUI.Leaderboard;
using BGJ_2025_2.GUI.Main;
using BGJ_2025_2.GUI.Overlay;
using BGJ_2025_2.GUI.Pause;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A játék modell-view (MV) architektúrájában a view rész hierarchiájának tetején áll.
    /// </summary>
    /// <see cref="GameManager"/>
    [AddComponentMenu("BGJ 2025.2/GUI/GUI manager")]
    public class GUIManager : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameManager _game;
        [SerializeField] private EventSystem _eventSystem;

        [SerializeField] private TextMeshProUGUI _framesPerSecondLabel;
        [SerializeField] private TransitionPanel _transitionPanel;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private LeaderboardMenu _leaderboardMenu;
        [SerializeField] private CreditsMenu _creditsMenu;
        [SerializeField] private OverlayMenu _overlayMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private FinishMenu _finishMenu;
        private Inputs _inputs;
        private Menu[] _menus;
        private List<Menu> _openMenus;
        private Menu _lastOpenedMenu;


        // Properties
        public GameManager Game => _game;
        public EventSystem EventSystem => _eventSystem;

        public TransitionPanel TransitionPanel => _transitionPanel;
        public MainMenu MainMenu => _mainMenu;
        public LeaderboardMenu LeaderboardMenu => _leaderboardMenu;
        public CreditsMenu CreditsMenu => _creditsMenu;
        public OverlayMenu OverlayMenu => _overlayMenu;
        public PauseMenu PauseMenu => _pauseMenu;
        public FinishMenu FinishMenu => _finishMenu;


        // Methods
        private void Awake()
        {
            _inputs = new Inputs();

            _inputs.UI.Cancel.performed += callbackContext => Cancel(callbackContext);

            _inputs.UI.Map.performed += callbackContext => ToggleMap(callbackContext);

            _inputs.UI.Tasks.performed += callbackContext => ToggleTaskDescriptions(callbackContext);

            _inputs.UI.FPS.performed += callbackContext => ToggleFramesPerSecondCounter(callbackContext);

            _framesPerSecondLabel.gameObject.SetActive(false);

            _menus = new Menu[]
            {
                _mainMenu,
                _leaderboardMenu,
                _creditsMenu,
                _overlayMenu,
                _pauseMenu,
                _finishMenu
            };

            _openMenus = new(Mathf.NextPowerOfTwo(_menus.Length));
        }

        private void Start()
        {
            _transitionPanel.TransitionOut();

            foreach (Menu menu in _menus)
            {
                menu.gameObject.SetActive(false);
            }

            _mainMenu.Open();
        }

        private void Update()
        {
            if (_framesPerSecondLabel.gameObject.activeSelf)
            {
                _framesPerSecondLabel.SetText($"{_game.FramesPerSecond} FPS");
            }
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }

        public void AddOpenMenu(Menu menu)
        {
            if (_openMenus.Contains(menu)) return;

            _openMenus.Add(menu);
            _lastOpenedMenu = menu;
        }

        public void RemoveOpenMenu(Menu menu)
        {
            if (!_openMenus.Contains(menu)) return;

            _openMenus.Remove(menu);
        }

        public void Cancel(InputAction.CallbackContext callbackContext)
        {
            if (_lastOpenedMenu == _mainMenu) return;

            _lastOpenedMenu.Cancel();
        }

        public void ToggleMap(InputAction.CallbackContext callbackContext)
        {
            if (_game.IsRunning && !_game.IsPaused)
            {
                _overlayMenu.ToggleMap();
            }
        }

        public void ToggleTaskDescriptions(InputAction.CallbackContext callbackContext)
        {
            if (_game.IsRunning && !_game.IsPaused)
            {
                _overlayMenu.ToggleTaskDescriptions();
            }
        }

        public void ToggleFramesPerSecondCounter(InputAction.CallbackContext callbackContext)
        {
            _framesPerSecondLabel.gameObject.SetActive(!_framesPerSecondLabel.gameObject.activeSelf);
        }
    }
}