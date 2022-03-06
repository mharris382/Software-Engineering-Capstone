using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RigidbodyProjectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _coll;

    public Rigidbody2D Rb => _rb;
    public Collider2D Coll => _coll;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }
}
