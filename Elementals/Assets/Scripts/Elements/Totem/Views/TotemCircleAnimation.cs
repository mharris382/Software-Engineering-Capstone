using System;
using DG.Tweening;
using Elements.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class TotemCircleAnimation : MonoBehaviour
{
    
    [Header("Animation Settings")]
    [Range(0, 1)]
    public float minRadius = 0;

    [Range(5, 25)]
    public float maxRadius = 10;

    [SerializeField]  private CircleTransition expandTransition;
    [SerializeField]  private CircleTransition contractTransition;

    [SerializeField, Range(0,1)]
    private float activationThreshold = 0.5f;
    
    [Header("Events")]
    public UnityEvent<float> updateRadius;
    public UnityEvent<bool> updateActive;
    public UnityEvent<float> updatePercentExpanded;
    
    public UnityEvent<Color> updateElementColor;
    
    private float _percentExpanded;
    private float _radius;
    private bool _isActive;

    private Tween transition;
    
    [Serializable]
    public class CircleTransition
    {
        public float duration = 3;
        public Ease ease = Ease.Linear;
    }
    
    private float radius
    {
        get => _radius;
        set
        {
            if (value != _radius)
            {
                _radius = value;
                updateRadius?.Invoke(_radius);
            }
        }
    }

    private bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                updateActive?.Invoke(_isActive);
            }
        }
    }

    private float PercentExpanded
    {
        get => _percentExpanded;
        set
        {
            value = Mathf.Clamp01(value);
            if (_percentExpanded != value)
            {
                _percentExpanded = value;
                updatePercentExpanded?.Invoke(_percentExpanded);
            }
        }
    }


    public void SetPercentExpanded(float percent)
    {
        PercentExpanded = percent;
        radius = Mathf.Lerp(minRadius, maxRadius, PercentExpanded);
        IsActive = PercentExpanded > activationThreshold;
    }

    public void SetRadius(float radius)
    {
        radius = Mathf.Clamp(radius, minRadius, maxRadius);
        PercentExpanded = Mathf.InverseLerp(minRadius, maxRadius, radius);
        IsActive = PercentExpanded > activationThreshold;
    }


    public float GetTransitionDuration(float from, float to, float current, float totalDuration)
    {
        current = Mathf.Clamp(current, from, to);
        float t = Mathf.InverseLerp(from, to, current);
        return t * totalDuration;
    }
    public float GetTransitionDurationCurved(float from, float to, float current, float totalDuration, AnimationCurve curve)
    {
        current = Mathf.Clamp(current, from, to);
        float t = Mathf.InverseLerp(from, to, current);
        return curve.Evaluate(t) * totalDuration;
    }

    public float GetContractDuration()
    {
        float from = maxRadius;
        float to = minRadius;
        float current = radius;
        float totalDuration = contractTransition.duration;
        return GetTransitionDuration(from, to, current, totalDuration);
    }
    public float GetExpandDuration()
    {
        float from = minRadius;
        float to = maxRadius;
        float current = radius;
        float totalDuration = expandTransition.duration;
        return GetTransitionDuration(from, to, current, totalDuration);
    }



    public void ExpandCircle(bool reset)
    {
        if (reset) SetRadius(minRadius);
        float duration = GetExpandDuration();
    }

    public void ContractCircle(bool reset)
    {
        if (reset) SetRadius(maxRadius);
        float duration = GetContractDuration();
    }

    private Color target;
    private Color current;

    void UpdateColor()
    {
        
    }

}