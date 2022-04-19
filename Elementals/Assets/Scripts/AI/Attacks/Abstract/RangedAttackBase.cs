using UnityEngine;

namespace AI.Attacks
{
    public abstract class RangedAttackBase : AttackBase
    {
        
        private Vector2 _aimPoint;

        private float _aimValue = 1;

        protected Vector2 AimPoint
        {
            get
            {
                if (AimValue >= 0.9999f)
                    return DesiredDesiredTarget.position;
                return _aimPoint;
            }
        }
        
        protected float AimValue
        {
            get
            {
                return _aimValue;
            }
            set
            {
                _aimValue = Mathf.Clamp01(value);
            }
        }

        protected Vector2 SpawnPoint
        {
            get => transform.position;
        }

        protected Quaternion SpawnRotation
        {
            get => transform.rotation;
        }
        
        
        protected abstract GameObject CreateAttackInstance();
    }
}