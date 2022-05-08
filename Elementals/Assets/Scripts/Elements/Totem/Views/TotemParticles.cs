using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace Elements.Totem.Views
{
    [RequireComponent(typeof(ParticleSystem))]
    public class TotemParticles : MonoBehaviour, IDisplayTotemRadius, IDisplayTotemColor, IDisplayTotemChargeState
    {
        [SerializeField] private float radius = 1;
        
        [Obsolete("using TotemElementEffectSwapper as a workaround b/c changing particle colors is not working")]
        [SerializeField] private Color startColor;
        
        
        private Color color;
   
        ParticleSystem _ps;

        //made this static so when particles are swapped by TotemElementEffectSwapper the simulation time remains the same   
        private static float _time;
    

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

        
        private static Color ColorFromVector4(Vector4 v4) => new Color(v4.x, v4.y, v4.z, v4.w);
    
    

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
}