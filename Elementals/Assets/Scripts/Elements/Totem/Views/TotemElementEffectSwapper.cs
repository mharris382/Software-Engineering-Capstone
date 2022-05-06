using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements.Totem.Views
{
    public class TotemElementEffectSwapper : MonoBehaviour, IDisplayTotemElement
    {
        private ElementEffect[] effects = new ElementEffect[5] {new ElementEffect(Element.Fire), new ElementEffect(Element.Water), new ElementEffect(Element.Thunder), new ElementEffect(Element.Earth), new ElementEffect(Element.Air)};

        [Serializable]
        private struct ElementEffect
        {
            public Element element;
            public GameObject effect;
        
            public ElementEffect(Element e)
            {
                element = e;
                effect = null;
            }

            public void SetActive(bool active)
            {
                if(effect!=null)
                    effect.SetActive(active);
            }
        }


        private Dictionary<Element, ElementEffect> _elementEffects;

        private ElementEffect activeEffect;
    
        private void Awake()
        {
            _elementEffects = new Dictionary<Element, ElementEffect>();
            foreach (var elementEffect in effects)
            {
                _elementEffects.Add(elementEffect.element, elementEffect);
            }
        
            Debug.Assert(_elementEffects.ContainsKey(Element.Fire));
            Debug.Assert(_elementEffects.ContainsKey(Element.Water));
            Debug.Assert(_elementEffects.ContainsKey(Element.Earth));
            Debug.Assert(_elementEffects.ContainsKey(Element.Thunder));
            Debug.Assert(_elementEffects.ContainsKey(Element.Air));
        }


    
        public Element Element
        {
            set => SwapEffects(activeEffect, _elementEffects[value]);
        }

        void SwapEffects(ElementEffect previousEffect, ElementEffect newEffect)
        {
            previousEffect.SetActive(false);
            newEffect.SetActive(true);
            activeEffect = newEffect;
        }
    }
}