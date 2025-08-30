using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Tasks;
using UnityEngine;

public class OldDocumentTaskStep : TaskStep, IUsable, IDescriptable
{
    public string Name => "Old documents";
    public string Description => "Haven't been touched for ages, but it's still here";

    public void Use()
    {
        FinishTaskStep();
    }
}
