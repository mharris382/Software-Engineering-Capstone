

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public ParticleSystem sys;

    private HealthState _health;
    HealthState Health
    {
        get
        {
            if(_health == null)
                _health = GetComponentInChildren<HealthState>();
            return _health;
        }
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_health == null)
            _health = GetComponentInChildren<HealthState>();
        if (_health != null && !_health.isAlive)
        {
            Debug.Log("You Are Dead");
            Destroy (this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_health == null)
        {
            Debug.LogWarning("Missing Health Component!", this);
            return;
        }
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
