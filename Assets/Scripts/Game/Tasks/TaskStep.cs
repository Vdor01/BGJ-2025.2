using BGJ_2025_2.Game.Events;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    [RequireComponent(typeof(MeshCollider))]
    public abstract class TaskStep : MonoBehaviour
    {
        private bool isFinished = false;
        private string taskId;

        public void InitializeTaskStep(string taskId)
        {
            this.taskId = taskId;
        }

        protected void FinishTaskStep()
        {
            if (!isFinished)
            {
                isFinished = true;
                GameEventsManager.Instance.taskEvents.AdvanceTask(taskId);
                Destroy(this.gameObject);
            }
        }
    }
}
