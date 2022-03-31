using System.Collections.Generic;
using UnityEngine;

public class MultiplySpeed : MonoBehaviour, IModifySpeed
{
    [Range(0.1f, 2)]
    public float amount = 1.5f;
    
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