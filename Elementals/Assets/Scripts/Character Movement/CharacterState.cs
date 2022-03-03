using System;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public MovementInputState MovementInput { get; private set; }
    public MovementState Movement { get; private set; }


    private void Awake()
    {
        MovementInput = new MovementInputState();
        Movement = new MovementState();
    }
}