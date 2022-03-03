using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    
    [SerializeField]
    private float moveSpeed = 10f;
    private int _index = 0;
    private int _direction = 1;
    private Rigidbody2D _rb;
    private Vector3 Target => points[_index].position;

    [SerializeField] private float arriveRadius = 0.5f; 


    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    

    public void Update()
    {
        var direction = Target - transform.position;

        if (Vector2.Distance(Target, transform.position) <= arriveRadius)
        {
            IncrementTarget();
            return;
        }

        Vector2 movement = direction.normalized * moveSpeed / Time.deltaTime;
        _rb.velocity = movement;
    }
    
    
    private void IncrementTarget()
    {
        if ((_direction > 0 && _index == points.Length - 1) || (_direction < 0 && _index == 0))
        {
            _direction = _direction * -1;
        }
        _index += _direction;
    }

    
    public void OnDrawGizmos()
    {
        if (points.Length <= 1)
        {
            return;
        }
        
        Gizmos.color = Color.green;
        
        for (int i = 1; i < points.Length; i++)
        {
            var p0 = points[i - 1].position;
            var p1 = points[i].position;
            Gizmos.DrawLine(p0, p1);
        }
    }
    
    
}

