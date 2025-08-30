using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class GetApprovalTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "HR APPROVED papers";
        public string Description => "It must be important";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
