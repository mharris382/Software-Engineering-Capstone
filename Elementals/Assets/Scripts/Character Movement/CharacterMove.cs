using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(CharacterState), typeof(GroundCheck))]
public class CharacterMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CharacterState _state;
    public float jumpTime = 1;
    public float moveSpeed = 10f;
    public float jumpVert = 10f;

    private GroundCheck _groundCheck;

    private CharacterState State
    {
        get
        {
            if (_state == null) _state = GetComponent<CharacterState>();
            return _state;
        }
    }

    private float _jumpTimeCounter;

    private bool IsGrounded => State.Movement.IsGrounded;

    private bool WantsToKeepJumping => State.MovementInput.TryingToJump;
    private bool WantsToStartJump => State.MovementInput.Jump;
    
    private bool IsJumping
    {
        get => _state.Movement.IsJumping;
        set => _state.Movement.IsJumping = value;
    }

    private float JumpTimeCounter
    {
        get => _jumpTimeCounter;
        set => _jumpTimeCounter = value;
    }

    void Start()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _rb = GetComponent<Rigidbody2D>();
        _state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        _state.Movement.IsGrounded = _groundCheck.grounded;
        _state.Movement.HorizontalMovement = _state.MovementInput.MoveInput.x * _state.CheckForSpeedModifiers(moveSpeed);

        if (IsMovementPhysicsRestricted())
        {
            IsJumping = false;
            return;
        }

        

        if (IsGrounded && WantsToStartJump)
        {
            IsJumping = true;
            JumpTimeCounter = jumpTime;
            ApplyJumpVelocity();
        }  

        HandleJumping();
    }

    private void HandleJumping()
    {
        if (IsJumping && WantsToKeepJumping)
        {
            if (JumpTimeCounter > 0)
            {
                ApplyJumpVelocity();
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
        }
        else if (IsJumping)
        {
            IsJumping = false;
        }
    }

    private void ApplyJumpVelocity() => State.Movement.VerticalMovement = jumpVert;

    // Based on Physics Updates
    private void FixedUpdate()
    {
        void CompensateForMovingPlatforms()
        {
            if (State.MovingGround.Count > 0)
            {
                Vector2 velocity = Vector2.zero;
                for (int i = 0; i < State.MovingGround.Count; i++)
                {
                    velocity += State.MovingGround[i].Velocity;
                }

                _rb.velocity = velocity + _rb.velocity;
            }
        }
        
        if(!IsJumping && IsGrounded)
            CompensateForMovingPlatforms();
        
        if (!_state.IsInteracting)
        {
            NormalMovementFixed();
            return;
        }
        
        switch (_state.PhysicsMode)
        {
            case InteractionPhysicsMode.Default:
                _rb.velocity += State.Movement.AnimatorAccel;
                break;
            case InteractionPhysicsMode.Mixed:
                _rb.velocity += State.Movement.AnimatorAccel + (Physics2D.gravity * Time.fixedDeltaTime);
                break;
            case InteractionPhysicsMode.FullPhysics:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void NormalMovementFixed()
    {
        if (IsMovementPhysicsRestricted())
        {
            return;
        }
        _rb.velocity = IsJumping ? State.Movement.Velocity : new Vector2(State.Movement.HorizontalMovement, _rb.velocity.y);

    }

    private bool IsMovementPhysicsRestricted()
    {
        return (_state.MovementRestrictions & MovementRestrictions.NoMovementPhysics) != 0;
    }
    
    private bool IsMovementInputRestricted()
    {
        return (_state.MovementRestrictions & MovementRestrictions.NoMovementInput) != 0;
    }
}
