using System;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
#if UNITY_EDITOR

    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavigationFollowerEnemyAI))]
    public class NavigationFollowerEnemyAIEditor : Editor
    {
        private void OnSceneGUI()
        {
            var follower = target as NavigationFollowerEnemyAI;
            if (follower == null) return;

           
            DrawHandlesForAI(follower);
            
        }

        private void DrawHandlesForAI(NavigationFollowerEnemyAI follower, bool multiTarget =false)
        {
            Handles.color = Color.green;
            if ( Event.current.control) {
                SetRadiusCenter(follower);
            }
            else if (!multiTarget && Event.current.shift) {
                ClearRadiusCenter(follower);
            }

            float radius = follower.Radius;
            var center = follower.RadiusCenter.position;
            float newRadius = Handles.RadiusHandle(Quaternion.identity, center, radius);
            if (Math.Abs(newRadius - radius) > Mathf.Epsilon) {
                follower.Radius = newRadius;
                Undo.RecordObject(follower, "Change radius");
            }
        }

        private void ClearRadiusCenter(NavigationFollowerEnemyAI follower)
        {
            if (follower.HasRadiusCenter()) {
                var  mousePos = Event.current.mousePosition;
                var scenePos = HandleUtility.GUIPointToWorldRay(mousePos).origin;
                var c = Color.red;
                var pos = follower.RadiusCenter.position;
                bool isSelecting = Vector2.Distance(scenePos, pos) < 1f;
                c.a = isSelecting ? 0.5f : 0.2f;
                if (Event.current.type == EventType.MouseDown&&isSelecting) {
                    Undo.RecordObject(follower, "Clear radius center");
                    follower.ClearRadiusCenter();
                    Undo.DestroyObjectImmediate(follower.RadiusCenter.gameObject);
                }
            }
        }

        private void SetRadiusCenter(NavigationFollowerEnemyAI follower)
        {
            var  mousePos = Event.current.mousePosition;
            var scenePos = HandleUtility.GUIPointToWorldRay(mousePos).origin;
            var c = Color.green;
            if (!follower.HasRadiusCenter()) {
          
                c.a = 0.5f;
                Handles.color = c;
               
                Handles.DrawSolidDisc(scenePos, Vector3.forward, 0.1f);
                if (Event.current.type == EventType.MouseDown) {
                    c.a = 1;
                    Handles.color = c;
                    var go = new GameObject("RadiusCenter");
                    go.transform.position = scenePos;
                    follower.AssignRadiusCenter(go.transform);
                    Undo.RegisterCreatedObjectUndo(go, "Assign radius center");
                }
            }
            else {
                c.a = 1;
                Handles.DrawSolidDisc(scenePos, Vector3.forward, 0.1f);
                var t = follower.RadiusCenter;
                t.position = scenePos;
                Undo.RecordObject(follower.RadiusCenter, "Change radius center");
            }
        }
    }
#endif
}