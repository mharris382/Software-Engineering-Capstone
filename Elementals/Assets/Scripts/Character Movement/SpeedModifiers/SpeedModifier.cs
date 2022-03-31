using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedModifier : IComparable<SpeedModifier>
{
    public float value = 0;
    
    [Range(0, 3)]
    public int priority = 1;
    public Type modifierType;
    public enum Type
    {
        Additive = 1,
        Multiplier = 2,
        Overwrite = 3,
        Maximum = 4,
        Minimum = 5
    }
    
    public int CompareTo(SpeedModifier other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var p = priority.CompareTo(other.priority);
        if (p == 0)
        {
            int i = (int) modifierType;
            int i2 = (int) other.modifierType;
            return i.CompareTo(i2);
        }
        return p;
    }

    public void ModifySpeed(ref float speed)
    {
        switch (modifierType)
        {
            case Type.Additive:
                speed += value;
                break;
            case Type.Multiplier:
                speed *= value;
                break;
            case Type.Overwrite:
                speed = value;
                break;
            case Type.Maximum:
                speed = Mathf.Max(speed, value);
                break;
            case Type.Minimum:
                speed = Mathf.Min(speed, value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static float ApplyModifierStack(float baseSpeed, params SpeedModifier[] modifiers)
    {
        var lom = new List<SpeedModifier>();
        lom.AddRange(modifiers);
        lom.Sort();
        float speed = baseSpeed;
        ModifySpeed(ref speed, lom);
        return speed;
    }

    public static void ModifySpeed(ref float speed, List<SpeedModifier> modifiers)
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].ModifySpeed(ref speed);
        }
    }
}


