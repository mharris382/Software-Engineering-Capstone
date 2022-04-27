using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class TotemParticles : MonoBehaviour
{
    public float finalRadius = 10;
    public float duration;
    public CircleCollider2D circleCollider2D;
    public float radius = 1;
    public GameObject circle;
    ParticleSystem _ps;

    private float _time;
    // Start is called before the first frame update

    public float Radius
    {
        set
        {
            this.radius = value;
        }
    }
    void Awake()
    {
        _ps  = GetComponent<ParticleSystem>();
    }


    private void Update()
    {
        _time += Time.deltaTime;
        _time %= _ps.main.duration;

        var shapeModule = _ps.shape;
        shapeModule.radius = radius;
        _ps.Simulate(_time, true, true);
        
        
    }

    private NativeArray<ParticleSystem.Particle> _particles;

    private void OnParticleUpdateJobScheduled()
    {
        
    }
}