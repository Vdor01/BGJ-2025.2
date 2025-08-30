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
            Debug.Log("Starting task with id: " + id);

            Task task = GetTaskById(id);
            task.InstantiateCurrentTaskStep(this.transform);
            ChangeTaskState(task.info.id, TaskState.IN_PROGRESS);

            ManageActiveTasks();
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

            ManageActiveTasks();
        }

        private void FinishTask(string id)
        {
            Debug.Log("Finishing task with id: " + id);

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
            taskToday.GetComponent<TextMeshProUGUI>().SetText($"Tasks done today: {finishedTaskCount} / 5");

            // Check if we need to reset all finished tasks
            int remainingUnusedTasks = 0;
            foreach (bool used in taskTab.Values)
            {
                if (!used) remainingUnusedTasks++;
            }

            Debug.Log($"Remaining unused tasks: {remainingUnusedTasks}, activeTaskNumber: {activeTaskNumber}");

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
                        Debug.LogWarning($"Resetting taskTab for taskId: {taskId}");
                    }
                }
                Debug.Log("Not enough unused tasks available, resetting all finished tasks.");
            }

            ManageActiveTasks();
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
                        Debug.LogWarning("No unused tasks available to fill active task slots!");
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
                            Debug.LogError("Unable to find an unused task after multiple attempts!");
                            break;
                        }
                    }

                    if (taskTab[randomTaskId] == false) // Only proceed if we found an unused task
                    {
                        Debug.Log("Randomly selected task id: " + randomTaskId);
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