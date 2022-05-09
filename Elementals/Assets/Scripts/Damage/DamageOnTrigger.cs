using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    /// <summary>
    /// attempts to deal damage to any rigidbody that enters the trigger
    /// <seealso cref="DamageOnCollision"/>
    /// <seealso cref=" Spell_Casting.Spells.DamageOnParticleCollision"/> 
    /// </summary>
    public class DamageOnTrigger : MonoBehaviour
    {
        public Element element;
        public float amount = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
            var hit = other.attachedRigidbody.gameObject;
            DamageSystem.DealDamage(hit, GetDamageInfo());
        }

        private DamageInfo GetDamageInfo()
        {
            return new DamageInfo()
            {
                Element = element,
                RawDamage = amount
            };
        }
    }
}