using BGJ_2025_2.Game;
using BGJ_2025_2.GUI.Credits;
using BGJ_2025_2.GUI.Main;
using BGJ_2025_2.GUI.Overlay;
using BGJ_2025_2.GUI.Pause;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private CreditsMenu _creditsMenu;
        [SerializeField] private OverlayMenu _overlayMenu;
        [SerializeField] private PauseMenu _pauseMenu;
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
            _menus = new Menu[]
            {
                _mainMenu,
                _creditsMenu,
                _overlayMenu
            };

            _openMenus = new(Mathf.NextPowerOfTwo(_menus.Length));
        }

        private void Start()
        {
            foreach (Menu menu in _menus)
            {
                if (menu == _mainMenu)
                {
                    menu.Open();
                }
                else
                {
                    menu.Close();
                }
            }
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
    }
}