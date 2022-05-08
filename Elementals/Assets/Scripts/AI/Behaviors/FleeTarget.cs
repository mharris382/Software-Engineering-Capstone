using UnityEngine;

namespace AI.Behaviors
{
    public class FleeTarget : AIBehaviorBase
    
    {
        public AINavDestination moveDestination;
        public EnemyAIState aiState;

        public float safeDistance = 15f;

        public float predictionTime = 25f;
        public bool overrideSpeed = false;
        public float fleeSpeed = 10;


        private float MoveSpeed
        {
            set
            {
                
            }
        }
        public void Update()
        {
            var aiTarget = aiState.Target;
            if (aiTarget == null)
            {
                moveDestination.Destination = transform.position;
                return;
            }

            var predictVelocity = aiTarget.TargetVelocity * (predictionTime * Time.deltaTime);
            var predictPosition = aiTarget.TargetPosition + predictVelocity;
            Debug.DrawLine(aiTarget.TargetPosition, predictVelocity, Color.magenta);
            var aiPos = aiState.position;
            var aiPredictVelocity = aiState.velocity * (predictionTime * Time.deltaTime);
            var aiPredictPos = aiState.position + aiPredictVelocity;
            var dist = Vector2.Distance(aiPredictPos, predictPosition);
            if (dist > safeDistance)
            {
                moveDestination.Destination = transform.position;
            }
            else
            {
                var fleeDirection = aiPredictPos - predictPosition;
                fleeDirection.Normalize();
                var sp = overrideSpeed ? fleeSpeed : aiState.speed;
                MoveSpeed = sp;
                var fleeVelocity = fleeDirection * sp;
                Debug.DrawLine(aiState.position, fleeVelocity, Color.red);
                moveDestination.Destination = aiState.position + (fleeVelocity * Time.deltaTime);
            }
        }

        public override Vector2 GetMovementDestination(IAIState aiState)
        {
            var aiTarget = aiState.Target;
            if (aiTarget == null)
            {
                return transform.position;
            }

            var predictVelocity = aiTarget.TargetVelocity * (predictionTime * Time.deltaTime);
            var predictPosition = aiTarget.TargetPosition + predictVelocity;
            Debug.DrawLine(aiTarget.TargetPosition, predictVelocity, Color.magenta);
            var aiPos = aiState.position;
            var aiPredictVelocity = aiState.velocity * (predictionTime * Time.deltaTime);
            var aiPredictPos = aiState.position + aiPredictVelocity;
            var dist = Vector2.Distance(aiPredictPos, predictPosition);
            if (dist > safeDistance)
            {
                return transform.position;
            }
            else
            {
                var fleeDirection = aiPredictPos - predictPosition;
                fleeDirection.Normalize();
                var sp = overrideSpeed ? fleeSpeed : aiState.speed;
                MoveSpeed = sp;
                var fleeVelocity = fleeDirection * sp;
                Debug.DrawLine(aiState.position, fleeVelocity, Color.red);
                return aiState.position + (fleeVelocity * Time.deltaTime);
            }
        }
    }
}