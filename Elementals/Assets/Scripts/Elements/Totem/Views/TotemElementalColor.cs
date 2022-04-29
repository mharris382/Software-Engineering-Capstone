using System;
using Elements.Utilities;
using UniRx;
using UnityEngine;

namespace Elements.Totem.Views
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TotemElementalColor : MonoBehaviour
    {
        [SerializeField] private ElementContainer totemElement;
        [SerializeField] private Color inactiveColor = Color.clear;
        [SerializeField,Range(0,1)]
        private float maxAlpha = 0.2f;
        private SpriteRenderer _sr;
        public SpriteRenderer sr
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
        
        
       [SerializeField] private BoolReactiveProperty isActive = new BoolReactiveProperty();
       [SerializeField] private ColorReactiveProperty color = new ColorReactiveProperty();

        public Color ElementColor
        {
            get => _sr.color;//return the sprite color which will be == to color if active, and clear if inactive
            set => color.Value = value;
        }
        public bool IsActive
        {
            get => isActive.Value;
            set => isActive.Value = value;
        }

        private void Awake()
        {
            totemElement.OnElementChanged += OnElementChanged;
        }

        void OnElementChanged(Element element)
        {
            var color = ElementColorPalettes.GetColor(element);
            color.a = Mathf.Min(maxAlpha, color.a);
            sr.color = color;
        }

        private void OnDestroy()
        {
            totemElement.OnElementChanged -= OnElementChanged;
        }
    }
}