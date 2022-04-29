using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Obsolete("Replaced by TotemDisplayHandler")]
[RequireComponent(typeof(SpriteRenderer))]
public class ElementTotemVisuals : MonoBehaviour
{
    
      [SerializeField] private ElementContainer playerActiveElement;

      [SerializeField] private TotemElementVisual fireRune;
      [SerializeField] private TotemElementVisual waterRune;
      [SerializeField] private TotemElementVisual lightningRune;
      [SerializeField] private TotemElementVisual earthRune;
      [SerializeField] private TotemElementVisual airRune;

      [Range(0, 1)]
      public float runeAlpha = 1;

      
      
     private SpriteRenderer _baseSr;
     private Element _lastElement;
     private Dictionary<Element, TotemElementVisual> _visualLookup;
    

    private SpriteRenderer BaseSpriteRenderer
    {
        get
        {
            if (_baseSr == null) _baseSr = GetComponent<SpriteRenderer>();
            return _baseSr;
        }
    }
    
    [Serializable]
    private class TotemElementVisual
    {
        public Sprite sprite;
        public Color elementColor = Color.white;
       [HideInInspector] public Material material;
       
        
        public SpriteRenderer spriteRenderer;
        private Func<float> multiplier;


        public SpriteRenderer CreateSpriteRenderer(SpriteRenderer parent)
        {
            if (spriteRenderer != null)
            {
                Destroy(spriteRenderer.gameObject);
            }
            var parentTransform = parent.transform;
            spriteRenderer = Instantiate(parent, parentTransform.position, parentTransform.rotation, parentTransform);
            UpdateSpriteRenderer();
            return spriteRenderer;
        }

        
        
        public void UpdateSpriteRenderer()
        {
            spriteRenderer.color = elementColor;
            spriteRenderer.sprite = sprite;
            if (material != null) spriteRenderer.material = material;
        }

        public void SetTransparency(float alpha)
        {
            if (spriteRenderer == null)
            {
                return;
            }
            alpha = Mathf.Clamp01(alpha);
            var color = elementColor;
            if (multiplier != null)
                alpha *= multiplier();
            color.a = alpha;
            spriteRenderer.color = color;
        }

        public void SetAlphaMultiplier(Func<float> alpha)
        {
            this.multiplier = alpha;
        }
    }


    void InitializeElementSpriteRenderers(Element startingElement)
    {
        _lastElement = startingElement;
        
        _visualLookup = new Dictionary<Element, TotemElementVisual>();
        _visualLookup.Add(Element.Fire, fireRune);
        _visualLookup.Add(Element.Water, waterRune);
        _visualLookup.Add(Element.Thunder, lightningRune);
        _visualLookup.Add(Element.Earth, earthRune);
        _visualLookup.Add(Element.Air, airRune);

        float AlphaMultiplier() => runeAlpha;
        foreach (var keyValuePair in _visualLookup)
        {
            var element = keyValuePair.Key;
            var visual = keyValuePair.Value;
            
            var sr = visual.CreateSpriteRenderer(BaseSpriteRenderer);
            sr.name = $"Rune_{element}";
            visual.SetAlphaMultiplier(AlphaMultiplier);
            visual.SetTransparency(element == startingElement ? 1 : 0);
        }
    }
    
    
    
    private void Awake()
    {
        //_baseSr = GetComponent<SpriteRenderer>();
        //playerActiveElement.OnElementChanged += ChangeActiveElement;
        //InitializeElementSpriteRenderers(playerActiveElement.Element);
    }
    
    void ChangeActiveElement(Element newElement)
    {
        FadeVisual(_lastElement);
        ShowVisual(newElement);
        _lastElement = newElement;
    }
    
    SpriteRenderer GetElementSpriteRenderer(Element e) => _visualLookup[e].spriteRenderer;
    
    void FadeVisual(Element e) => FadeRune(_visualLookup[e]);
    void ShowVisual(Element e) => ShowRune(_visualLookup[e]);

    
    void FadeRune(TotemElementVisual rune)
    {
        rune.SetTransparency(0);
    }

    void ShowRune(TotemElementVisual rune)
    {
        rune.SetTransparency(1);
    }
 
    

    private void OnDestroy()
    {
        playerActiveElement.OnElementChanged -= ChangeActiveElement;
    }
}
