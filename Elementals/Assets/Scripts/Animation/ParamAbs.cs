using UnityEngine;



public class ParamAbs : StateMachineBehaviour
{
    [Tooltip("the name of the float parameter to read the value from")]
    public string inputParameterName = "MoveX";

    [Tooltip("the name of the float parameter to be written to")]
    public string outputParameterName = "SpeedX";
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float inputValue = animator.GetFloat(inputParameterName);
        float outputValue = Mathf.Abs(inputValue);
        animator.SetFloat(outputParameterName, outputValue);
    }

}