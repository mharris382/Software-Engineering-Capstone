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
        _state.Input.Jump = UnityEngine.Input.GetButtonDown("Jump");
        var x = UnityEngine.Input.GetAxis("Horizontal");
        var y = UnityEngine.Input.GetAxis("Vertical");

        _state.Input.MoveInput = new Vector2(x, y);

        _state.Input.Casting = UnityEngine.Input.GetButton("Fire1");
        _state.Input.CastDown = UnityEngine.Input.GetButtonDown("Fire1");
        _state.Input.CastUp = UnityEngine.Input.GetButtonUp("Fire1");
        
    }
}