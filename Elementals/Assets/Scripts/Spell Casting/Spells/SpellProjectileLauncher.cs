using System;
using System.Collections;
using Spell_Casting.Spells;
using UnityEngine;

[AddComponentMenu("Spells/Spell Projectile Launcher")]
public class SpellProjectileLauncher : InstantiateObjectSpell<RigidbodyProjectile>, ISpell
{
    private const float MAX_ANGLE_OFFSET = 30f;
    [Obsolete("Moved this to projectile config")]
    public ProjectileConfig projectile;
    public bool flipSprite;
    public GameObject ignore;
    
    private float Accuracy => projectile.accuracy;

    private float lastCastTime = 0;


    [SerializeField] private RigidbodyProjectile projectilePrefab;

    public override bool CastSpell(GameObject caster, Vector2 position, Vector2 direction)
    {
        if (Time.time - lastCastTime < projectilePrefab.config.castRate)
            return false;
        lastCastTime = Time.time;
        return base.CastSpell(caster, position, direction);
    }


    protected override RigidbodyProjectile InstantiateSpell(GameObject caster, Vector2 position, Vector2 direction)
    {
        return projectilePrefab.FireProjectile(caster, position, direction);
        // //check to see if the direction is within projectile angle limits 
        // var referenceVector = direction.x > 0 ? Vector2.right : Vector2.left;
        // var angle = Vector2.SignedAngle(referenceVector, direction);
        // var clampedAngle = Mathf.Clamp(angle, projectile.minAngle, projectile.maxAngle);
        // if (Math.Abs(clampedAngle - angle) > 0.01f) 
        // {
        //     //if the angle is outside the limits then replace it with the nearest valid angle 
        //     direction = Quaternion.Euler(0, 0, clampedAngle) * referenceVector;
        //     
        // }
        //
        // //apply randomness to the direction
        // var randomness = UnityEngine.Random.Range(-MAX_ANGLE_OFFSET, MAX_ANGLE_OFFSET) * (1-Accuracy);
        // direction = Quaternion.Euler(0, 0, randomness) * direction;
        //
        // var spawnPosition = position;
        // spawnPosition += (direction * (projectile.projectileRadius + 0.5f));
        //
        // var angleZ = Vector2.Angle(Vector2.right, direction);
        // var rot = Quaternion.Euler(0, 0, angleZ);
        //
        // //spawn the projectile
        // var spawnedProjectile = Instantiate(projectile.prefab, spawnPosition, rot);
        // if (flipSprite)
        // {
        //     spawnedProjectile.FlipX = direction.x < 0;
        // }
        //
        //
        // var ignoreColls = this.ignore.GetComponentsInChildren<Collider2D>();
        // foreach (var coll in ignoreColls) Physics2D.IgnoreCollision(coll, spawnedProjectile.Coll, true);
        // //apply the launch force
        // var force = direction.normalized * projectile.launchForce;
        // spawnedProjectile.Rb.AddForce(force, ForceMode2D.Impulse);
        //
        // var decorators = GetComponentsInChildren<IProjectileDecorator>();
        // foreach (var decorator in decorators)
        // {
        //     decorator.OnProjectileFired(spawnedProjectile);
        // }
        //
        // return spawnedProjectile;
    }
}

public interface IProjectileDecorator
{
    void OnProjectileFired(RigidbodyProjectile projectile);
}


