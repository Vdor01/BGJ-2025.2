using BGJ_2025_2.Game.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Descriptable grabbable usable item")]
    public class DescriptableGrabbableUsableItem : MonoBehaviour, IDescriptable, IGrabbable, IUsable
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private string _usage;
        [SerializeField] private BoxCollider _boundingBox;
        [SerializeField] private UnityEvent _grabAction;
        [SerializeField] private UnityEvent _placeAction;
        [SerializeField] private UnityEvent _useAction;


        // Properties
        public string Name => _name;
        public string Description => _description;
        public string Usage => _usage;
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

        public void Use()
        {
            _useAction?.Invoke();
        }
    }
}