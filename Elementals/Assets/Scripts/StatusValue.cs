using System;
using UnityEngine;

public class StatusValue : MonoBehaviour
{
    [SerializeField]
    private float maxValue = 10;
    private float currentValue = 10;
    public float MaxValue => maxValue;

    public float CurrentValue
    {
        get => currentValue;
        set => currentValue = Mathf.Clamp(value, 0, maxValue);
    }
    
    [Range(-5, 5)]
    public float Generation;
    private void Awake()
    {
        currentValue = maxValue;
    }

    private void Update()
    {
        currentValue += Generation * Time.deltaTime;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
    }
    
    
}