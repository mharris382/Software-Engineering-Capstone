using System;
using Spell_Casting.Spells;
using UnityEngine;

/// <summary>
/// the caster controller is responsible for reading from the caster input and updating the cast state based on the input
/// <para>this class is the bridge between what the "caster wants to do" and what the "caster can do"</para>
/// <para>this class will only interact with data. Essentially acting as the caster's data processor.</para> 
/// </summary>
[RequireComponent(typeof(CasterState))]
public class CasterController : MonoBehaviour
{
    private CasterState _state;
    public ElementContainer playerElement;
    private void Awake()
    {
        _state = GetComponent<CasterState>();
    }

    private bool isCastingStrongSpell, isCastingBasicSpell;

    
    private void Update()
    {
        isCastingStrongSpell = _state.StrongSpell.IsCasting;  
        isCastingBasicSpell = _state.BasicSpell.IsCasting;
        bool isCasting = isCastingBasicSpell || isCastingStrongSpell;
      
        _state.Gathering = _state.input.IsGatheringHeld && !isCasting;
        bool canStartNewCast = !isCasting && !_state.Gathering;
        
        if (canStartNewCast)
        {
            if (_state.input.IsCastBasicDown)
            {
                _state.SpellCast.Start($"{SpellNames.FastAttackSpell}_{playerElement.Element.ToString()}");
            }
            else if (_state.input.IsCastStrongDown)
            {
                _state.SpellCast.Start($"{SpellNames.StrongAttackSpell}_{playerElement.Element.ToString()}");
            }
        }
    }
}