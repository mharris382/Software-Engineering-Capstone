using System;
using System.Collections;
using System.Collections.Generic;
using Elements;
using UnityEngine;

namespace Damage
{
    /// <summary>
    /// attempts to deal damage to any rigidbody that enters the trigger
    /// <seealso cref="DamageOnCollision"/>
    /// <seealso cref=" Spell_Casting.Spells.DamageOnParticleCollision"/> 
    /// </summary>
    public class DamageOnTrigger : MonoBehaviour, IElementalDependent
    {
        public Element element;
        public float amount = 1;
        private RigidbodyProjectile _projectile;

        private void Awake()
        {
            _projectile = GetComponentInParent<RigidbodyProjectile>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
            if (_projectile != null && _projectile.Shooter != null)
            {
                if(_projectile.Shooter.gameObject == other.attachedRigidbody.gameObject) return;
            }
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

        public Element Element
        {
            set => element = value;
        }
    }
}