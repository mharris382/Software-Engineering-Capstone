using UnityEngine;
using UnityEngine.Animations;

namespace Utilities.StateMachineBehaviours
{
    public class SetBoolParam : StateMachineBehaviour
    {
        [SerializeField] private string paramName = "IsInteracting";
        [SerializeField] private bool boolValue = true;

        [SerializeField] private bool resetOnExit;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            animator.SetBool(paramName, boolValue);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (resetOnExit)
            {
                animator.SetBool(paramName, !boolValue);
            }
        }
    }
}