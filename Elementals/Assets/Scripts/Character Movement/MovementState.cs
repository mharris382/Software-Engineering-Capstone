using UnityEngine;

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