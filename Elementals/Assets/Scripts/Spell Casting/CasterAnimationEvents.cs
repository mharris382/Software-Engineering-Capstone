using System;
using JetBrains.Annotations;
using UnityEngine;

public class CasterAnimationEvents : MonoBehaviour
{
    private CasterState _state;
    
    
    //called from animation event
    [UsedImplicitly]
    public void BasicCast()
    {
       // _state.BasicSpell.Trigger();
        TriggerSpell();
    }
    
    //called from animation event
    [UsedImplicitly]
    public void StrongCast()
    {
        //_state.StrongSpell.Trigger();
        TriggerSpell();
    }

    [UsedImplicitly]
    public void TriggerSpell()
    {
        _state.SpellCast.Trigger();
    }

    private void Awake()
    {
        _state = GetComponentInParent<CasterState>();
        if (_state == null) Debug.LogError("No Caster State found in parent of Cast Animation Events!", this);

    }
}