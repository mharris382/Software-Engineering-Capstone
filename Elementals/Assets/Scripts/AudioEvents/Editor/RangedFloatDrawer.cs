using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(RangedFloat))]
public class RangedFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        float minValue = minProp.floatValue;
        float maxValue = maxProp.floatValue;


        float rangeMin = 0;
        float rangeMax = 1;

        var ranges = (MinMaxRangeAttribute[]) fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
        if(ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        var minLabelRect = new Rect(position);
        minLabelRect.width = 40f;
        GUI.Label(minLabelRect, new GUIContent(minValue.ToString("F2")));
        position.xMin += 40f;

        var maxLabelRect = new Rect(position);
        maxLabelRect.xMin = maxLabelRect.xMax - 40f;
        GUI.Label(maxLabelRect, new GUIContent(maxValue.ToString("F2")));
        position.xMax -= 40f;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
        if (EditorGUI.EndChangeCheck())
        {
            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }
    }
}

