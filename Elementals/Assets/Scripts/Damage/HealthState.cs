using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthState : StatusValue, IHealth
{
    public ElementContainer container;
    public Element element;
    public Element Element => container != null ? container.Element : element; 
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

}