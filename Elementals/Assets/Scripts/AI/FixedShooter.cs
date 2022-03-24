using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class FixedShooter : MonoBehaviour
    {
        public SpellCaster spellCaster;
        [SerializeField] private Transform target;

        public float fireRadius = 5;
        public bool useStrongCast;
        
        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private void Update()
        {
            
        }

        void Fire()
        {
            if(useStrongCast)
                spellCaster.StrongCast();
            else
                spellCaster.BasicCast();
        }
    }
}