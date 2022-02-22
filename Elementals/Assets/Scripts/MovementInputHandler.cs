﻿using System;
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
        _state.Input.Jump = Input.GetButton("Jump");
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        _state.Input.MoveInput = new Vector2(x, y);
    }
}