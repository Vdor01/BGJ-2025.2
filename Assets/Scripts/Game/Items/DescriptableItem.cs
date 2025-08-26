using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Descriptable item")]
    public class DescriptableItem : MonoBehaviour, IDescriptable
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField] private string _description;


        // Properties
        public string Name => _name;
        public string Description => _description;
    }
}