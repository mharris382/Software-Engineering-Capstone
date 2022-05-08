using Elements.Utilities;
using UnityEngine;

namespace AI
{
    public class AITestTarget : MonoBehaviour, IAITarget
    {
        public Transform targetTransform;
        public bool hasTarget = true; 
        public bool HasTarget => hasTarget;
        public float speed = 5;
        public Vector2 targetPosition = Vector2.right * 100;
        public Element targetElement = Element.Fire;
        public Vector2 TargetPosition => (Vector2)target.position + targetPosition;
        public Vector2 TargetVelocity => target.right * speed;
        public Element TargetElement => targetElement;

        public Transform target => targetTransform == null ? transform : targetTransform;

        private void OnDrawGizmos()
        {
            var pos = TargetPosition;
            var vel = TargetVelocity;
            var color = ElementColorPalettes.GetColor(targetElement);
            color.a = hasTarget ? 0.8f : 0.2f;
            Gizmos.color = color;
            Gizmos.DrawSphere(pos, 0.25f);
            Gizmos.DrawRay(pos, vel);

        }
    }
}