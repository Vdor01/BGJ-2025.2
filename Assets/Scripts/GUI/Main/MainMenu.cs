using UnityEngine;

namespace BGJ_2025_2.GUI.Main
{
    /// <summary>
    /// A j�t�k f�men�je.
    /// </summary>
    /// <seealso cref="Menu"/>
    [AddComponentMenu("BGJ 2025.2/GUI/Main/Main menu")]
    public class MainMenu : Menu
    {
        // Methods
        public void PlayGame()
        {
            Close();

            Game.Play();

            _gui.DisableCamera();
            _gui.OverlayMenu.Open();
        }

        public void OpenCredits()
        {
            _gui.CreditsMenu.Open();

            Close();
        }
    }
}