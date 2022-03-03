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
        _state.BasicSpell.onCastStarted.AddListener(() =>
        {
            _anim.SetTrigger("cast");
            StartCoroutine(WaitForAnimationToCompleteCast(_state.BasicSpell));
        });
        _state.StrongSpell.onCastStarted.AddListener(() =>
        {
            _anim.Play("HCast");
            StartCoroutine(WaitForAnimationToCompleteCast(_state.StrongSpell));
        });
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