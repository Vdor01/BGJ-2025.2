using BGJ_2025_2.Game;
using BGJ_2025_2.GUI.Credits;
using BGJ_2025_2.GUI.Main;
using BGJ_2025_2.GUI.Overlay;
using BGJ_2025_2.GUI.Pause;
using System.Collections.Generic;
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

        [SerializeField] private Camera _camera;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private CreditsMenu _creditsMenu;
        [SerializeField] private OverlayMenu _overlayMenu;
        [SerializeField] private PauseMenu _pauseMenu;
        private Inputs _inputs;
        private Menu[] _menus;
        private List<Menu> _openMenus;
        private Menu _lastOpenedMenu;


        // Properties
        public GameManager Game => _game;
        public EventSystem EventSystem => _eventSystem;

        public MainMenu MainMenu => _mainMenu;
        public CreditsMenu CreditsMenu => _creditsMenu;
        public OverlayMenu OverlayMenu => _overlayMenu;
        public PauseMenu PauseMenu => _pauseMenu;


        // Methods
        private void Awake()
        {
            _inputs = new Inputs();

            _inputs.UI.Cancel.performed += callbackContext => Cancel(callbackContext);

            _menus = new Menu[]
            {
                _mainMenu,
                _creditsMenu,
                _overlayMenu
            };

            _openMenus = new(Mathf.NextPowerOfTwo(_menus.Length));
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }

        private void Start()
        {
            EnableCamera();

            foreach (Menu menu in _menus)
            {
                menu.gameObject.SetActive(false);
            }

            _mainMenu.Open();
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
            if (_lastOpenedMenu == _mainMenu || _lastOpenedMenu == _overlayMenu) return;

            _lastOpenedMenu.Cancel();
        }

        public void EnableCamera()
        {
            _camera.gameObject.SetActive(true);
        }

        public void DisableCamera()
        {
            _camera.gameObject.SetActive(false);
        }
    }
}