using System.Collections;
using UnityEngine;

namespace Spell_Casting.Spells
{
    public class AutoDestroyProjectile : MonoBehaviour, IProjectileDecorator
    {
        public float destroyTime = 5f;
        public void OnProjectileFired(RigidbodyProjectile projectile)
        {
            StartCoroutine(AutoDestroy(projectile));
        }

        IEnumerator AutoDestroy(RigidbodyProjectile projectile)
        {
            yield return new WaitForSeconds(destroyTime);
            GameObject.Destroy(projectile.gameObject);
        }
    }
}