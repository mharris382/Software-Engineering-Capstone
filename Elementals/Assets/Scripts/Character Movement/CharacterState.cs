using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public MovementInputState MovementInput { get; private set; }
    public MovementState Movement { get; private set; }

    public MovementRestrictions MovementRestrictions { get; set; }
    public List<IMovingGround> MovingGround { get; } = new List<IMovingGround>();
    public bool IsInteracting { get; set; }

    private MoveSpeed _speed;

    public float CheckForSpeedModifiers(float baseSpeed) => _speed == null ? baseSpeed : _speed.GetModifiedSpeed(baseSpeed);

    private void Awake()
    {
        _speed = GetComponent<MoveSpeed>();
        
        MovementInput = new MovementInputState();
        Movement = new MovementState();
    }
}

public interface IMovingGround
{
    public Vector2 Velocity { get; set; }
}