using UnityEngine;

namespace BGJ_2025_2.GUI.Main
{
    /// <summary>
    /// A játék fõmenüje.
    /// </summary>
    /// <seealso cref="Menu"/>
    [AddComponentMenu("BGJ 2025.2/GUI/Main/Main menu")]
    public class MainMenu : Menu
    {
        // Fields
#if UNITY_EDITOR
        [Header("Development options")]
        [SerializeField] private bool _skip;
#endif

        // Methods
#if UNITY_EDITOR
        private void Start()
        {
            if (_skip)
            {
                Close();
                _gui.OverlayMenu.Open();
                Game.Play();
            }
        }
#endif
        public void PlayGame()
        {
            _gui.TransitionPanel.TransitionInAndOut(() =>
            {
                Close();
                _gui.OverlayMenu.Open();
                Game.Play();
            }, text: "Day 1");
        }

        public void OpenCredits()
        {
            _gui.CreditsMenu.Open();

            Close();
        }
    }
}