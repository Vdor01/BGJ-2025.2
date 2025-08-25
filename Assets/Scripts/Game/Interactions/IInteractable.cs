namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Minden t�rgy, ami valamilyen interakci�t k�n�l a j�t�kos sz�m�ra, implement�lnia kell ezt az interface-t. <br/>
    /// H�romfajta interakci� lehets�ges: <br/>
    /// - <see cref="IDescriptable"/>: az adott t�rgyhoz tartozik valami le�r�s, ami a kurzor alatt jelenik meg,<br/>
    /// - <see cref="IGrabbable"/>: az adott t�rgy felvehet� / lehelyezhet� / eldobhat�,<br/>
    /// - <see cref="IUsable"/>: az adott t�rgy haszn�lhat� interakci� billenty�vel.<br/>
    /// Egy t�rgy implement�lhat ezek k�z�l egyet, vagy ak�r t�bbet is a funkcionalit�s�t�l f�gg�en. Ha csak az <see cref="IInteractable"/> <br/>
    /// �sinterface-t implement�lja, akkor csak egy n�vvel ell�tott t�rgy lesz extra funkcionalit�s n�lk�l. <br/>
    /// <b>FONTOS:</b> minden interakt�lhat� t�rgynak az <i>Interactions</i> layer-en kell lennie, hogy m�k�dj�n.
    /// </summary>
    /// <seealso cref="IDescriptable"/>
    /// <seealso cref="IGrabbable"/>
    /// <seealso cref="IUsable"/>
    public interface IInteractable
    {
        /// <summary>
        /// Az interakci� neve, ami a kurzor f�l�tt megjelenik, ha az adott t�rgyra n�z a j�t�kos.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Van-e le�r�sa az adott t�rgynak, alapb�l <c>false</c>.
        /// </summary>
        bool IsDescriptable => false;

        /// <summary>
        /// Felvehet� / lehelyezhet� / eldobhat�-e az adott t�rgy, alapb�l <c>false</c>.
        /// </summary>
        bool IsGrabbable => false;

        /// <summary>
        /// Haszn�lhat�-e az adott t�rgynak az interakci� billenty�vel, alapb�l <c>false</c>.
        /// </summary>
        bool IsUsable => false;

        /// <summary>
        /// A le�r�ssal rendelkez� t�rgy referenci�ja, alapb�l <c>null</c>.
        /// </summary>
        IDescriptable Descriptable => null;

        /// <summary>
        /// A felvehet� / lehelyezhet� / eldobhat� t�rgy referenci�ja, alapb�l <c>null</c>.
        /// </summary>
        IGrabbable Grabbable => null;

        /// <summary>
        /// Az interakci� billenty�vel haszn�lhat� t�rgy, alapb�l <c>null</c>.
        /// </summary>
        IUsable Usable => null;
    }
}