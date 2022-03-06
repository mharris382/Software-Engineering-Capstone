using System;
using UnityEngine;
[Serializable]
public class ProjectileConfig 
{
    [Tooltip("Projectile Prefab that will be instantiated when fired")]
    public RigidbodyProjectile prefab;
    
    [Tooltip("The impulse force that will be applied to the projectile when fired")]
    public float launchForce = 10;
    
    [Tooltip("Minimum amount of time after firing before another projectile can be fired")]
    public float castRate = 0.5f;
    
    [Tooltip("0 is completely random, 1 is perfect accuracy")]
    [Range(0,1)] 
    public float accuracy = 0.9f;

    [Tooltip("Maximum upwards angle that the projectile can be launched at")]    
    [Range(0, 90)]public float maxAngle = 90;
    
    [Tooltip("Minimum downwards angle that the projectile can be launched at")]
    [Range(-90, 0)]public float minAngle = -90;

    public float projectileRadius = 0.25f;
}