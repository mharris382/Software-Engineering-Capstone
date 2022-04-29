using System;
using System.Collections;
using System.Collections.Generic;
using Elements.Totem;
using Unity.Collections;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;


[RequireComponent(typeof(ParticleSystem))]
public class TotemParticles : MonoBehaviour, IDisplayTotemRadius, IDisplayTotemColor, IDisplayTotemChargeState
{
    [SerializeField] private float radius = 1;
    [SerializeField] private Color startColor;
     private Color color;
   
    ParticleSystem _ps;

    private float _time;
    

    public float Radius
    {
        set
        {
            this.radius = value;
        }
    }
    public Color Color
    {
        set
        {
            this.color = color;
        }
    }
    
    
    void Awake()
    {
        color = startColor;
        _ps  = GetComponent<ParticleSystem>();
        var main = _ps.main;
        main.startColor = color;
        
    }


    private void OnEnable()
    {
        _time = 0;
        var main = _ps.main;
        var maxParticles = main.maxParticles;

        // NativeArray<float> GetNativeArrayFloat() => new NativeArray<float>(maxParticles, Allocator.Persistent);
        // ParticleSystemNativeArray3 GetNativeArray3()
        // {
        //     return new ParticleSystemNativeArray3()
        //     {
        //         x = GetNativeArrayFloat(),
        //         y = GetNativeArrayFloat(),
        //         z = GetNativeArrayFloat()
        //     };
        // }
        // ParticleSystemNativeArray4 GetNativeArray4()
        // {
        //     return new ParticleSystemNativeArray4()
        //     {
        //         x = GetNativeArrayFloat(),
        //         y = GetNativeArrayFloat(),
        //         z = GetNativeArrayFloat(),
        //         w = GetNativeArrayFloat()
        //     };
        // }
        //
        // _particles = new NativeArray<ParticleSystem.Particle>(maxParticles, Allocator.Persistent);
        // psPositions = GetNativeArray3();
        // psVelocities = GetNativeArray3();
        // psSizes = GetNativeArray3();
        // psJob = new ParticleSystemJobData();

    }
    
    private static Color ColorFromVector4(Vector4 v4) => new Color(v4.x, v4.y, v4.z, v4.w);

    private ParticleSystemJobData psJob;
    private NativeArray<ParticleSystem.Particle> _particles;
    private ParticleSystemNativeArray3 psPositions, psVelocities, psSizes, psRotations;
    
    

    private void Update()
    {
        _time += Time.deltaTime;
        _time %= _ps.main.duration;

        var mainModule = _ps.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient()
        {
            mode = ParticleSystemGradientMode.Color,
            color = color,
            colorMax = color,
            colorMin = color
        };
        
        var shapeModule = _ps.shape;
        shapeModule.radius = radius;
        _ps.Simulate(_time, true, true);
        int count = _ps.particleCount;
        var particles = new NativeArray<ParticleSystem.Particle>(count, Allocator.TempJob);


        ParticleSystemJobData data = new ParticleSystemJobData();
        
    }

    public bool IsCharging
    {
        set
        {
            if (value)
                enabled = true;
        }
    }

  
}

