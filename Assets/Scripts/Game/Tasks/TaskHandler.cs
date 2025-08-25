using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    [AddComponentMenu("BGJ 2025.2/Game/Tasks/Task handler")]
    public class TaskHandler : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameManager _game;


        // Properties
        public GameManager Game => _game;
    }
}