using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _anim;
    private CharacterState _state;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    void Start()
    {
        _anim = GetComponent<Animator>();
        _state = GetComponentInParent<CharacterState>();
    }

    
    void Update()
    {
        var moving = Mathf.Abs(_state.Movement.Velocity.x) > 0.1f;
        _anim.SetBool("moving", moving);
        _anim.SetBool("grounded", _state.Movement.IsGrounded);
        _state.IsInteracting = _anim.GetBool(IsInteracting);
        if (moving)
        {
            var yRotation = 0;
            if (_state.Movement.Velocity.x < 0)
            {
                yRotation = 180;
            }
            
            transform.rotation = Quaternion.Euler(0, yRotation , 0);
        }
    }

    public void SetCharacterIsInteracting(bool isInteracting)
    {
        _anim.SetBool(IsInteracting, isInteracting);
    }
}