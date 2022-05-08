using UnityEngine;

namespace AI
{
    public class AICirleMoveArea : AIMoveAreaBase
    {
        public float circleRadius = 25f;
        public override Vector2 GetAIMovementDestination(IAIState state, float dt)
        {
            Vector2 center = transform.position;
            var aiPosition = state.position;
            var sqrRadius = circleRadius * circleRadius;
            var diff = aiPosition - center;
            bool isInsideRadius = diff.sqrMagnitude < sqrRadius;
            if (isInsideRadius)
            {
                var vel = state.velocity;
                if (vel.sqrMagnitude < 0.01f)
                    vel = (center - state.position);
                vel.Normalize();
                var rot = Quaternion.Euler(0, 0,  dt);
                vel *= state.speed * dt;
                vel = rot * vel;
                return state.position + vel;
            }
            var dir = -diff.normalized;
            return dir * (dt * state.speed);
        }

        private void OnDrawGizmos()
        {
            var color = Color.blue;
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, circleRadius);
        }
    }
    
    
    
}