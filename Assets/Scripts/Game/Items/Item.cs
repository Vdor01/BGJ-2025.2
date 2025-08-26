using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Item")]
    public class Item : MonoBehaviour, IInteractable
    {
        // Fields
        [SerializeField] private string _name;


        // Properties
        public string Name => _name;
    }
}