using UnityEngine;

namespace AI.Behaviors
{
    public class MoveToTargetBehavior : AIBehaviorBase
    {
        public bool useAiTarget = true;
        public Vector2 fallbackPosition;
        public bool useStartPositionAsFallback;
        public bool hideTarget;
        public Transform target;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private EnemyAIState _enemy;

        private EnemyAIState enemy
        {
            get
            {
                if (_enemy == null)
                {
                    _enemy = GetComponentInParent<EnemyAIState>();
                    Debug.Assert(_enemy != null);
                }
                return _enemy;
            }
        }
            
        private void Start()
        {
            if (useStartPositionAsFallback)
            {
                fallbackPosition = transform.position;
            }
        }

        public override Vector2 GetMovementDestination(IAIState aiState)
        {
            if (useAiTarget && enemy.Target != null)
            {
                var aiTarget = enemy.Target;
                return !aiTarget.HasTarget ? fallbackPosition : aiTarget.TargetPosition;
            }
            return (Target == null || hideTarget) ? fallbackPosition : Target.position;
        }
    }

    public class PursueTargetBehavior : MoveToTargetBehavior
    {
        public float predictionTime = 25f;
        public bool overrideSpeed = false;
        public float fleeSpeed = 10;

        public override Vector2 GetMovementDestination(IAIState aiState)
        {
            var seekDest = base.GetMovementDestination(aiState);
            var aiTarget = aiState.Target;
            if (aiTarget != null)
            {
                var vel = aiTarget.TargetVelocity;
            }
            return seekDest;
        }
    }
}