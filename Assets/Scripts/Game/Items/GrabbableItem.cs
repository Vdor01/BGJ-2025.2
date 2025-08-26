using BGJ_2025_2.Game.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Grabbable item")]
    public class GrabbableItem : MonoBehaviour, IGrabbable
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField] private UnityEvent _grabAction;
        [SerializeField] private UnityEvent _placeAction;


        // Properties
        public string Name => _name;


        // Methods
        public void Grab()
        {
            _grabAction?.Invoke();
        }

        public void Place()
        {
            _placeAction?.Invoke();
        }
    }
}