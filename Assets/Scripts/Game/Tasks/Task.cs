using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class Task
    {
        public TaskInfoSO info;

        public TaskState state;
        private int currentTaskStepIndex;

        public Task(TaskInfoSO taskInfo)
        {
            this.info = taskInfo;
            this.state = TaskState.NOT_AVAILABLE;
            this.currentTaskStepIndex = 0;
        }

        public void MoveToNextStep()
        {
            currentTaskStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (currentTaskStepIndex < info.taskStepPrefabs.Length);
        }

        public void InstantiateCurrentTaskStep(Transform parentTransform)
        {
            GameObject taskStepPrefab = GetCurrentTaskStepPrefab();
            if (taskStepPrefab != null)
            {
                TaskStep taskStep = Object.Instantiate<GameObject>(taskStepPrefab, parentTransform).GetComponent<TaskStep>();
                taskStep.InitializeTaskStep(info.id);
            }
        }

        private GameObject GetCurrentTaskStepPrefab()
        {
            GameObject taskStepPrefab = null;
            if (CurrentStepExists())
            {
                taskStepPrefab = info.taskStepPrefabs[currentTaskStepIndex];
            }
            else
            {
                Debug.LogWarning("No more task steps available. TaskId=" + info.id + ", stepIndex=" + currentTaskStepIndex);
            }
            return taskStepPrefab;
        }
    }
}