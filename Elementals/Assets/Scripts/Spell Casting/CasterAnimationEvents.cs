using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CasterAnimationEvents : MonoBehaviour
{
    private CasterState _state;
    private Animator _anim;
    [SerializeField]
    [Tooltip("General Purpose unity event for adding callbacks onto the animation event")]
    private UnityEvent onSpellEvent;

    private void Awake()
    {
        _state = GetComponentInParent<CasterState>();
        if (_state == null) Debug.LogError("No Caster State found in parent of Cast Animation Events!", this);
        _anim = GetComponent<Animator>();

    }
    
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
        onSpellEvent?.Invoke();
    }


}