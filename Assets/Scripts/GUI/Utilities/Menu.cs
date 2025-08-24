using UnityEngine;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-t fel�p�t� men�k �soszt�lya a r�juk jellemz� alapvet� m�k�d�ssel.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        // Fields
        [SerializeField] private GUIManager _gui;


        // Properties
        /// <summary>A j�t�k felhaszn�l�i fel�let�t kezel� f�oszt�ly.</summary>
        public GUIManager GUI => _gui;

        /// <summary>Megadja, hogy nyitva van-e �pp a men�.</summary>
        public bool IsOpen => gameObject.activeSelf;
        /// <summary>Megadja, hogy z�rva van-e �pp a men�.</summary>
        public bool IsClosed => !IsOpen;


        // Methods
        /// <summary>
        /// Megnyitja a men�t, ha be van z�rva.
        /// </summary>
        public void Open()
        {
            if (IsOpen) return;

            gameObject.SetActive(true);
        }

        /// <summary>
        /// Bez�rja a men�t, ha meg van nyitva.
        /// </summary>
        public void Close()
        {
            if (IsClosed) return;

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Nyitott-bez�rt �llapot k�z�tt v�lotgatja a men�t.
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