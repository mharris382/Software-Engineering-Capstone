using System;
using System.Collections;
using UnityEngine;
public class CasterAnimator : MonoBehaviour
{
    private Animator _anim;
    private CasterState _state;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _state = GetComponentInParent<CasterState>();
        _state.BasicSpell.onCastStarted.AddListener(str =>
        {
            _anim.SetTrigger("cast");
            StartCoroutine(WaitForAnimationToCompleteCast(_state.BasicSpell));
        });
        _state.StrongSpell.onCastStarted.AddListener(str =>
        {
            _anim.Play("HCast");
            StartCoroutine(WaitForAnimationToCompleteCast(_state.StrongSpell));
        });
        _state.SpellCast.onCastStarted.AddListener(StartSpellCastAnimation);
    }

    void StartSpellCastAnimation(string spellName)
    {
        if (spellName.Contains('_'))
        {
            var animName = spellName.Remove(spellName.IndexOf('_'));
            Debug.Log($"Starting Animation named {animName}");
            _anim.Play(animName);
            StartCoroutine(WaitForAnimationToCompleteCast(_state.SpellCast));
        }
    }

    private IEnumerator WaitForAnimationToCompleteCast(CastEvents spell)
    {
        yield return new WaitForSeconds(0.15f);
        while (IsInCastingAnimationState())
        {
            yield return null;
        }
        spell.Finish();
    }

    private bool IsInCastingAnimationState()
    {
        var currentState = _anim.GetCurrentAnimatorStateInfo(0);
        return currentState.IsTag("Cast");
    }

    private void Update()
    {
        var currentState = _anim.GetCurrentAnimatorStateInfo(0);
        _state.AllowCast = !currentState.IsTag("Cast");
        if ( _state.AllowCast)
        {
            if(_state.Casting)
                _anim.SetTrigger("cast");
            else if(_state.CastingStrong)
                _anim.Play("HCast");
        }
    }


   
}


