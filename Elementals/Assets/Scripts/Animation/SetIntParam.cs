using UnityEngine;

namespace Animation
{
    public class SetIntParam : StateMachineBehaviour
    {
        public string paramName = "combo";
        public int value;
        public SetMode mode;

        public enum SetMode
        {
            OnEnter,
            OnExit
        }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (mode == SetMode.OnEnter)
            {
                animator.SetInteger(paramName, value);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (mode == SetMode.OnExit)
            {
                animator.SetInteger(paramName, value);
            }
        }
    }
}