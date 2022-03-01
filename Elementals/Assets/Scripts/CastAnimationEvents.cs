using System;
using JetBrains.Annotations;
using UnityEngine;

public class CastAnimationEvents : MonoBehaviour
{
    private CasterState _state;
    
    
    //called from animation event
    [UsedImplicitly]
    public void BasicCast()
    {
        _state.BasicSpell.onCastTriggered?.Invoke();
    }
    
    //called from animation event
    [UsedImplicitly]
    public void StrongCast()
    {
        _state.StrongSpell.onCastTriggered?.Invoke();
    }

    private void Awake()
    {
        _state = GetComponentInParent<CasterState>();
        if (_state == null) Debug.LogError("No Caster State found in parent of Cast Animation Events!", this);

    }
}