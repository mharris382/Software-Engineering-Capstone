using System;
using Elements;
using UnityEngine;

namespace Damage
{
    /// <summary>
    /// attempts to deal damage to any rigidbody that it collides with
    /// <see cref="DamageOnTrigger"/>
    /// <seealso cref=" Spell_Casting.Spells.DamageOnParticleCollision"/> 
    /// </summary>
    public class DamageOnCollision : MonoBehaviour, IElementalDependent
    {
        public Element element;
        public float amount = 1;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.rigidbody == null) return;
            var hit = other.rigidbody.gameObject;
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

        public Element Element
        {
            get => element;
            set => element = value;
        }
    }
}