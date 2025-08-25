using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Levels.Demo
{
    /// <summary>
    /// Egy demo script egy kock�hoz, ami az <see cref="IGrabbable"/> interface haszn�lat�t mutatja be.
    /// </summary>
    /// /// <seealso cref="IInteractable"/>
    /// <seealso cref="IGrabbable"/>
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Demo/Cube B")]
    public class CubeB : MonoBehaviour, IGrabbable, IDescriptable
    {
        // Properties
        public string Name => "Cube B";

        public string Description => "Can be grabbed / placed / thrown";
    }
}