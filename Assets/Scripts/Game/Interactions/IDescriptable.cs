namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Le�r�ssal ell�tott t�rgyak �ltal implement�lt interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IDescriptable : IInteractable
    {
        /// <summary>
        /// Van-e le�r�sa az adott t�rgynak, alapb�l <c>true</c>.
        /// </summary>
        bool IInteractable.IsDescriptable => true;

        /// <summary>
        /// Le�r�ssal rendelkez� t�rgy referenci�ja, alapb�l <c>this</c>, teh�t az objektum, amin az implement�l� script <br/>
        /// rajta van.
        /// </summary>
        IDescriptable IInteractable.Descriptable => this;

        /// <summary>
        /// A t�rgyhoz tartoz� le�r�s, ami a kurzor alatt jelenik meg.
        /// </summary>
        string Description { get; }
    }
}