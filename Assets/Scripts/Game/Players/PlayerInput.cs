using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékostól érkezõ inputokat kezeli.
    /// </summary>
    /// <seealso cref="Player"/>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player input")]
    public class PlayerInput : PlayerComponent
    {
        // Fields
        private Inputs _inputs;


        // Methods
        private void Awake()
        {
            _inputs = new Inputs();
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}