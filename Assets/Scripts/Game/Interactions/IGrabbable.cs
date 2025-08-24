namespace BGJ_2025_2.Game.Interactions
{
    /// <summary>
    /// Felvehetõ / letehetõ / eldobható tárgyak által implementált interface.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    public interface IGrabbable : IInteractable
    {
        /// <summary>
        /// Felvehetõ / lehelyezhetõ / eldobható-e az adott tárgy, alapból <c>true</c>.
        /// </summary>
        bool IInteractable.IsGrabbable => true;

        /// <summary>
        /// A felvehetõ / lehelyezhetõ / eldobható tárgy referenciája, alapból <c>this</c>, tehát az objektum, amin az <br/>
        /// implementáló script rajta van.
        /// </summary>
        IGrabbable IInteractable.Grabbable => this;

        /// <summary>
        /// A felvétel után végrehajtható funkcionalitás.
        /// </summary>
        void Grab() { }

        /// <summary>
        /// A lehelyezés után végrehajtható funkcionalitás.
        /// </summary>
        void Place() { }
    }
}
