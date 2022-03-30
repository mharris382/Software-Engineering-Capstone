using System;
using Elements;
using ManaSystem;
using UnityEngine;

namespace Damage
{
    
    public class DropManaOnDeath : MonoBehaviour, IElementalDependent
    {
        
        [SerializeField]
        private Element element;

        public Element Element
        {
            get => element;
            set => element = value;
        }

        public void OnHealthDamaged(IHealth health, DamageInfo damageInfo)
        {
            if (health.isAlive) return;
            ManaDropper.DropMana(element, transform.position);
        }
        
    }

}