using System;
using UnityEngine;

namespace AI.Behaviors
{
    public class SeekTarget : MonoBehaviour
    {
        public AINavDestination moveDestination;
        public EnemyAIState aiState;


        public void Update()
        {
            var target = aiState.Target;
            if (target == null)
            {
                moveDestination.Destination = transform.position;
                return;
            }
            moveDestination.Destination = target.TargetPosition;
        }
    }
}