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
            GameEventsManager.Instance.taskEvents.onStartTask += StartTask;
            GameEventsManager.Instance.taskEvents.onAdvanceTask += AdvanceTask;
            GameEventsManager.Instance.taskEvents.onFinishTask += FinishTask;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.taskEvents.onStartTask -= StartTask;
            GameEventsManager.Instance.taskEvents.onAdvanceTask -= AdvanceTask;
            GameEventsManager.Instance.taskEvents.onFinishTask -= FinishTask;
        }

        private void Start()
        {
            // Broadcast the initial state of all tasks
            foreach (Task task in taskMap.Values)
            {
                GameEventsManager.Instance.taskEvents.TaskStateChange(task);
            }
        }

        private void ChangeTaskState(string id, TaskState state)
        {
            Task task = GetTaskById(id);
            task.state = state;
            GameEventsManager.Instance.taskEvents.TaskStateChange(task);
        }

        private void StartTask(string id)
        {
            Debug.Log("Starting task with id: " + id);

            Task task = GetTaskById(id);
            task.InstantiateCurrentTaskStep(this.transform);
            ChangeTaskState(task.info.id, TaskState.IN_PROGRESS);
        }

        private void AdvanceTask(string id)
        {
            Debug.Log("Advancing task with id: " + id);

            Task task = GetTaskById(id);

            task.MoveToNextStep();

            if (task.CurrentStepExists())
            {
                task.InstantiateCurrentTaskStep(this.transform);
            }
            else
            {
                ChangeTaskState(task.info.id, TaskState.CAN_FINISH);
            }
        }

        private void FinishTask(string id)
        {
            Debug.Log("Finishing task with id: " + id);

            Task task = GetTaskById(id);
            // TODO - reward player here, not fired yet
            ChangeTaskState(task.info.id, TaskState.FINISHED);
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
                //Debug.Log("Loaded task: " + taskInfo.name);
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