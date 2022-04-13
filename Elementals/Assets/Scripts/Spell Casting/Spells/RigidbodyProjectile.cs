using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RigidbodyProjectile : MonoBehaviour
{

    public ProjectileConfig config;
    
    public bool ignoreCollisions;

    [SerializeField] private FlipSpriteMode flipSprite;

    [Flags]
    private enum FlipSpriteMode
    {
        None = 0,
        X,
        Y
    }
    
    private Rigidbody2D _rb;
    private Collider2D _coll;
    private SpriteRenderer _sr;

    public Rigidbody2D Rb => _rb;
    public Collider2D Coll => _coll;
    private SpriteRenderer Sr => _sr;

    public bool FlipX
    {
        set
        {
            if (Sr != null)
            {
                Sr.flipX = value;
            }
        }
    }
    
    public bool FlipY
    {
        set
        {
            if (Sr != null)
            {
                Sr.flipY = value;
            }
        }
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

 
    public RigidbodyProjectile FireProjectile(GameObject shooter, Vector2 spawnOrigin, Vector2 direction)
    {
        config.ApplyAccuracy(ref direction);
        config.ClampDirection(ref direction);
        config.OffsetSpawnPosition(ref spawnOrigin, direction);
        
        var angleZ = Vector2.Angle(Vector2.right, direction);
        var rot = Quaternion.Euler(0, 0, angleZ);
        var spawnedProjectile = GameObject.Instantiate(this, spawnOrigin, rot);
        
        UpdateSpriteFlipped();
        UpdateIgnoreColliders();
        CallDecorators(spawnedProjectile);

        //apply the launch force
        var force = direction.normalized * spawnedProjectile.config.launchForce;
        spawnedProjectile.Rb.AddForce(force, ForceMode2D.Impulse);
        
        void UpdateSpriteFlipped()
        {
            if (((int) flipSprite & (int) FlipSpriteMode.X) != 0)
            {
                spawnedProjectile.FlipX = direction.x < 0;
            }

            if (((int) flipSprite & (int) FlipSpriteMode.Y) != 0)
            {
                spawnedProjectile.FlipY = direction.y < 0;
            }
        }

        void UpdateIgnoreColliders()
        {
            if (shooter != null && ignoreCollisions)
            {
                var ignoreColls = shooter.GetComponentsInChildren<Collider2D>();
                foreach (var coll in ignoreColls) Physics2D.IgnoreCollision(coll, spawnedProjectile.Coll, true);
            }
        }


        return spawnedProjectile;
    }

    private static void CallDecorators(RigidbodyProjectile spawnedProjectile)
    {
        var decorators = spawnedProjectile.GetComponentsInChildren<IProjectileDecorator>();
        foreach (var decorator in decorators)
        {
            decorator.OnProjectileFired(spawnedProjectile);
        }
    }
}
