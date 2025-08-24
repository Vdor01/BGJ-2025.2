namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Minden tárgy, ami valamilyen interakciót kínál a játékos számára, implementálnia kell ezt az interface-t. <br/>
    /// Háromfajta interakció lehetséges: <br/>
    /// - <see cref="IDescriptable"/>: az adott tárgyhoz tartozik valami leírás, ami a kurzor alatt jelenik meg,<br/>
    /// - <see cref="IGrabbable"/>: az adott tárgy felvehetõ / lehelyezhetõ / eldobható,<br/>
    /// - <see cref="IUsable"/>: az adott tárgy használható interakció billentyûvel.<br/>
    /// Egy tárgy implementálhat ezek közül egyet, vagy akár többet is a funkcionalitásától függõen. Ha csak az <see cref="IInteractable"/> <br/>
    /// õsinterface-t implementálja, akkor csak egy névvel ellátott tárgy lesz extra funkcionalitás nélkül. <br/>
    /// <b>FONTOS:</b> minden interaktálható tárgynak az <i>Interactions</i> layer-en kell lennie, hogy mûködjön.
    /// </summary>
    /// <seealso cref="IDescriptable"/>
    /// <seealso cref="IGrabbable"/>
    /// <seealso cref="IUsable"/>
    public interface IInteractable
    {
        /// <summary>
        /// Az interakció neve, ami a kurzor fölött megjelenik, ha az adott tárgyra néz a játékos.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Van-e leírása az adott tárgynak, alapból <c>false</c>.
        /// </summary>
        bool IsDescriptable => false;

        /// <summary>
        /// Felvehetõ / lehelyezhetõ / eldobható-e az adott tárgy, alapból <c>false</c>.
        /// </summary>
        bool IsGrabbable => false;

        /// <summary>
        /// Használható-e az adott tárgynak az interakció billentyûvel, alapból <c>false</c>.
        /// </summary>
        bool IsUsable => false;

        /// <summary>
        /// A leírással rendelkezõ tárgy referenciája, alapból <c>null</c>.
        /// </summary>
        IDescriptable Descriptable => null;

        /// <summary>
        /// A felvehetõ / lehelyezhetõ / eldobható tárgy referenciája, alapból <c>null</c>.
        /// </summary>
        IGrabbable Grabbable => null;

        /// <summary>
        /// Az interakció billentyûvel használható tárgy, alapból <c>null</c>.
        /// </summary>
        IUsable Usable => null;
    }
}