using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("Transform that implicitly communicates which direction the entity is traveling")]
    [SerializeField] private Transform facingReference;

    [SerializeField] private LayerMask mask = 1;
    [SerializeField] private CheckPoint[] checkPoints = new[] { new CheckPoint(0,0), new CheckPoint(-0.5f, 0),new CheckPoint(0.5f,0),   };
    
    [SerializeField] private float groundedDelay = 0.05f;
    [SerializeField] private CheckPoint rightCorner;
    [SerializeField] private CheckPoint leftCorner;
    public bool grounded;


    private bool _grounded;
    private float _timeLastGrounded;
    
    [System.Serializable]
    private class CheckPoint : IComparable<CheckPoint>
    {
        [SerializeField] private  Vector2 offset;
        [SerializeField] private  float distance = 1;
        
        public RaycastHit2D Hit { get; private set; }
        public float TimeLastChecked { get; private set; }
        public float TimeLastGrounded { get; private set; }
        public CheckPoint(float x, float y, float distance=1)
        {
            offset = new Vector2(x,y);
            this.distance = distance;
        }

        public int CompareTo(CheckPoint other)
        {
            return other.offset.x > offset.x ? 1 : -1;
        }


        public bool Check(Transform owner, LayerMask mask)
        {
            TimeLastChecked = Time.time;
            var pos = (Vector2)owner.position + offset;
            var hit = Physics2D.Raycast(pos, Vector2.down, distance, mask);
            if (hit)
            {
                TimeLastGrounded = Time.time;
            }
            return hit;
            
            
        }
        public bool Check(Transform owner, Vector2 direction, LayerMask mask)
        {
            TimeLastChecked = Time.time;
            var pos = (Vector2)owner.position + offset;
            var hit = Physics2D.Raycast(pos, direction, distance, mask);
            if (hit)
            {
                TimeLastGrounded = Time.time;
            }
            return hit;
        }

#if UNITY_EDITOR
        public void DrawGizmos(Transform owner)
        {
            Gizmos.color = Color.green;
            var p0 = (Vector2)owner.position + offset;
            var p1 = p0 + (Vector2.down * distance);
            Gizmos.DrawLine(p0, p1);
        }
        public void DrawGizmos(Transform owner, Color color)
        {
            Gizmos.color = color;
            var p0 = (Vector2)owner.position + offset;
            var p1 = p0 + (Vector2.down * distance);
            Gizmos.DrawLine(p0, p1);
        }
        
        public void DrawGizmos(Transform owner, Vector2 direction, Color color)
        {
            Gizmos.color = color;
            var p0 = (Vector2)owner.position + offset;
            var p1 = p0 + (direction.normalized * distance);
            Gizmos.DrawLine(p0, p1);
        }
#endif
    }


    private void Awake()
    {
        var lop = new List<CheckPoint>(checkPoints);
        lop.Sort();
        checkPoints = lop.ToArray();

        if (facingReference == null) facingReference = transform;
    }

    

    private void Update()
    {
        var travelingRight = facingReference.right.x > 0;
        int found = 0;
        for (int i = 0; i < checkPoints.Length; i++)
        {
            var i2 = travelingRight ? i : (checkPoints.Length - 1) - i;//check from right to left if traveling right
            var pnt = checkPoints[i2];
            if (pnt.Check(transform, mask)) found++;
        }
        _grounded = found > 0;
        if (_grounded)
        {
            grounded = true;
            return;
        }
        var time = float.MaxValue;
        foreach (var point in checkPoints)
        {
            if ( point.TimeLastGrounded < time)
            {
                time = point.TimeLastGrounded;
            }
        }

        grounded = Time.time - time < groundedDelay;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (var point in checkPoints)
        {
            point.DrawGizmos(transform);
        }
        rightCorner.DrawGizmos(transform, Color.cyan);
        rightCorner.DrawGizmos(transform, Vector2.right, Color.cyan);
        leftCorner.DrawGizmos(transform, Color.cyan);
        leftCorner.DrawGizmos(transform, Vector2.left, Color.cyan);
    }
#endif


    public void CheckForSlope(bool movingRight, ref Vector2 velocity)
    {
        if (velocity.y > 0)
        {
            return;
        }

        CheckPoint pnt = movingRight ? rightCorner : leftCorner;
        Vector2 dir = movingRight ? Vector2.right : Vector2.left;
        if (pnt.Check(transform, mask) || pnt.Check(transform, dir, mask))
        {
            //check the hit for a slope
            var hit = pnt.Hit;
            var angle = Vector2.Angle(Vector2.up, hit.normal);
            if (angle <= 0.001f)
            {
                //not on a slope
            }
            else
            {
                var speed = velocity.magnitude;
                var direction = Vector3.Cross(hit.normal, Vector3.forward);
                velocity = direction * speed;
            }
        }
    }
}
