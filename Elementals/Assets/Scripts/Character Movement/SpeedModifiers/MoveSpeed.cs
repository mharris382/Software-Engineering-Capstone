using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    
    
    private List<ISpeedModifyingObject> _modifyingObjects;


    public float GetModifiedSpeed(float baseSpeed)
    {
        if (_modifyingObjects == null || _modifyingObjects.Count == 0) {
            return baseSpeed;
        }
        float speed = baseSpeed;
        var modifiers = new List<SpeedModifier>();
        modifiers.AddRange(_modifyingObjects.SelectMany(t => t.GetSpeedModifiers()));
        if (modifiers.Count == 0){ return speed; }
        modifiers.Sort();
        SpeedModifier.ModifySpeed(ref speed, modifiers);
        return speed;
    }


    private void OnEnable()
    {
        FindModifierChildren();
    }

    private void OnTransformChildrenChanged()
    {
        FindModifierChildren();
    }

    private void FindModifierChildren()
    {
        _modifyingObjects = new List<ISpeedModifyingObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("SpeedModifier"))
            {
                var modObjects = GetComponents<ISpeedModifyingObject>();
                Debug.Assert(modObjects != null && modObjects.Length > 0,
                    "Object tagged as modifier is missing modifier component");
                if (modObjects.Length > 0)
                    _modifyingObjects.AddRange(modObjects);
            }
        }
    }
}