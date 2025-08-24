using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Levels.Demo
{
    /// <summary>
    /// Egy demo script egy kockához, ami az <see cref="IDescriptable"/> interface használatát mutatja be.
    /// </summary>
    /// /// <seealso cref="IInteractable"/>
    /// <seealso cref="IDescriptable"/>
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Demo/Cube A")]
    public class CubeA : MonoBehaviour, IDescriptable
    {
        // Properties
        public string Name => "Cube A";

        public string Description => "Has a description";
    }
}