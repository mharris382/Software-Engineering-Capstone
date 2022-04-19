using UnityEngine;

namespace AI.Attacks
{
    public abstract class AttackBase : MonoBehaviour
    {
        [Tooltip("Time in seconds before the attack can be repeated")]
        [Range(0.1f, 30)]
        [SerializeField] private float minRechargeTime = 1;
        
        
        private Transform desiredTarget;
        
        /// <summary>
        /// stores the desired target of this attack.  The process of translating the desired 
        /// </summary>
        public Transform DesiredDesiredTarget
        {
            set
            {
                if (value != null)
                {
                    if (desiredTarget == null)
                    {
                        desiredTarget = new GameObject("Target").transform;
                    }
                    desiredTarget.SetParent(value, false);
                }
            }
            get
            {
                if (desiredTarget == null)
                {
                    desiredTarget = new GameObject("Target").transform;
                }

                return desiredTarget;
            }
        }
        
        protected virtual void Awake()
        {
            if (desiredTarget == null)
            {
                desiredTarget = new GameObject("Target").transform;
            }
        }


        protected abstract void Prepare();
        protected abstract void Attack();
        protected abstract void Cooldown();
    }
}