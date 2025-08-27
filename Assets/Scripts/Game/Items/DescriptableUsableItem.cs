using BGJ_2025_2.Game.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Descriptable usable item")]
    public class DescriptableUsableItem : MonoBehaviour, IDescriptable, IUsable
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private string _usage;
        [SerializeField] private UnityEvent _useAction;


        // Properties
        public string Name => _name;
        public string Description => _description;
        public string Usage => _usage;


        // Methods
        public void Use()
        {
            _useAction?.Invoke();
        }
    }
}