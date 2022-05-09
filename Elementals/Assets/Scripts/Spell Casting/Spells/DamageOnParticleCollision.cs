using System;
using Damage;
using Elements;
using UnityEngine;

namespace Spell_Casting.Spells
{
    /// <summary>
    /// another way to deal damage besides using rigidbodies and colliders
    /// <see cref="DamageOnCollision"/>
    /// <seealso cref="DamageOnTrigger"/>
    /// </summary>
    public class DamageOnParticleCollision : MonoBehaviour, IElementalDependent
    {
        public int damageAmount = 1;
        public Element damageElement;
        
        public void OnParticleCollision(GameObject other)
        {
            var coll = other.GetComponent<Collider2D>();
            if (coll == null) return;
            var rb = coll.attachedRigidbody;
            if (rb == null) return;
            DamageSystem.DealDamage(rb.gameObject, new DamageInfo()
            {
                RawDamage = damageAmount,
                Element = damageElement
            });
        }

        public Element Element
        {
            set => damageElement = value;
        }
    }
}