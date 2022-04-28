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


        public static ElementColors GetPalette(Element e) => ElementConfig.Instance.colorPalettes.GetElementColors(e);
        public static Color GetColor(Element e) => ElementConfig.Instance.colorPalettes.GetElementColors(e).mainColor;
        public static Gradient GetGradient(Element e) => ElementConfig.Instance.colorPalettes.GetElementColors(e).mainGradient;
        
        
        

        public static ElementColors FireColors => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Fire);
        public static ElementColors WaterColors => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Water);
        public static ElementColors LightningColors => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Thunder);
        public static ElementColors EarthColors => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Earth);
        public static ElementColors AirColors => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Air);
        
        public static Color FireColor => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Fire).mainColor;
        public static Color WaterColor => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Water).mainColor;
        public static Color LightningColor => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Thunder).mainColor;
        public static Color EarthColor => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Earth).mainColor;
        public static Color AirColor => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Air).mainColor;
        
        public static Gradient FireGradient => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Fire).mainGradient;
        public static Gradient WaterGradient => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Water).mainGradient;
        public static Gradient LightningGradient => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Thunder).mainGradient;
        public static Gradient EarthGradient => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Earth).mainGradient;
        public static Gradient AirGradient => ElementConfig.Instance.colorPalettes.GetElementColors(Element.Air).mainGradient;
        
        
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