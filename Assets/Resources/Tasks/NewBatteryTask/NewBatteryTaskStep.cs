using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Tasks;
using UnityEngine;

public class NewBatteryTaskStep : TaskStep, IUsable, IDescriptable
{
    public string Name => "Batteries";
    public string Description => "Useful for small electronic devices, like remotes";

    public void Use()
    {
        FinishTaskStep();
    }
}
