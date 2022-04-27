using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(ParticleSystem))]
public class SimpleParticleAbsorbtion : MonoBehaviour
{
    [SerializeField]
    private Transform collisionPlane;
    [SerializeField]
    private Transform toPoint;
    public Transform To
    {
        set => toPoint = value;
    }

    [SerializeField]
    private Transform fromPoint;

    public Transform From
    {
        set => fromPoint = value;
    }
    
    private ParticleSystem _ps;

    private ParticleSystem ps
    {
        get
        {
            if (_ps == null)
            {
                _ps = GetComponent<ParticleSystem>();
            }
            return _ps;
        }
    }
    void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        if (toPoint == null || fromPoint == null) 
            return;

        var fromPos = fromPoint.position;
        var toPos = toPoint.position;
        
        transform.position = toPos;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        var shapeModule = ps.shape;
        shapeModule.position = fromPos - toPos;

        if (collisionPlane != null)
        {
            collisionPlane.transform.position = toPos;
            collisionPlane.up = fromPos - toPos;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Particle Collision Occured with: {other.name}!");
    }
}
