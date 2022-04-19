using UnityEngine;
using UnityEngine.Animations;

namespace Utilities.StateMachineBehaviours
{
    public class SetIntParam : StateMachineBehaviour
    {
        [SerializeField] private string paramName = "IsInteracting";
        [SerializeField] private int intValue = 0;

        [SerializeField] private bool resetOnExit;


        private int _prevValue;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            _prevValue = animator.GetInteger(paramName);
            animator.SetInteger(paramName, intValue);
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (resetOnExit)
            {
                animator.SetInteger(paramName, intValue);
            }
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
