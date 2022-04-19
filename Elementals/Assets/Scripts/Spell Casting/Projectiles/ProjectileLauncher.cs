using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Projectiles 
{
    public class ProjectileLauncher : MonoBehaviour
    {
        private const float MAX_ANGLE_OFFSET = 30f;
        
        public RigidbodyProjectile prefab;

        public ProjectileConfig projectile => prefab.config;
        public bool flipSprite;
        public GameObject ignore;
        
        
        public UnityEvent<LaunchInfo> onLaunchedProjectile;
        private float lastCastTime = 0;
        
        
        private float Accuracy => projectile.accuracy;
        private float CastRate => projectile.castRate;

        public float TimeUntilReadyToFire() => IsReadyToLaunch() ? 0 : Time.time - lastCastTime;
        public float TimeUntilReadyToFireNormalized() => 1 - (TimeUntilReadyToFire() / CastRate);


        private void Awake()
        {
            var rb = GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                ignore = rb.gameObject;
            }
            var decorators = GetComponentsInChildren<IProjectileDecorator>();
            if (decorators != null && decorators.Length > 0)
            {
                onLaunchedProjectile.AddListener(launchInfo => {
                    foreach (var decorator in decorators)
                    {
                        decorator.OnProjectileFired(launchInfo.projectileInstance);
                    }
                });
            }
        }


        public bool IsReadyToLaunch()
        {
            return !(Time.time - lastCastTime < projectile.castRate);
        }

        protected virtual void Launch(LaunchInfo launchInfo)
        {
            var force = launchInfo.direction.normalized * projectile.launchForce;
            launchInfo.projectileInstance.Rb.AddForce(force, ForceMode2D.Impulse);
            onLaunchedProjectile?.Invoke(launchInfo);
        }

        public LayerMask collisionMask;
        public RigidbodyProjectile CreateAndLaunchProjectile(Vector2 position, Vector2 direction)
        {
          var info = new LaunchInfo()
            {
                projectileInstance = null,
                direction = direction,
                origin = position,
                time = Time.time
            };
            
            RestrictAimToLimits(ref info);
            
            AdjustAimForAccuracy(ref info);
            
            OffsetLaunchOrigin(ref info);
            
            
            info.projectileInstance = SpawnRigidbodyProjectile(info.origin, info.direction);
            
            Launch(info);
            
            return info.projectileInstance;
        }

        public float maxDegreeAdjust = 5;
        protected void OffsetLaunchOrigin(ref LaunchInfo launchInfo)
        {
            var spawnPosition = launchInfo.origin;
            spawnPosition += (launchInfo.direction * (projectile.projectileRadius + 0.15f));
            launchInfo.origin = spawnPosition;
            var pnt = spawnPosition;
            var dir = launchInfo.direction;
            var dist = projectile.projectileRadius * 4;
            var spawnInColliderHit = Physics2D.Raycast(pnt, dir, dist, collisionMask);
            Debug.DrawRay(pnt, dir.normalized * dist);
            if (spawnInColliderHit)
            {
                var normal = spawnInColliderHit.normal;
                Debug.Log("Will fire into floor, adjusting!");
                launchInfo.direction = Vector2.MoveTowards(dir, normal, 5);
            }
            var spawnInColliderHit2 = Physics2D.Raycast(pnt, Vector2.down, projectile.projectileRadius*1.5f, collisionMask);
            if (spawnInColliderHit2)
            {
                var d = spawnInColliderHit2.distance;
                var offset = Vector2.up * (d+0.125f);
                launchInfo.origin += offset;
            }
        }
        
        /// <summary>
        /// restricts the direction to the projectile config's upper & lower angle limit.  
        /// </summary>
        /// <param name="launchInfo"></param>
        /// <returns>true if the given direction is within limits, false if the direction was outside valid angles and was adjusted to the nearest limit </returns>
        protected bool RestrictAimToLimits(ref LaunchInfo launchInfo)
        {
            var referenceVector = launchInfo.direction.x > 0 ? Vector2.right : Vector2.left;
            var angle = Vector2.SignedAngle(referenceVector, launchInfo.direction);
            var clampedAngle = Mathf.Clamp(angle, projectile.minAngle, projectile.maxAngle);
            
            if (Mathf.Abs(clampedAngle - angle) > 0.01f) 
            {
                //if the angle is outside the limits then replace it with the nearest valid angle 
                launchInfo.direction = Quaternion.Euler(0, 0, clampedAngle) * referenceVector;
                return false;
            }

            return true;
        }
        
        protected void AdjustAimForAccuracy(ref LaunchInfo launchInfo)
        {
            var randomness = Random.Range(-MAX_ANGLE_OFFSET, MAX_ANGLE_OFFSET) * (1-Accuracy);
            launchInfo.direction = Quaternion.Euler(0, 0, randomness) * launchInfo.direction;
        }

        private RigidbodyProjectile SpawnRigidbodyProjectile(Vector2 spawnOrigin, Vector2 spawnDirection)
        {
            var angleZ = Vector2.Angle(Vector2.right, spawnDirection);
            var rot = Quaternion.Euler(0, 0, angleZ);
            //spawn the projectile
            var spawnedProjectile = Instantiate(prefab, spawnOrigin, rot);
            if (flipSprite) spawnedProjectile.FlipY = spawnDirection.x < 0;
            var ignoreColls = this.ignore.GetComponentsInChildren<Collider2D>();
            foreach (var coll in ignoreColls) Physics2D.IgnoreCollision(coll, spawnedProjectile.Coll, true);
            return spawnedProjectile;
        }
    }

    public struct LaunchInfo
    {
        public RigidbodyProjectile projectileInstance;
        public Vector2 origin;
        public Vector2 direction;
        public float time;
    }
}