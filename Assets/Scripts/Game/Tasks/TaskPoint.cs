using BGJ_2025_2.Game.Events;
using BGJ_2025_2.Game.Players;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    [AddComponentMenu("BGJ_2025_2/Game/Tasks/Task Point")]
    public class TaskPoint : MonoBehaviour
    {
        private Inputs inputs;

        [Header("Task")]
        [SerializeField] private TaskInfoSO taskInfoForPoint;

        [Header("Config")]
        [SerializeField] private bool startPoint = true;
        [SerializeField] private bool finishPoint = true;

        private int playerIsNear = 0;
        private string taskId;
        private TaskState currentTaskState;

        private void Awake()
        {
            taskId = taskInfoForPoint.id;

            inputs = new Inputs();

            inputs.Player.Use.performed += ctx => SubmitPressed();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.taskEvents.onTaskStateChange += TaskStateChange;
            inputs.Player.Use.Enable();
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.taskEvents.onTaskStateChange -= TaskStateChange;
            inputs.Player.Use.Disable();
        }

        private void SubmitPressed()
        {
            if (playerIsNear <= 0)
            {
                playerIsNear = 0;
                return;
            }

            if (currentTaskState.Equals(TaskState.CAN_START) && startPoint)
            {
                GameEventsManager.Instance.taskEvents.StartTask(taskId);
            }
            else if (currentTaskState.Equals(TaskState.CAN_FINISH) && finishPoint)
            {
                GameEventsManager.Instance.taskEvents.FinishTask(taskId);
            }

            Debug.Log($"TaskPoint {taskId} detected SubmitPressed, current state: {currentTaskState}");
        }

        private void TaskStateChange(Task task)
        {
            if (task.info.id.Equals(taskId))
            {
                currentTaskState = task.state;
                Debug.Log($"TaskPoint {taskId} detected state change: {currentTaskState}");
            }
        }

        public void OnEnter()
        {
            playerIsNear += 1;
            Debug.Log($"Player entered TaskPoint {taskId} area, current state: {currentTaskState}");
        }

        public void OnExit()
        {
            playerIsNear -= 1;
            Debug.Log($"Player exited TaskPoint {taskId} area, current state: {currentTaskState}");
        }
    }
}
