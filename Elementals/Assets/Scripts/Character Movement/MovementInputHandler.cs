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
        _state.MovementInput.Jump = UnityEngine.Input.GetButtonDown("Jump");
        var x = UnityEngine.Input.GetAxis("Horizontal");
        var y = UnityEngine.Input.GetAxis("Vertical");

        _state.MovementInput.MoveInput = new Vector2(x, y);

    }
}