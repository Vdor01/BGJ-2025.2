using BGJ_2025_2.Game.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Descriptable grabbable item")]
    public class DescriptableGrabbableItem : MonoBehaviour, IDescriptable, IGrabbable
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private BoxCollider _boundingBox;
        [SerializeField] private UnityEvent _grabAction;
        [SerializeField] private UnityEvent _placeAction;


        // Properties
        public string Name => _name;
        public string Description => _description;
        public BoxCollider BoundingBox => _boundingBox;


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