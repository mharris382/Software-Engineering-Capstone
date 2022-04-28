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
            Observable.FromEvent<Element>(t => totemElement.OnElementChanged += t, t => totemElement.OnElementChanged -= t)
                .Select(ElementColorPalettes.GetColor)
                .TakeUntilDestroy(this)
                .Subscribe(c => color.Value = c);
            
            isActive.Select(active => active ? color : Observable.Return(inactiveColor))
                .Switch()
                .TakeUntilDestroy(this)
                .Subscribe( color => sr.color = color);
            
            
        }
    }
}