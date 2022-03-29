using System;
using UnityEngine;

public class MovementInputHandler : MonoBehaviour
{
    private CharacterState _state;

    private void Start()
    {
        _state = GetComponent<CharacterState>();
    }

    private void Update()
    {
       _state.MovementInput.Jump = Input.GetButtonDown("Jump");
        _state.MovementInput.TryingToJump = Input.GetButton("Jump");
        _state.MovementInput.IsDoneJumping = Input.GetButtonUp("Jump");
        
        var x = UnityEngine.Input.GetAxisRaw("Horizontal");
        var y = UnityEngine.Input.GetAxisRaw("Vertical");
        _state.MovementInput.MoveInput = new Vector2(x, y);

    }
}