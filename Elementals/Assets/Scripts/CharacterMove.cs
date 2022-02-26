using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState), typeof(GroundCheck))]
public class CharacterMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CharacterState _state;
    public float moveSpeed = 10f;
    public float jumpVert = 10f;

    private GroundCheck _groundCheck;

    void Start()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _rb = GetComponent<Rigidbody2D>();
        _state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        _state.Movement.HorizontalMovement = _state.Input.MoveInput.x * moveSpeed;
        _state.Movement.IsGrounded = _groundCheck.grounded;
        if (_state.Movement.IsGrounded && _state.Input.Jump) 
        {
            _rb.AddForce(Vector2.up * jumpVert, ForceMode2D.Impulse);
        }
    }

    // Based on Physics Updates
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_state.Movement.HorizontalMovement, _rb.velocity.y);
    }
}
