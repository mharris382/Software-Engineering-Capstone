using DG.Tweening;
using UnityEngine;

namespace Spell_Casting.Projectiles
{
    public class LinearProjectileLauncher : MonoBehaviour
    {

        public LinearProjectile projectilePrefab;
        [SerializeField] Transform spawnPoint;

        public Transform SpawnPoint
        {
            set => spawnPoint = value;
        }
        
        public void LaunchProjectile(Vector3 direction)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var origin = spawnPoint.position;
            var velocity = direction * projectile.speed;
            var lifeTime = projectile.lifeTime;
            var collisionMask = projectile.collisionMask;
            var maxDistance = projectile.speed * projectile.lifeTime;
            var endPosition = origin + velocity * lifeTime;
            var hit = Physics2D.Raycast(origin, velocity, maxDistance, collisionMask);
            if (hit)
            {
                endPosition = hit.point;
                maxDistance = hit.distance;
                lifeTime = maxDistance / projectile.speed;
            }
            var angle = Vector2.Angle(Vector2.right, velocity);
            if (velocity.y < 0)
            {
                angle = -angle;
            }
            projectile.transform.position = origin;
            projectile.transform.DOMove(endPosition, lifeTime);
        }
    }
}