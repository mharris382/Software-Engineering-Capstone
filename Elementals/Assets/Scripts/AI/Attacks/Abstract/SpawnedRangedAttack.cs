using UnityEngine;

namespace AI.Attacks
{
    public abstract class SpawnedRangedAttack<T> : RangedAttackBase where T : Component
    {
        [SerializeField] T prefab;
        protected override GameObject CreateAttackInstance() => Instantiate(prefab, SpawnPoint, SpawnRotation).gameObject;
    }

}