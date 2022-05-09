using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    /// <summary>
    /// entry/exit point for all damage.  This is the only class that should be called from outside of the damage system
    /// </summary>
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
                
                void ApplyDamageToHealth(DamageInfo dInfo)
                {
                    health.damageHealth(damageInfo);
                    dInfo.HasDamageBeenApplied = true;
                }
                
                damageInfo.DamageMultiplier = ElementMatrix.getDamageModifier(health.Element, damageInfo.Element);
                ApplyDamageToHealth(damageInfo);
            }
        }
    }
}