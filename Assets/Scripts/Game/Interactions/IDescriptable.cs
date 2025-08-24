namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Leírással ellátott tárgyak által implementált interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IDescriptable : IInteractable
    {
        /// <summary>
        /// Van-e leírása az adott tárgynak, alapból <c>true</c>.
        /// </summary>
        bool IInteractable.IsDescriptable => true;

        /// <summary>
        /// Leírással rendelkezõ tárgy referenciája, alapból <c>this</c>, tehát az objektum, amin az implementáló script <br/>
        /// rajta van.
        /// </summary>
        IDescriptable IInteractable.Descriptable => this;

        /// <summary>
        /// A tárgyhoz tartozó leírás, ami a kurzor alatt jelenik meg.
        /// </summary>
        string Description { get; }
    }
}