using System;
using UnityEngine;
using UnityEngine.Events;

namespace AI
{
   
    /// <summary>
    /// the built-in navigation AI in AStar PathfindingProject AIPath, does not support dynamic rigidbodies.  As a workaround
    /// for that this class intercepts the desired destination, and instead of steering in that direction it indirectly tells
    /// the navigation agent to find a path to that destination.  Then it will steer towards the navigation agent's current position.
    /// essentially following the path by following the agent who is following the path.
    /// <para>It is important that the navTarget transform is also referenced by the AI destination setter component.</para>
    /// </summary>
    [RequireComponent(typeof(AINavDestination))]
    public class NavigationFollowerEnemyAI : StupidEnemyAI
    {
        public Transform followTarget;


        private AINavDestination _navDestination;
        
        [SerializeField]
        private float acceptableDistanceToFollowTarget = 0.01f;
        
        public LayerMask blockingMask;

        private Vector2 _lastLosPosition;
        
        protected override void Awake()
        {
            base.Awake();
            _navDestination = GetComponent<AINavDestination>();

            
        }

        protected override void Update()
        {
            base.Update();
            if (acceptableDistanceToFollowTarget < 0) return;
            var pos = transform.position;
            var followTargetPos = followTarget.position;
            var sqrDistanceToFollowTarget = (followTargetPos - pos).sqrMagnitude;
            if (sqrDistanceToFollowTarget > acceptableDistanceToFollowTarget * acceptableDistanceToFollowTarget) {
                var los = Physics2D.Linecast(pos, followTargetPos, blockingMask);
                if (!los) {
                    _lastLosPosition = followTargetPos;
                }
                else {
                    _navDestination.Destination = _lastLosPosition;
                }
            }
            else {
                _lastLosPosition = followTargetPos;
            }
        }


        protected override Vector2 GetSteeringDestination()
        {
            var desiredDest = base.GetSteeringDestination();
            _navDestination.Destination = desiredDest;
            return followTarget.position;
        }

        private void OnDrawGizmos()
        {
            if (_navDestination != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_navDestination.Destination, 0.1f);
                Gizmos.DrawLine(_navDestination.Destination, transform.position);
            }

            if (acceptableDistanceToFollowTarget > 0) {
                Gizmos.color = Color.Lerp(Color.green, Color.white, 0.6f);
                Gizmos.DrawWireSphere(transform.position, acceptableDistanceToFollowTarget);
            }
            
        }
    }
}