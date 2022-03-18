using System;
using UnityEngine;

namespace Damage
{
    public class DamageReceiver : MonoBehaviour
    {
        private void Awake()
        {
            var Health = GetComponentInChildren<HealthState>();
            DamageSystem.AddReceiver(gameObject, Health);
        }
        
    }
}