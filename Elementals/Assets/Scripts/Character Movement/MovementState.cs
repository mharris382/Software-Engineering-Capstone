using UnityEngine;

public class MovementState
{
    public Vector2 Velocity { get; set; }
    public bool IsGrounded { get; set; }
    
    public bool IsJumping { get; set; }
    
    public float HorizontalMovement
    {
        get => Velocity.x;
        set => Velocity = new Vector2(value, Velocity.y);
    }

    public float VerticalMovement
    {
        get => Velocity.y;
        set => Velocity = new Vector2(Velocity.x, value);
    }

    public Vector2 AnimatorDelta
    {
        get;
        set;
    }
    public Vector2 AnimatorAccel
    {
        get;
        set;
    }
    
}