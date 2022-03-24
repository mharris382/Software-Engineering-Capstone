using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
            if (Vector2.Distance(target.position, transform.position) <= fireRadius)
            {

                var randNumber = Random.Range(1, 5);
                if (randNumber == 5)
                {
                    useStrongCast = true;
                }
                else
                {
                    useStrongCast = false;
                }
                Fire();
            }
        }

        void Fire()
        {
            if(useStrongCast)
                spellCaster.StrongCast();
            else
                spellCaster.BasicCast();
        }


        private void OnDrawGizmos()
        {
            var c = Color.red;
            c.a = 0.6f;
            Gizmos.color = c;
            var position = transform.position;
            Gizmos.DrawWireSphere(position, fireRadius);
            

        }
    }
}