using BGJ_2025_2.Game;
using BGJ_2025_2.Game.Players;
using UnityEngine;
using UnityEngine.UI;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-t felépítõ menük õsosztálya a rájuk jellemzõ alapvetõ mûködéssel.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        // Fields
        [SerializeField] protected GUIManager _gui;

        [SerializeField] protected Selectable _defaultSelectable;


        // Properties
        /// <summary>A játék felhasználói felületét kezelõ fõosztály.</summary>
        public GUIManager GUI => _gui;
        /// <summary>A játék üzleti logikáját kezelõ fõosztály</summary>
        public GameManager Game => _gui.Game;
        /// <summary>A játékot játszó játékos</summary>
        public Player Player => Game.Player;

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

            _gui.AddOpenMenu(this);
        }

        /// <summary>
        /// Bezárja a menüt, ha meg van nyitva.
        /// </summary>
        public void Close()
        {
            if (IsClosed) return;

            gameObject.SetActive(false);

            _gui.RemoveOpenMenu(this);
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

        /// <summary>
        /// Beállítja a menüt alapértékekre, általában <c>Start</c>-on belül használható.
        /// </summary>
        public virtual void SetUp()
        {

        }

        /// <summary>
        /// A menü állapotát frissíti, általában <c>Update</c>-en belül használható.
        /// </summary>
        public virtual void Refresh()
        {

        }

        /// <summary>
        /// Fölülírható egyedi menübõl való visszalépkedéses logika implementálására, (pl. almenük bezárása sorrendben) <br/> 
        /// alapból csak bezárja a menüt.
        /// </summary>
        public virtual void Cancel()
        {
            Close();
        }
    }
}