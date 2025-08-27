using UnityEngine;

namespace BGJ_2025_2.Game.Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }

        public TaskEvents taskEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Multiple instances of GameEventsManager detected.");
            }
            instance = this;

            taskEvents = new TaskEvents();
        }
    }
}