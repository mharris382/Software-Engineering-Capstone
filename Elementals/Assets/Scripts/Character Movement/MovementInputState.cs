using UnityEngine;

public class MovementInputState
{
    public Vector2 MoveInput { get; set; }
    public bool Jump { get; set; }
    public bool TryingToJump { get; set; }

    public bool IsDoneJumping { get; set; }

}