using DungeonMan.CustomAttributes;
using System.Threading.Tasks;
using UnityEngine;

public class HealSelf : AbilityBehavior
{
    private AbilityController _controller;
    [StaticBool("valueBool")] public float _appliedValue;

    [HideInInspector] public bool valueBool;
    public override Task Execute(float effectivenessFactor)
    {
        if(valueBool) _controller.CharachterData.Heal(_appliedValue * effectivenessFactor);
        else _controller.CharachterData.Heal(_appliedValue);

        return Task.CompletedTask;
    }

    public override void Initialize(AbilityController abilityController)
    {
        _controller = abilityController;
    }
}
