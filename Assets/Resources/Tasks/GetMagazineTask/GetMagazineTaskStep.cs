using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class GetMagazineTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "New magazine";
        public string Description => "Did they even use the new design?";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
