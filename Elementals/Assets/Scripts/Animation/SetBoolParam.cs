using UnityEngine;

namespace Animation
{

    public class SetBoolParam : StateMachineBehaviour
    {

        [SerializeField]
        private BoolValue[] values;
        
        [System.Serializable]
        public class BoolValue
        {
            public string parameterName;
            public bool status;
            public bool resetOnExit;
        }
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var boolValue in values)
            {
                animator.SetBool(boolValue.parameterName, boolValue.status);
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var boolValue in values)
            {
                if(boolValue.resetOnExit)
                    animator.SetBool(boolValue.parameterName, !boolValue.status);
            }
        }

        
    }

}