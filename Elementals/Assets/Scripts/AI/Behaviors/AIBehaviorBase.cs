using System;
using UnityEngine;

namespace AI.Behaviors
{
    public abstract class AIBehaviorBase : MonoBehaviour
    {

        public EnemyAIState enemyAIState;
        public AINavDestination navDestination;

        private void Update()
        {
            navDestination.Destination =  GetMovementDestination(enemyAIState);
        }

        public abstract Vector2 GetMovementDestination(IAIState aiState);
    }
}