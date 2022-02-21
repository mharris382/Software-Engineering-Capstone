using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState
{
    public Vector2 MoveInput { get; set; }
    public bool Jump { get; set; }
}

public class MovementState
{
    public Vector2 Velocity { get; set; }
    public bool IsGrounded { get; set; }
    
    public float HorizontalMovement
    {
        get => Velocity.x;
        set => Velocity = new Vector2(value, Velocity.y);
    }
}

[RequireComponent(typeof(CharacterState))]
public class CharacterMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CharacterState _state;
    public float moveSpeed = 10f;
    public float jumpVert = 10f;
    public LayerMask groundMask;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _state = GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        _state.Movement.HorizontalMovement = _state.Input.MoveInput.x * moveSpeed;
        var hit = Physics2D.Raycast(transform.position, Vector2.down, distance: 0.5f, groundMask);
        _state.Movement.IsGrounded = hit;

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
