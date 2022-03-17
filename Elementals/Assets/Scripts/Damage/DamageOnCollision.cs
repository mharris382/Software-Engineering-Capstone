using System;
using UnityEngine;

namespace Damage
{
    public class DamageOnCollision : MonoBehaviour
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
                RawAmount = amount
            };
        }
    }
}