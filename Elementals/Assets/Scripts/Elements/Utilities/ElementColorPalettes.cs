using System;
using System.Collections.Generic;
using DG.DOTweenEditor.Core;
using UnityEngine;

namespace Elements.Utilities
{
    [CreateAssetMenu(fileName = "New Element Color Palette", menuName = "Containers/Element Color Palette", order = 0)]
    public class ElementColorPalettes : ScriptableObject
    {
        public ElementColors fireColors = new ElementColors()
        {
            mainColor = Color.red
        };
        
        public ElementColors waterColors = new ElementColors()
        {
            mainColor = Color.blue
            
        };
        
        public ElementColors earthColors = new ElementColors()
        {
            mainColor = Color.green
        };
        
        public ElementColors lightningColors = new ElementColors()
        {
            mainColor = Color.yellow
        };
        
        public ElementColors airColors = new ElementColors()
        {
            mainColor = Color.gray
        };

        public Color GetMainColor(Element element) => GetElementColors(element).mainColor;

        public Color GetSecondaryColor(Element element) => GetElementColors(element).secondaryColor;

        public Gradient GetMainGradient(Element element) => GetElementColors(element).mainGradient;

        public Gradient GetSecondaryGradient(Element element) => GetElementColors(element).secondaryGradient;

        ElementColors GetElementColors(Element e)
        {
            switch (e)
            {
                case Element.Fire:
                    return fireColors;
                case Element.Water:
                    return waterColors;
                case Element.Earth:
                    return earthColors;
                case Element.Air:
                    return airColors;
                case Element.Thunder:
                    return lightningColors;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }
        }

        [ContextMenu("Copy Colors To Gradient/All")]
        void CopyGradients()
        {
            CopyGradientAir();
            CopyGradientEarth();
            CopyGradientFire();
            CopyGradientLightning();
            CopyGradientWater();
        }
        
        [ContextMenu("Copy Colors To Gradient/Air")]
        void CopyGradientAir()
        {
            airColors.CopyColorsToMainGradient();
        }
        
        [ContextMenu("Copy Colors To Gradient/Water")]
        void CopyGradientWater()
        {
            waterColors.CopyColorsToMainGradient();
        }
        
        [ContextMenu("Copy Colors To Gradient/Fire")]
        void CopyGradientFire()
        {
            fireColors.CopyColorsToMainGradient();
        }
        
        [ContextMenu("Copy Colors To Gradient/Earth")]
        void CopyGradientEarth()
        {
            earthColors.CopyColorsToMainGradient();
        }
        
        [ContextMenu("Copy Colors To Gradient/Lightning")]
        void CopyGradientLightning()
        {
            lightningColors.CopyColorsToMainGradient();
        }
        
        [Serializable]
        public class ElementColors
        {
            public Color mainColor = Color.white;
            public Color secondaryColor= Color.white;
            
            public Gradient mainGradient = new Gradient()
            {
                alphaKeys = new[]
                {
                    new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, 0.5f),
                    new GradientAlphaKey(1, 1)
                },
                colorKeys = new[]
                {
                    new GradientColorKey(Color.white, 0),
                    new GradientColorKey(Color.white, 0.5f),
                    new GradientColorKey(Color.Lerp(Color.white, Color.black, 0.25f), 1)
                }
            };
            public Gradient secondaryGradient= new Gradient()
            {
                alphaKeys = new[]
                {
                    new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, 0.5f),
                    new GradientAlphaKey(1, 1)
                },
                colorKeys = new[]
                {
                    new GradientColorKey(Color.gray, 0),
                    new GradientColorKey(Color.gray, 0.5f),
                    new GradientColorKey(Color.gray, 1)
                }
            };

          
           public  void CopyColorsToMainGradient()
            {
                mainGradient = new Gradient()
                {
                    alphaKeys = new[]
                    {
                        new GradientAlphaKey(1, 0),
                        new GradientAlphaKey(1, 1)
                    },
                    colorKeys = new[]
                    {
                        new GradientColorKey(mainColor, 0),
                        new GradientColorKey(secondaryColor, 1)
                    }
                };
            }
        }
    }
}