using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Elements.Utilities;
using UnityEngine;

namespace Elements.Totem
{
    public class TotemDisplayHandler : MonoBehaviour
    {
           
        public TotemParticles particles;
        public TotemSpriteOverlay overlay;
        public ElementContainer playerContainer;
        public ElementContainer uiContainer;
        private DisplayState _state;
        private DisplayState _prevState;


        public float consumeAnimationTime = 1f;
        public float consumeAnimRadius = 100;
        public Ease consumeAnimEase = Ease.OutExpo;
        public Ease fadeAnimEase = Ease.Flash;
        
        private float _radius;
        private float _targetRadius;

        public float TargetRadius
        {
            set
            {
                _targetRadius = value;
                if (_state != DisplayState.Charging)
                {
                    _radius = _targetRadius;
                }
            }
        }
        public float Radius
        {
            set
            {
                _radius = value;
            }
            get
            {
                return _radius;
            }
        }
        
        [Range(0,1)]private float alphaOff = 0.1f;
        [Range(0, 1)] private float alphaOn = 1;

        private Tween consumeTween;
        private List<IDisplayTotemElement> _elementDisplays;
        private List<IDisplayTotemColor> _colorDisplays;
        private List<IDisplayTotemRadius> _radiusDisplays;
        private List<IDisplayTotemChargeState> _stateDisplays;


        private void Awake()
        {
            InitializeDisplays();
        }


        private void Update()
        {
            if (consumeTween != null && consumeTween.IsPlaying()) return;
            if (_state != _prevState)
            {
                //State change occured
                if (_state == DisplayState.Charging)
                {
                    overlay.enabled = particles.enabled = false;
                    overlay.Color =particles.Color = Color.clear;
                }
                else
                {
                    var color = ElementColorPalettes.GetColor(_state == DisplayState.InUse ? uiContainer.Element : playerContainer.Element);
                    if (_state == DisplayState.InUse)
                    {
                        color.a = alphaOn;
                        UpdateColorDisplays(color);
                        UpdateRadiusDisplays(_targetRadius);
                    }
                    else
                    {
                        color.a = alphaOff;
                        UpdateColorDisplays(color);
                        UpdateRadiusDisplays(0);
                    }
                   
                }
            }
            _prevState = _state;
        }

        public enum DisplayState
        {
            Charging,
            Ready,
            InUse
        }
        
        
        
        
        private void InitializeDisplays()
        {
            var displays = GetComponentsInChildren<ITotemDisplay>();
            _radiusDisplays = new List<IDisplayTotemRadius>();
            _colorDisplays = new List<IDisplayTotemColor>();
            _stateDisplays = new List<IDisplayTotemChargeState>();
            _elementDisplays = new List<IDisplayTotemElement>();
            foreach (var totemDisplay in displays)
            {
                if (totemDisplay is IDisplayTotemRadius radiusDisplay)
                    _radiusDisplays.Add(radiusDisplay);
                if (totemDisplay is IDisplayTotemColor colorDisplay)
                    _colorDisplays.Add(colorDisplay);
                if (totemDisplay is IDisplayTotemChargeState stateDisplay)
                    _stateDisplays.Add(stateDisplay);
                if (totemDisplay is IDisplayTotemElement elementDisplay)
                    _elementDisplays.Add(elementDisplay);
            }
        }

 
        private void UpdateElementDisplays(Element element)
        {
            foreach (var elementDisplay in _elementDisplays)
                elementDisplay.Element = element;
        }
        
        private void UpdateColorDisplays(Color color)
        {
            foreach (var colorDisplay in _colorDisplays)
                colorDisplay.Color = color;
        }
        private void UpdateStateDisplays(DisplayState state)
        {
            bool isCharging = state == DisplayState.Charging;
            foreach (var stateDisplay in _stateDisplays)
                stateDisplay.IsCharging = isCharging;
        }

        private void UpdateRadiusDisplays(float radius)
        {
            foreach (var radiusDisplay in _radiusDisplays) 
                radiusDisplay.Radius = radius;
        }
        
        
        public void SetDisplayElement(Element element)
        {
            //update element displays
            UpdateElementDisplays(element);

            //update color displays
            UpdateColorDisplays(ElementColorPalettes.GetColor(element));
        }

        public void SetDisplayState(DisplayState state)
        {
            _state = state;
            
            //update state displays
            UpdateStateDisplays(state);
        }



        public void Consume()
        {
            var seq = DOTween.Sequence();
            var sr = overlay.GetComponent<SpriteRenderer>();
            
            var t1 = DOTween.ToAlpha(
                    () => sr.color,
                    value => {
                    sr.color = value;
                    UpdateColorDisplays(value);
                }, 
                    0,
                    consumeAnimationTime/2f)
                .SetEase(fadeAnimEase)
                .OnComplete(() => sr.enabled = false);
            
            var t2 = DOTween.To(
                    () => Radius, 
                    r => {
                    Radius = r;
                    UpdateRadiusDisplays(r);
                },  
                    consumeAnimRadius,
                    consumeAnimationTime)
                .SetEase(consumeAnimEase);
                
            seq.Insert(0, t1);
            seq.Insert(0, t2);
            
            consumeTween = seq;
            consumeTween.Play();
        }
    }
}