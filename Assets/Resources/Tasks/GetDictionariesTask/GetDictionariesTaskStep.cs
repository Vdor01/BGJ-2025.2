using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public class GetDictionariesTaskStep : TaskStep, IUsable, IDescriptable
    {
        public string Name => "Dictionaries";
        public string Description => "You never even heard of these languages";

        public void Use()
        {
            FinishTaskStep();
        }
    }
}
