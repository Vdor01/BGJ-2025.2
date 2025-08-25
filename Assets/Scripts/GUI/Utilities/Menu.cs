using BGJ_2025_2.Game;
using BGJ_2025_2.Game.Players;
using UnityEngine;
using UnityEngine.UI;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-t fel�p�t� men�k �soszt�lya a r�juk jellemz� alapvet� m�k�d�ssel.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        // Fields
        [SerializeField] protected GUIManager _gui;

        [SerializeField] protected Selectable _defaultSelectable;


        // Properties
        /// <summary>A j�t�k felhaszn�l�i fel�let�t kezel� f�oszt�ly.</summary>
        public GUIManager GUI => _gui;
        /// <summary>A j�t�k �zleti logik�j�t kezel� f�oszt�ly</summary>
        public GameManager Game => _gui.Game;
        /// <summary>A j�t�kot j�tsz� j�t�kos</summary>
        public Player Player => Game.Player;

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

            _gui.AddOpenMenu(this);
        }

        /// <summary>
        /// Bez�rja a men�t, ha meg van nyitva.
        /// </summary>
        public void Close()
        {
            if (IsClosed) return;

            gameObject.SetActive(false);

            _gui.RemoveOpenMenu(this);
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

        /// <summary>
        /// Be�ll�tja a men�t alap�rt�kekre, �ltal�ban <c>Start</c>-on bel�l haszn�lhat�.
        /// </summary>
        public virtual void SetUp()
        {

        }

        /// <summary>
        /// A men� �llapot�t friss�ti, �ltal�ban <c>Update</c>-en bel�l haszn�lhat�.
        /// </summary>
        public virtual void Refresh()
        {

        }

        /// <summary>
        /// F�l�l�rhat� egyedi men�b�l val� visszal�pked�ses logika implement�l�s�ra, (pl. almen�k bez�r�sa sorrendben) <br/> 
        /// alapb�l csak bez�rja a men�t.
        /// </summary>
        public virtual void Cancel()
        {
            Close();
        }
    }
}