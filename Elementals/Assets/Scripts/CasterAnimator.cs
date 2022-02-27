using UnityEngine;

public class CasterAnimator : MonoBehaviour
{
    private Animator _anim;
    private CasterState _state;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _state = GetComponentInParent<CasterState>();
    }
    
    void Update()
    {
        var currentState = _anim.GetCurrentAnimatorStateInfo(0);
        _state.AllowCast = !currentState.IsTag("Cast");
        if (_state.Casting && _state.AllowCast)
        {
            _anim.SetTrigger("cast");
        }
    }
}