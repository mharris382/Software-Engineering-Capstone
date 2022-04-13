using System;
using UnityEngine;
[Serializable]
public class ProjectileConfig 
{
    private const float MAX_ANGLE_OFFSET = 30f;
    
    [System.Obsolete("")]
    [Tooltip("Projectile Prefab that will be instantiated when fired")]
    public RigidbodyProjectile prefab;
    
    [Header("Main Settings")]
    
    [Tooltip("The impulse force that will be applied to the projectile when fired")]
    public float launchForce = 10;
    
    [Tooltip("Minimum amount of time after firing before another projectile can be fired")]
    public float castRate = 0.5f;

    [Header("Extra Settings")]
    [Tooltip("0 is completely random, 1 is perfect accuracy")]
    [Range(0,1)] 
    public float accuracy = 1f;

    [Tooltip("Maximum upwards angle that the projectile can be launched at")]    
    [Range(0, 90)]public float maxAngle = 90;
    
    [Tooltip("Minimum downwards angle that the projectile can be launched at")]
    [Range(-90, 0)]public float minAngle = -90;

    public float projectileRadius = 0.25f;


    public void ClampDirection(ref Vector2 direction)
    {
        var referenceVector = direction.x > 0 ? Vector2.right : Vector2.left;
        var angle = Vector2.SignedAngle(referenceVector, direction);
        var clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);
        if (Math.Abs(clampedAngle - angle) > 0.01f) 
        {
            //if the angle is outside the limits then replace it with the nearest valid angle 
            direction = Quaternion.Euler(0, 0, clampedAngle) * referenceVector;
            
        }
    }

    public void ApplyAccuracy(ref Vector2 direction)
    {
        var randomness = UnityEngine.Random.Range(-MAX_ANGLE_OFFSET, MAX_ANGLE_OFFSET) * (1-accuracy);
        direction = Quaternion.Euler(0, 0, randomness) * direction;
    }

    public void OffsetSpawnPosition(ref Vector2 spawnPosition,Vector2 direction)
    {
        spawnPosition += (direction * (projectileRadius + 0.5f));
    }
    //  public bool FireProjectile(RigidbodyProjectile projectile, Vector2 spawnPosition, Vector2 direction)
    // {
    //    //  // Vector2 spawnPosition = aimTransform.position;
    //    // // var direction = AimDirection;
    //    //
    //    //
    //    // //check to see if the direction is within projectile angle limits 
    //    //  var referenceVector = direction.x > 0 ? Vector2.right : Vector2.left;
    //    //  var angle = Vector2.SignedAngle(referenceVector, direction);
    //    //  var clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);
    //    //  if (Math.Abs(clampedAngle - angle) > 0.01f) 
    //    //  {
    //    //      //if the angle is outside the limits then replace it with the nearest valid angle 
    //    //      direction = Quaternion.Euler(0, 0, clampedAngle) * referenceVector;
    //    //      
    //    //  }
    //    //  
    //    //  //apply randomness to the direction
    //    //  var randomness = UnityEngine.Random.Range(-MAX_ANGLE_OFFSET, MAX_ANGLE_OFFSET) * (1-accuracy);
    //    //  direction = Quaternion.Euler(0, 0, randomness) * direction;
    //    //
    //    //  spawnPosition += (direction * (projectileRadius + 0.5f));
    //    //
    //    //  var angleZ = Vector2.Angle(Vector2.right, direction);
    //    //  var rot = Quaternion.Euler(0, 0, angleZ);
    //    //  
    //    //  //spawn the projectile
    //    //  var spawnedProjectile = Instantiate(projectile.prefab, spawnPosition, rot);
    //    //  if (flipSprite)
    //    //  {
    //    //      spawnedProjectile.FlipY = direction.x < 0;
    //    //  }
    //    //  
    //    //  
    //    //  var ignoreColls = this.ignore.GetComponentsInChildren<Collider2D>();
    //    //  foreach (var coll in ignoreColls) Physics2D.IgnoreCollision(coll, spawnedProjectile.Coll, true);
    //    //  //apply the launch force
    //    //  var force = direction.normalized * projectile.launchForce;
    //    //  spawnedProjectile.Rb.AddForce(force, ForceMode2D.Impulse);
    //    //
    //    //  var decorators = GetComponentsInChildren<IProjectileDecorator>();
    //    //  foreach (var decorator in decorators)
    //    //  {
    //    //      decorator.OnProjectileFired(spawnedProjectile);
    //    //  }
    //    //
    //    //  return true;
    // }
}


