using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Levels.Demo
{
    /// <summary>
    /// Egy demo script egy kockához, ami az <see cref="IUsable"/> interface használatát mutatja be.
    /// </summary>
    /// <seealso cref="IInteractable"/>
    /// <seealso cref="IUsable"/>
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Demo/Cube C")]
    public class CubeC : MonoBehaviour, IUsable, IDescriptable
    {
        // Properties
        public string Name => "Cube C";

        public string Description => "Can be used";

        public string Usage => "Use to: place higher";


        // Methods
        public void Use()
        {
            transform.position += Vector3.up * 2f;
        }
    }
}