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
    
    public bool grounded;


    private bool _grounded;
    private float _timeLastGrounded;
    
    [System.Serializable]
    private class CheckPoint : IComparable<CheckPoint>
    {
        [SerializeField] private  Vector2 offset;
        [SerializeField] private  float distance;
        
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

#if UNITY_EDITOR
        public void DrawGizmos(Transform owner)
        {
            Gizmos.color = Color.green;
            var p0 = (Vector2)owner.position + offset;
            var p1 = p0 + (Vector2.down * distance);
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
    }
#endif
}
