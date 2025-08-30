using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class GetDesignTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "New design";
        public string Description => "Looks like the old one, but newer";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
