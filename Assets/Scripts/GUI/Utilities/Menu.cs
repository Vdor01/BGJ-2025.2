using UnityEngine;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-t felépítõ menük õsosztálya a rájuk jellemzõ alapvetõ mûködéssel.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        // Fields
        [SerializeField] private GUIManager _gui;


        // Properties
        /// <summary>A játék felhasználói felületét kezelõ fõosztály.</summary>
        public GUIManager GUI => _gui;

        /// <summary>Megadja, hogy nyitva van-e épp a menü.</summary>
        public bool IsOpen => gameObject.activeSelf;
        /// <summary>Megadja, hogy zárva van-e épp a menü.</summary>
        public bool IsClosed => !IsOpen;


        // Methods
        /// <summary>
        /// Megnyitja a menüt, ha be van zárva.
        /// </summary>
        public void Open()
        {
            if (IsOpen) return;

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Bezárja a menüt, ha meg van nyitva.
        /// </summary>
        public void Close()
        {
            if (IsClosed) return;

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Nyitott-bezárt állapot között válotgatja a menüt.
        /// </summary>
        public void Toggle()
        {
            if (IsClosed)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}