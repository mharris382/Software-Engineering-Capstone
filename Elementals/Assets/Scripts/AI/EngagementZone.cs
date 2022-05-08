using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [Serializable]
    public class EngagementZone
    {
        public PlayerDetectionTrigger triggerArea;
        public Vector2 planeOrigin;
        
        public ZoneType zoneType = ZoneType.HorizontalPlane;
        
        public enum ZoneType
        {
            Trigger,
            HorizontalPlane,
            VerticalPlane
        }

        public bool negativePlane = true;


        private int ticks = 0;
        private bool lastDetectedState;
        public event Action<bool> playerDetectedStateChanged;


        public bool IsTargetInZone => lastDetectedState && ticks > 0;

        public void AddListener(Action<bool> onDetectedPlayer) => playerDetectedStateChanged += onDetectedPlayer;
        public void RemoveListener(Action<bool> onDetectedPlayer) => playerDetectedStateChanged -= onDetectedPlayer;


        public void Reset()
        {
            ticks = 0;
        }
        public void Tick(Transform target)
        {
            var position = target.position;
            bool newDetectedState = CheckZone(position);
            
            if (lastDetectedState != newDetectedState || ticks==0)
            {
                lastDetectedState = newDetectedState;
                playerDetectedStateChanged?.Invoke(newDetectedState);
            }
            
            ticks++;
        }
        
        public bool CheckZone(Vector2 position)
        {
            switch (zoneType)
            {
                case ZoneType.Trigger:
                    return triggerArea.IsPlayerDetected;
                    break;
                case ZoneType.HorizontalPlane:
                    return CheckHorizontalPlane(position);
                    break;
                case ZoneType.VerticalPlane:
                    return CheckVerticalPlane(position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (HasTriggerZone())
                return triggerArea.IsPlayerDetected;

            return false;
        }

        

        private bool HasTriggerZone()
        {
            return  triggerArea != null && ((int) zoneType & (int)ZoneType.Trigger) != 0;
        }

        private bool HasHorizontalZone()
        {
            return ((int) zoneType & (int) ZoneType.HorizontalPlane) != 0;
        }
        private bool HasVerticalZone()
        {
            return ((int) zoneType & (int) ZoneType.VerticalPlane) != 0;
        }

        public bool CheckVerticalPlane(Vector2 position)
        {
            var center = planeOrigin;
            var diff = position - center;
            var dist = diff.y;
            if (negativePlane) dist *= -1;
            return dist > 0;
        }
        public bool CheckHorizontalPlane(Vector2 position)
        {
            var center = planeOrigin;
            var diff = position - center;
            var dist = diff.x;
            if (negativePlane) dist *= -1;
            return dist > 0;
        }

        #region [EDITOR ONLY]

#if UNITY_EDITOR
        
        public void DrawTestObject(Transform t)
        {
            var color = Color.red;
            switch (zoneType)
            {
                case ZoneType.Trigger:
                    break;
                case ZoneType.HorizontalPlane:
                    if (CheckHorizontalPlane(t.position)) color = Color.green;
                    break;
                case ZoneType.VerticalPlane:
                    if (CheckVerticalPlane(t.position)) color = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Gizmos.color = color;
            Gizmos.DrawSphere(t.position, 1);
        }

        public void DrawGizmos(Color c)
        {
            c.a = 0.5f;
            Gizmos.color = c;
            switch (zoneType)
            {
                case ZoneType.Trigger:
                    DrawTypeTrigger();
                    break;
                case ZoneType.HorizontalPlane:
                    DrawHorizontalEdge();
                    break;
                case ZoneType.VerticalPlane:
                    DrawVerticalEdge();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
        }

        private const float lineDist = 50; 
        void DrawVerticalEdge()
        {
            var direction = negativePlane ? Vector2.down : Vector2.up;
            var lineDir = direction;
            var orth = Vector2.Perpendicular(direction);

            var maxDist = 100;
            var extent = maxDist / 2f;
            var p0 = planeOrigin + (orth * extent);
            var p1 = planeOrigin - (orth * extent);
            Gizmos.DrawLine(p0, p1);
            var separation = 0.5f;
            var numLines = Mathf.FloorToInt(maxDist / separation);
            var offsetDir = orth.normalized;
            for (int i = 0; i < numLines; i++)
            {
                var offset = offsetDir * (i * separation);
                var offset2 = (offsetDir * ((i + 1) * separation)) + direction;
                var p2 = p1 + offset;
                var p3 = p1 + offset2;
                var dir = p3 - p2;
                dir.Normalize();
                Gizmos.DrawLine(p2, p3);
                var prevColor = Gizmos.color;
                var tmpColor = prevColor;
                tmpColor.a = 0.05f;
                Gizmos.color = tmpColor;
                Gizmos.DrawRay(p2, dir * lineDist);
                Gizmos.color = prevColor;
            }
        }

        void DrawHorizontalEdge()
        {
            var direction = negativePlane ? Vector2.left : Vector2.right;
            var lineDir = direction;
            var orth = Vector2.Perpendicular(direction);

            var maxDist = 100;
            var extent = maxDist / 2f;
            var p0 = planeOrigin + (orth * extent);
            var p1 = planeOrigin - (orth * extent);
            Gizmos.DrawLine(p0, p1);
            var separation = 0.5f;
            var numLines = Mathf.FloorToInt(maxDist / separation);
            var offsetDir = orth.normalized;
            for (int i = 0; i < numLines; i++)
            {
                var offset = offsetDir * (i * separation);
                var offset2 = (offsetDir * ((i + 1) * separation)) + direction;
                var p2 = p1 + offset;
                var p3 = p1 + offset2;
                var dir = p3 - p2;
                dir.Normalize();
                Gizmos.DrawLine(p2, p3);
                var prevColor = Gizmos.color;
                var tmpColor = prevColor;
                tmpColor.a = 0.05f;
                Gizmos.color = tmpColor;
                Gizmos.DrawRay(p2, dir * lineDist);
                Gizmos.color = prevColor;
            }
        }

        void DrawTypeTrigger()
        {
            if (triggerArea == null)
            {
                Debug.LogWarning("No Trigger Area assigned to TriggerType zone");
                return;
            }
            var col = triggerArea.GetComponent<Collider2D>();
            if (col is CircleCollider2D circle)
                DrawCircle(circle);
            else if (col is BoxCollider2D box)
                DrawBox(box);
            else if (col is PolygonCollider2D poly)
                DrawPolygon(poly);
            else if (col is CompositeCollider2D compositeCollider2D) DrawComposite(compositeCollider2D);
        }
        
        
        void DrawBox(BoxCollider2D boxCollider2D)
        {
            var center = (Vector2)boxCollider2D.transform.position + boxCollider2D.offset;
            Debug.Assert(boxCollider2D.transform.rotation == Quaternion.identity, "Cannot rotate box");
            var size = boxCollider2D.size * new Vector2(boxCollider2D.transform.lossyScale.x, boxCollider2D.transform.lossyScale.y);
            var extent = size / 2f;
            var min = center - extent;
            var max = center + extent;
            var p0 = new Vector2(min.x, min.y);
            var p1 = new Vector2(min.x, max.y);
            var p2 = new Vector2(max.x, max.y);
            var p3 = new Vector2(max.x, min.y);
            var p4= new Vector2(min.x, min.y);
            var points = new List<Vector2>(new[] {p0, p1, p2, p3, p4});
            for (int i = 1; i < points.Count; i++)
            {
                var pp0 = points[i - 1];
                var pp1 = points[i];
                Gizmos.DrawLine(pp0,pp1);
            }
        }
        void DrawCircle(CircleCollider2D circle)
        {
            var center = (Vector2)circle.transform.position + circle.offset;
            var radius = circle.radius;
            Gizmos.DrawWireSphere(center, radius);
        }
        void DrawPolygon(PolygonCollider2D polygonCollider2D)
        {
            var points = new List<Vector2>();
            polygonCollider2D.GetPath(0, points);
            for (int i = 1; i < points.Count; i++)
            {
                var p0 = points[i - 1];
                var p1 = points[i];
                Gizmos.DrawLine(p0,p1);
            }
            Gizmos.DrawLine(points[0], points[^1]);
        }
        void DrawComposite(CompositeCollider2D compositeCollider2D)
        {
            PhysicsShapeGroup2D shapeGroup2D = new PhysicsShapeGroup2D();
            compositeCollider2D.GetShapes(shapeGroup2D, 0);
            compositeCollider2D.geometryType = CompositeCollider2D.GeometryType.Polygons;
            var points = new List<Vector2>();
            var cnt = compositeCollider2D.GetPath(0, points);
            for (int i = 1; i < points.Count; i++)
            {
                var p0 = points[i - 1];
                var p1 = points[i];
                Gizmos.DrawLine(p0,p1);
            }
            Gizmos.DrawLine(points[0], points[^1]);
        }
#endif

        #endregion
    }
}