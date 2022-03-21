using System;
using UnityEngine;

namespace Damage
{
    public class DamageReceiver : MonoBehaviour
    {
        private void Awake()
        {
            var Health = GetComponentInChildren<HealthState>();
            if (Health == null) Debug.LogError("Damage Receiver is missing health state!", this);
            DamageSystem.AddReceiver(gameObject, Health);
        }

        private void OnDestroy()
        {
            DamageSystem.RemoveReceiver(gameObject);
        }
    }
}