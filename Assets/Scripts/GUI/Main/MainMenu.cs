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
        // Methods
        public void PlayGame()
        {
            Close();

            Game.Play();

            _gui.OverlayMenu.Open();
        }
    }
}