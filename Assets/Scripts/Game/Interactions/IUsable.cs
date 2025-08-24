namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Interakció billentyûvel használható tárgyak által implementált interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IUsable : IInteractable
    {
        /// <summary>
        /// Használható-e az adott tárgynak az interakció billentyûvel, alapból <c>true</c>.
        /// </summary>
        bool IInteractable.IsUsable => true;

        /// <summary>
        /// Az interakció billentyûvel használható tárgy, alapból <c>this</c>, tehát az objektum, amin az implementáló script <br/>
        /// rajta van.
        /// </summary>
        IUsable IInteractable.Usable => this;

        /// <summary>
        /// A tárgy használatának leírása, alapból <c>null</c>. Használható az <see cref="IDescriptable"/> interface implementálása <br/>
        /// helyett.
        /// </summary>
        string Usage => null;

        /// <summary>
        /// A tárgy használata az interakció billentyûvel.
        /// </summary>
        void Use();
    }
}