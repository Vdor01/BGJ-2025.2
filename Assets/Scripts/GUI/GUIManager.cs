using BGJ_2025_2.Game;
using BGJ_2025_2.GUI.Credits;
using BGJ_2025_2.GUI.Main;
using BGJ_2025_2.GUI.Overlay;
using UnityEngine;

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

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private CreditsMenu _creditsMenu;
        [SerializeField] private OverlayMenu _overlayMenu;


        // Properties
        public GameManager Game => _game;

        public MainMenu MainMenu => _mainMenu;
        public CreditsMenu CreditsMenu => _creditsMenu;
        public OverlayMenu OverlayMenu => _overlayMenu;
    }
}