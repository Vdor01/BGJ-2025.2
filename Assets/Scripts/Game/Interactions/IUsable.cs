namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Interakci� billenty�vel haszn�lhat� t�rgyak �ltal implement�lt interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IUsable : IInteractable
    {
        /// <summary>
        /// Haszn�lhat�-e az adott t�rgynak az interakci� billenty�vel, alapb�l <c>true</c>.
        /// </summary>
        bool IInteractable.IsUsable => true;

        /// <summary>
        /// Az interakci� billenty�vel haszn�lhat� t�rgy, alapb�l <c>this</c>, teh�t az objektum, amin az implement�l� script <br/>
        /// rajta van.
        /// </summary>
        IUsable IInteractable.Usable => this;

        /// <summary>
        /// A t�rgy haszn�lat�nak le�r�sa, alapb�l <c>null</c>. Haszn�lhat� az <see cref="IDescriptable"/> interface implement�l�sa <br/>
        /// helyett.
        /// </summary>
        string Usage => null;

        /// <summary>
        /// A t�rgy haszn�lata az interakci� billenty�vel.
        /// </summary>
        void Use();
    }
}