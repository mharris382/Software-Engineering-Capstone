using System;
using System.Collections;
using System.Collections.Generic;
using Spell_Casting.Spells;
using UnityEngine;

public class SpellProjectileLauncher : MonoBehaviour, ISpell
{
    private const float MAX_ANGLE_OFFSET = 90f;
    
    public ProjectileConfig projectile;
    
    public GameObject ignore;
    
    private float Accuracy => projectile.accuracy;


    public void FireProjectile( Vector2 spawnPosition, Vector2 direction)
    {
        // Vector2 spawnPosition = aimTransform.position;
       // var direction = AimDirection;
        
        //check to see if the direction is within projectile angle limits 
        var referenceVector = direction.x > 0 ? Vector2.right : Vector2.left;
        var angle = Vector2.SignedAngle(referenceVector, direction);
        var clampedAngle = Mathf.Clamp(angle, projectile.minAngle, projectile.maxAngle);
        if (Math.Abs(clampedAngle - angle) > 0.01f) 
        {
            //if the angle is outside the limits then replace it with the nearest valid angle 
            direction = Quaternion.Euler(0, 0, clampedAngle) * referenceVector;
        }
        
        //apply randomness to the direction
        var randomness = UnityEngine.Random.Range(-MAX_ANGLE_OFFSET, MAX_ANGLE_OFFSET) * (1-Accuracy);
        direction = Quaternion.Euler(0, 0, randomness) * direction;

        spawnPosition += (direction * (projectile.projectileRadius + 0.5f));
        
        //spawn the projectile
        var spawnedProjectile = Instantiate(projectile.prefab, spawnPosition, Quaternion.identity);
        spawnedProjectile.transform.right = direction;
        var ignoreColls = this.ignore.GetComponentsInChildren<Collider2D>();
        foreach (var coll in ignoreColls) Physics2D.IgnoreCollision(coll, spawnedProjectile.Coll, true);
        //apply the launch force
        var force = direction.normalized * projectile.launchForce;
        spawnedProjectile.Rb.AddForce(force, ForceMode2D.Impulse);

        var decorators = GetComponentsInChildren<IProjectileDecorator>();
        foreach (var decorator in decorators)
        {
            decorator.OnProjectileFired(spawnedProjectile);
        }

    }

    public void CastSpell(GameObject caster, Vector2 position, Vector2 direction)
    {
        FireProjectile(position, direction);
    }
    public float ManaCost
    {
        get { return manaCost; }
    }

    [SerializeField]
    private float manaCost = 1;
    
}

public interface IProjectileDecorator
{
    void OnProjectileFired(RigidbodyProjectile projectile);
}