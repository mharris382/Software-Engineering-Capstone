using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public InputState Input { get; private set; }
    public MovementState Movement { get; private set; }


    private void Awake()
    {
        Input = new InputState();
        Movement = new MovementState();
    }
}