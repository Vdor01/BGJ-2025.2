using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishTaskStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // TODO - Advance to the next step in the task

            Destroy(this.gameObject);
        }
    }
}
