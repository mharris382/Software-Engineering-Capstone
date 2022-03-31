using System;
using Damage;
using Elements;
using UnityEngine;
using UnityEngine.Events;

public class HealthState : StatusValue, IHealth, IElementalDependent
{
    public ElementContainer container;
    public Element element;

    public Element Element
    {
        get => container != null ? container.Element : element;
        set
        {
            if(container!=null)
                Debug.LogError("Shared Element Container on injected element!", this);
            element = value;
        }
    }
    [Tooltip("Triggered when the entity gains health")]
    public UnityEvent OnHealed;
    
    [Tooltip("Triggered when the entity loses health")]
    public UnityEvent OnDamaged;
    
    [Tooltip("Triggered when the entity's health reaches zero")]
    public UnityEvent OnKilled;

    [Header("Magic Damage Events")]
    [Tooltip("Called directly before damage is applied")]
    public UnityEvent<IHealth, DamageInfo> onPreMagicDamaged;
    
    [Tooltip("Called directly after damage was applied")]
    public UnityEvent<IHealth, DamageInfo> onPostMagicDamaged;
    
    
    public bool isAlive => CurrentValue > 0;

    private DamageInfo _damageInfo;
    
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
        
        if(prev > CurrentValue)
            OnDamaged?.Invoke();
        
        if(!isAlive) 
            OnKilled?.Invoke();
    }
    public void damageHealth(DamageInfo damageInfo)
    {
        if (isAlive == false) 
            return;
        
        onPreMagicDamaged?.Invoke(this, damageInfo);
        
        damageHealth(damageInfo.Damage);
        
        onPostMagicDamaged?.Invoke(this, damageInfo);
    }
    


}