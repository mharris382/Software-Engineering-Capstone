using UnityEngine;

namespace Utilities.StateMachineBehaviours
{
    public class RandomizeIntParam : StateMachineBehaviour
    {
        public string paramName = "";
        public int rangeMin = 0;
        public int rangeMax = 3;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger(paramName, UnityEngine.Random.Range(rangeMin, rangeMax));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}