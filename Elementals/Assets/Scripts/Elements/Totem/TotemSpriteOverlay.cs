using System;
using UnityEngine;

namespace Elements.Totem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TotemSpriteOverlay : MonoBehaviour, IDisplayTotemRadius, IDisplayTotemColor, IDisplayTotemChargeState
    {
        private SpriteRenderer _sr;
        [Range(0, 1)] private float maxAlpha = 0.2f;
        
        SpriteRenderer sr
        {
            get
            {
                if (_sr == null)
                {
                    _sr = GetComponent<SpriteRenderer>();
                }
                return _sr;
            }
        }

        public float Radius
        {
            set
            {
                transform.localScale = (Vector3.one *2)* value;       
            }
        }

        public Color Color
        {
            set
            {
                value.a = Mathf.Min(maxAlpha, value.a);
                sr.color = value;
            }
        }

        private void OnEnable()
        {
            sr.enabled = true;
        }

        public bool IsCharging
        {
            set => enabled = !value;
        }
    }
}