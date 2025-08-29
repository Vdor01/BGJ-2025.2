using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class CleanToiletTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "Mop & Bucket";
        public string Description => "Use to clean the toilet";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
