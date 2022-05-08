using System;
using Elements;
using ManaSystem;
using UnityEngine;

namespace Damage
{
    
    public class DropManaOnDeath : MonoBehaviour, IElementalDependent
    {
        [SerializeField, MinMaxRange(0, 50)]
        private RangeI dropAmount = new RangeI(){min = 3, max = 6};
        
        [SerializeField]
        private Element element;

        public bool debug = false;
        public Element Element
        {
            get => element;
            set
            {
                element = value;
                Debug.Log("Element set to " + element);
            }
        }

        public void OnHealthDamaged(IHealth health, DamageInfo damageInfo)
        {
            if (health.isAlive) return;
            Debug.Log("Dropping mana of type " + element);
            ManaDropper.DropMana(element, transform.position, dropAmount);
        }
        
    }

}