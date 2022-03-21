using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public static class DamageSystem
    {
        private static Dictionary<GameObject, IHealth> _receivers = new Dictionary<GameObject, IHealth>();
        
        public static void AddReceiver(GameObject owner, IHealth receiver)
        {
            _receivers.Add(owner, receiver);
        }

        public static void RemoveReceiver(GameObject owner)
        {
            _receivers.Remove(owner);
        }

        public static void DealDamage(GameObject target, DamageInfo damageInfo)
        {
            if (_receivers.ContainsKey(target))
            {
                var health = _receivers[target];
                void ApplyDamageToHealth()
                {
                    health.damageHealth(damageInfo.Damage);
                    damageInfo.HasDamageBeenApplied = true;
                }
                
                
                damageInfo.DamageMultiplier = ElementMatrix.getDamageModifier(health.Element, damageInfo.Element);
                ApplyDamageToHealth();
            }
        }
    }
}