using System.Collections.Generic;
using BGJ_2025_2.Game.Events;
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

        private Dictionary<string, Task> taskMap;

        private void Awake()
        {
            taskMap = CreateTaskMap();
        }

        private void OnEnable()
        {
            GameEventsManager.instance.taskEvents.onStartTask += StartQuest;
            GameEventsManager.instance.taskEvents.onAdvanceTask += AdvanceQuest;
            GameEventsManager.instance.taskEvents.onFinishTask += FinishQuest;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.taskEvents.onStartTask -= StartQuest;
            GameEventsManager.instance.taskEvents.onAdvanceTask -= AdvanceQuest;
            GameEventsManager.instance.taskEvents.onFinishTask -= FinishQuest;
        }

        private void Start()
        {
            // Broadcast the initial state of all tasks
            foreach (Task task in taskMap.Values)
            {
                GameEventsManager.instance.taskEvents.TaskStateChange(task);
            }
        }

        private void StartQuest(string id)
        {
            // TODO - start the task
        }

        private void AdvanceQuest(string id)
        {
            // TODO - advance the task
        }

        private void FinishQuest(string id)
        {
            // TODO - finish the task
        }

        private Dictionary<string, Task> CreateTaskMap()
        {
            // Load all TaskInfoSO Scriptable Objects under the Assets/Resources/Tasks folder
            TaskInfoSO[] allTasks = Resources.LoadAll<TaskInfoSO>("Tasks");

            Dictionary<string, Task> idToTaskMap = new Dictionary<string, Task>();
            foreach (TaskInfoSO taskInfo in allTasks)
            {
                if (idToTaskMap.ContainsKey(taskInfo.id))
                {
                    Debug.LogWarning("Duplicate TaskInfoSO id found: " + taskInfo.id + ". Please ensure all TaskInfoSO ids are unique.");
                }
                idToTaskMap.Add(taskInfo.id, new Task(taskInfo));
            }
            return idToTaskMap;
        }

        private Task GetTaskById(string id)
        {
            Task task = taskMap[id];
            if (task == null)
            {
                Debug.LogError("No task found with id: " + id);
            }
            return task;
        }
    }
}