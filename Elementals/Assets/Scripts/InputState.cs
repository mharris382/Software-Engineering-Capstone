using UnityEngine;

public class InputState
{
    public Vector2 MoveInput { get; set; }
    public bool Jump { get; set; }
    public bool CastDown { get; set; }
    public bool Casting { get; set; }
    public bool CastUp { get; set; }
    
    public bool StrongCastDown { get; set; }
}