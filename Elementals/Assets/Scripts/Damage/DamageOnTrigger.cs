using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
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
                RawAmount = amount
            };
        }
    }
}