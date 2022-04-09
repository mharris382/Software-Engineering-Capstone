using System;
using System.Collections;
using System.Collections.Generic;
using Damage;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    private HealthState _health;
    public float amount = 5.0f;

    private void Awake()
    {
        _health = GetComponent<HealthState>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Health"))
        {
            Debug.Log("Health Pickup Collided");
            _health.healHealth(amount);
            Destroy(col.gameObject);
        }
    }

}