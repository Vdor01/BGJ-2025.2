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
        [SerializeField] private GameObject _exitButton;
#if UNITY_EDITOR
        [Header("Development options")]
        [SerializeField] private bool _skip;
#endif

        // Methods

        private void Start()
        {
#if UNITY_EDITOR
            if (_skip)
            {
                Close();
                _gui.OverlayMenu.Open();
                Game.Play();
            }
#elif UNITY_WEBGL
            _exitButton.gameObject.SetActive(false);
#endif
        }

        public void PlayGame()
        {
            _gui.TransitionPanel.TransitionInAndOut(() =>
            {
                Close();
                _gui.OverlayMenu.Open();
                Game.Play();
            }, text: "Day 1");
        }

        public void OpenLeaderboard()
        {
            _gui.LeaderboardMenu.Open();

            Close();
        }

        public void OpenCredits()
        {
            _gui.CreditsMenu.Open();

            Close();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}