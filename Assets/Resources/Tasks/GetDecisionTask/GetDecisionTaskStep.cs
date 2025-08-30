using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class GetDecisionTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "Meeting Decisions";
        public string Description => "Took them a while...";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
