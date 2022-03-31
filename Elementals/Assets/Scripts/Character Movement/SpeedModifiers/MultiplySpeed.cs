using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMultiplier : MonoBehaviour, ISpeedModifyingObject
{
    [Range(0.1f, 2)]
    public float amount = 1.5f;

    private void Awake()
    {
        Debug.Assert(gameObject.CompareTag("SpeedModifier"));
    }

    public IEnumerable<SpeedModifier> GetSpeedModifiers()
    {
        yield return new SpeedModifier()
        {
            value = amount,
            priority = 1,
            modifierType = SpeedModifier.Type.Multiplier
        };
    }
}