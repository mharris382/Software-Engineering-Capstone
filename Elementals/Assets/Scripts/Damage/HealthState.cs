using System;
using UnityEngine;
using UnityEngine.Events;

public interface IHealth
{
    bool isAlive { get; }
    void healHealth(float amount);
    void damageHealth(float amount);
}

public class HealthState : StatusValue, IHealth
{
    [Tooltip("Triggered when the entity gains health")]
    public UnityEvent OnHealed;
    
    [Tooltip("Triggered when the entity loses health")]
    public UnityEvent OnDamaged;
    
    [Tooltip("Triggered when the entity's health reaches zero")]
    public UnityEvent OnKilled;
    
    public bool isAlive => CurrentValue > 0;

    public void healHealth(float amount)
    {
        var prev = CurrentValue;
        CurrentValue += amount;
        if (prev < CurrentValue) OnHealed?.Invoke();
    }

    public void damageHealth(float amount)
    {
        if (isAlive == false) 
            return;
        var prev = CurrentValue;
        CurrentValue -= amount;
        if(prev > CurrentValue) OnDamaged?.Invoke();
        if(!isAlive) OnKilled?.Invoke();
    }

    public void OnDestroy()
    {
        
    }
}