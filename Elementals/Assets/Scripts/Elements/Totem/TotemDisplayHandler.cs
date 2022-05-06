using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Elementals;
using Elements.Totem.Views;
using Elements.Utilities;
using UnityEngine;

namespace Elements.Totem
{
    /// <summary>
    /// Coordinates all view systems to correctly update all visual components together.
    ///all the information needed to coordinates the displays is passed to the totem display handler through public methods
    /// <see cref="Totem"/> 
    /// <para> the display handler marks a system boundary between the gameplay system logic and the UI/UX system logic.
    /// The boundary line is defined through interfaces <see cref="ITotemDisplay"/> The gameplay
    /// system logic defines the underlying mechanics, while the UI/UX systems are responsible for making those underlying
    /// mechanics intuitive for new player to understand, and clear for advance players to use. </para>
    /// <para>displays the effective radius of the totem <see cref="IDisplayTotemRadius"/></para>
    /// <para>displays whether the totem is ready to use or not<seealso cref="IDisplayTotemChargeState"/></para>
    /// <para>passes the current element to displays <see cref="IDisplayTotemElement"/>, although this could also be
    /// accomplished through the use of ElementContainers</para>
    /// <para>passes element's colors to displays <see cref="IDisplayTotemColor"/> to make it easier to coordinate color schemes</para> 
    /// </summary>
    public class TotemDisplayHandler : MonoBehaviour
    {
        public enum DisplayState
        {
            Charging,
            Ready,
            InUse
        }

        
        [System.Obsolete("now controlled indirectly through ITotemDisplay")] public TotemParticles particles;
        [System.Obsolete("now controlled indirectly through ITotemDisplay")] public TotemSpriteOverlay overlay;
        
        
        public ElementContainer playerContainer;
        public ElementContainer uiContainer;
        [Header("Totem Settings")]
        
        [SerializeField, Range(0,1)] 
        private float alphaOff = 0.1f;
        
        
        [SerializeField,Range(0, 1)] 
        private float alphaOn = 1;

        [SerializeField]
        [MinMax(0,1, "off", "on" ,FlexibleFields = false)]
        private Vector2 alphaRange = new Vector2(0.1f, 1);
        
        [Header("Consume Animation Settings")]
        [Tooltip("How long does the consume totem feedback animation last (in seconds)")]
        [SerializeField,Range(0, 3)]
        public float consumeAnimationTime = 1f;
        
        [Tooltip("radius displays will have their radius animated to this value (grow/shrink)")]
        [SerializeField,Range(0, 1000)]
        public float consumeAnimRadius = 100;
        
        [Tooltip("Easing for the consume radius animation")]
        [SerializeField] public Ease consumeAnimEase = Ease.OutExpo;
        
        [Tooltip("Easing for the color fade of color displays (to clear)")]
        [SerializeField] public Ease fadeAnimEase = Ease.Flash;

        
        
        private DisplayState _state;
        private DisplayState _prevState;
 

        private Tween consumeTween;
        private List<IDisplayTotemElement> _elementDisplays;
        private List<IDisplayTotemColor> _colorDisplays;
        private List<IDisplayTotemRadius> _radiusDisplays;
        private List<IDisplayTotemChargeState> _stateDisplays;
 

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
        
        


        #region [Unity MonoBehaviour Events]

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
                    UpdateColorDisplays(Color.clear);
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
                UpdateStateDisplays(DisplayState.Charging);
            }
            _prevState = _state;
        }
        

        #endregion


        
        #region [Internal Display Calls]

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

 
        /// <summary>
        /// updates all element displays <see cref="IDisplayTotemElement"/>
        /// </summary>
        /// <param name="element"></param>
        private void UpdateElementDisplays(Element element)
        {
            foreach (var elementDisplay in _elementDisplays)
                elementDisplay.Element = element;
        }
        
        /// <summary>
        /// updates all color displays <see cref="IDisplayTotemColor"/>
        /// </summary>
        /// <param name="color"></param>
        private void UpdateColorDisplays(Color color)
        {
            foreach (var colorDisplay in _colorDisplays)
                colorDisplay.Color = color;
        }
        
        /// <summary>
        /// updates all <see cref="IDisplayTotemChargeState">state displays</see>
        /// </summary>
        /// <param name="state"></param>
        private void UpdateStateDisplays(DisplayState state)
        {
            bool isCharging = state == DisplayState.Charging;
            foreach (var stateDisplay in _stateDisplays)
                stateDisplay.IsCharging = isCharging;
        }

        /// <summary>
        /// updates all radius displays <see cref="IDisplayTotemRadius"/>
        /// </summary>
        /// <param name="radius"></param>
        private void UpdateRadiusDisplays(float radius)
        {
            foreach (var radiusDisplay in _radiusDisplays) 
                radiusDisplay.Radius = radius;
        }
        
        

        #endregion
        

        
        #region [Public Methods]

        [Obsolete("Not sure if this needs to be public, since the totem owns ElementContainers")]
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



        /// <summary>
        /// This method should be called to notify the player that they have confirmed their element selection and the totem is now charging.
        /// This is a significant gameplay event, so it's importance should be clearly displayed to the player.  For that reason the display handler
        /// will trigger it's own special display logic for this event 
        /// </summary>
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

        #endregion
    }
}