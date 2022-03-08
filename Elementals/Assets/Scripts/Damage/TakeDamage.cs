using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private HealthState _health;
    public ParticleSystem sys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _health = GetComponentInChildren<HealthState>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("Ouch that hurt!");
            _health.damageHealth(1);
        }
        if (!_health.isAlive)
        {
            Debug.Log("You Are Dead");
            Destroy (this.gameObject);
        }
    }

    // Want to add a destroyed effect like particles to this
}
