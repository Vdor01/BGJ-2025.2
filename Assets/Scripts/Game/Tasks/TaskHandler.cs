using BGJ_2025_2.Game.Events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    [AddComponentMenu("BGJ 2025.2/Game/Tasks/Task handler")]
    public class TaskHandler : MonoBehaviour
    {
        // Fields
        public const int MinTaskCount = 5;

        [SerializeField] private GameManager _game;

        [Header("Config")]
        [SerializeField] private int activeTaskNumber;

        [Header("Canvas")]
        [SerializeField] private GameObject taskToday;
        [SerializeField] private GameObject[] tasks;

        // Properties
        public GameManager Game => _game;

        private Dictionary<string, Task> taskMap;
        private Dictionary<string, bool> taskTab;
        private List<Task> activeTasks;

        private int finishedTaskCount = 0;


        public int FinishedTaskCount => finishedTaskCount;

        private void Awake()
        {
            taskMap = CreateTaskMap();

            taskTab = new Dictionary<string, bool>();
            foreach (string id in taskMap.Keys)
            {
                taskTab.Add(id, false);
            }
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

            ManageActiveTasks();
        }

        private void ChangeTaskState(string id, TaskState state)
        {
            Task task = GetTaskById(id);
            task.state = state;
            GameEventsManager.Instance.taskEvents.TaskStateChange(task);
        }

        private void StartTask(string id)
        {
#if UNITY_EDITOR
            Debug.Log("Starting task with id: " + id);
#endif

            Task task = GetTaskById(id);
            task.InstantiateCurrentTaskStep(this.transform);
            ChangeTaskState(task.info.id, TaskState.IN_PROGRESS);

            ManageActiveTasks();
        }

        private void AdvanceTask(string id)
        {
#if UNITY_EDITOR
            Debug.Log("Advancing task with id: " + id);
#endif

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

            ManageActiveTasks();
        }

        private void FinishTask(string id)
        {
#if UNITY_EDITOR
            Debug.Log("Finishing task with id: " + id);
#endif

            Task task = GetTaskById(id);

            finishedTaskCount++;

            // Remove from active tasks first
            if (activeTasks != null && activeTasks.Contains(task))
            {
                activeTasks.Remove(task);
            }

            // Mark the task as unused in taskTab so it can be selected again
            taskTab[id] = false;

            // Change state to finished
            ChangeTaskState(task.info.id, TaskState.FINISHED);

            // Update UI
            taskToday.GetComponent<TextMeshProUGUI>().SetText($"Tasks done today: {finishedTaskCount} / {MinTaskCount}");

            // Check if we need to reset all finished tasks
            int remainingUnusedTasks = 0;
            foreach (bool used in taskTab.Values)
            {
                if (!used) remainingUnusedTasks++;
            }
#if UNITY_EDITOR
            Debug.Log($"Remaining unused tasks: {remainingUnusedTasks}, activeTaskNumber: {activeTaskNumber}");
#endif

            if (remainingUnusedTasks < activeTaskNumber)
            {
                // Create a copy of the keys to avoid modifying collection during enumeration
                List<string> taskIds = new List<string>(taskTab.Keys);
                foreach (string taskId in taskIds)
                {
                    if (GetTaskById(taskId).state == TaskState.FINISHED)
                    {
                        taskTab[taskId] = false;
                        ChangeTaskState(taskId, TaskState.NOT_AVAILABLE);
#if UNITY_EDITOR
                        Debug.LogWarning($"Resetting taskTab for taskId: {taskId}");
#endif
                    }
                }
#if UNITY_EDITOR
                Debug.Log("Not enough unused tasks available, resetting all finished tasks.");
#endif
            }

            ManageActiveTasks();

            if (_game.Office.CookieJar.IsEmpty)
            {
                _game.EndDay();
            }
        }

        private void ManageActiveTasks()
        {
            while (activeTasks == null || activeTasks.Count < activeTaskNumber)
            {
                // Choose a random task from taskMap
                List<string> taskIds = new List<string>(taskMap.Keys);
                if (taskIds.Count > 0)
                {
                    // Check if there are any unused tasks available
                    bool hasUnusedTasks = false;
                    foreach (string taskId in taskIds)
                    {
                        if (taskTab[taskId] == false)
                        {
                            hasUnusedTasks = true;
                            break;
                        }
                    }

                    if (!hasUnusedTasks)
                    {
#if UNITY_EDITOR
                        Debug.LogWarning("No unused tasks available to fill active task slots!");
#endif
                        break; // Exit the while loop to prevent infinite loop
                    }

                    string randomTaskId = "";
                    int attempts = 0;
                    while (randomTaskId == "" || taskTab[randomTaskId] == true)
                    {
                        int randomIndex = Random.Range(0, taskIds.Count);
                        randomTaskId = taskIds[randomIndex];

                        attempts++;
                        if (attempts > taskIds.Count * 2) // Safety check to prevent infinite loop
                        {
#if UNITY_EDITOR
                            Debug.LogError("Unable to find an unused task after multiple attempts!");
#endif
                            break;
                        }
                    }

                    if (taskTab[randomTaskId] == false) // Only proceed if we found an unused task
                    {
#if UNITY_EDITOR
                        Debug.Log("Randomly selected task id: " + randomTaskId);
#endif
                        // You can now use randomTaskId to start or process the task
                        ChangeTaskState(randomTaskId, TaskState.CAN_START);
                        taskTab[randomTaskId] = true;
                        if (activeTasks == null)
                        {
                            activeTasks = new List<Task>();
                        }
                        activeTasks.Add(taskMap[randomTaskId]);
                    }
                    else
                    {
                        break; // Exit if we couldn't find a valid task
                    }
                }
                else
                {
                    break; // No tasks available at all
                }
            }

            if (activeTasks != null && activeTasks.Count > 0)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (i < activeTasks.Count)
                    {
                        tasks[i].SetActive(true);
                        Task task = activeTasks[i];
                        TaskState state = task.state;
                        tasks[i].GetComponent<TextMeshProUGUI>().SetText(task.info.displayName);
                        if (state == TaskState.IN_PROGRESS)
                        {
                            tasks[i].GetComponent<TextMeshProUGUI>().color = Color.yellow;
                        }
                        else if (state == TaskState.CAN_FINISH)
                        {
                            tasks[i].GetComponent<TextMeshProUGUI>().color = Color.green;
                        }
                        else
                        {
                            tasks[i].GetComponent<TextMeshProUGUI>().color = Color.white;
                        }
                    }
                    else
                    {
                        tasks[i].SetActive(false);
                    }
                }
                taskToday.SetActive(true);
            }
            else
            {
                taskToday.SetActive(false);
            }
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
#if UNITY_EDITOR
                    Debug.LogWarning("Duplicate TaskInfoSO id found: " + taskInfo.id + ". Please ensure all TaskInfoSO ids are unique.");
#endif
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
#if UNITY_EDITOR
                Debug.LogError("No task found with id: " + id);
#endif
            }
            return task;
        }

        public void ClearFinishedTaskCount()
        {
            finishedTaskCount = 0;
        }

        public void Clear()
        {
            for (int i = activeTasks.Count - 1; i >= 0; --i)
            {
                FinishTask(activeTasks[i].info.id);
            }
            finishedTaskCount = 0;

            taskToday.GetComponent<TextMeshProUGUI>().SetText($"Tasks done today: {finishedTaskCount} / {MinTaskCount}");
        }
    }
}