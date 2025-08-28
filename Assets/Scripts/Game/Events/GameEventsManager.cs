using UnityEngine;

namespace BGJ_2025_2.Game.Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager Instance { get; private set; }

        public TaskEvents taskEvents;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple instances of GameEventsManager detected.");
            }
            Instance = this;

            taskEvents = new TaskEvents();
        }
    }
}