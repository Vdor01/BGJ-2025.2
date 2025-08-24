using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A j�t�kot j�tsz� j�t�kost reprezent�lja.
    /// </summary>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player")]
    public class Player : MonoBehaviour
    {
        // Fields
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInteraction _interaction;


        // Properties
        public PlayerInput Input => _input;
        public PlayerMovement Movement => _movement;
        public PlayerInteraction Interaction => _interaction;
    }
}