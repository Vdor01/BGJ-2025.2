namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Felvehet� / letehet� / eldobhat� t�rgyak �ltal implement�lt interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IGrabbable : IInteractable
    {
        /// <summary>
        /// Felvehet� / lehelyezhet� / eldobhat�-e az adott t�rgy, alapb�l <c>true</c>.
        /// </summary>
        bool IInteractable.IsGrabbable => true;

        /// <summary>
        /// A felvehet� / lehelyezhet� / eldobhat� t�rgy referenci�ja, alapb�l <c>this</c>, teh�t az objektum, amin az <br/>
        /// implement�l� script rajta van.
        /// </summary>
        IGrabbable IInteractable.Grabbable => this;

        /// <summary>
        /// A felv�tel ut�n v�grehajthat� funkcionalit�s.
        /// </summary>
        void Grab() { }

        /// <summary>
        /// A lehelyez�s ut�n v�grehajthat� funkcionalit�s.
        /// </summary>
        void Place() { }
    }
}
