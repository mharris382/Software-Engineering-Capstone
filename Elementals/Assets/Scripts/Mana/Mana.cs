using System;
using System.Collections;
using System.Collections.Generic;
using Elements.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Mana
{
    /// <summary>
    /// represents mana pickup in the game world.  stores 1 unit of mana
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mana : MonoBehaviour
    {
        public Element element;

    }


#if UNITY_EDITOR
    [CanEditMultipleObjects()]
    [CustomEditor(typeof(Mana))]
    public class ManaEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
           
            var prevBgColor = GUI.backgroundColor;
            var prevColor = GUI.color;
            
            var mana = target as Mana;
            var element = mana.element;
            var color = ElementColorPalettes.GetColor(element);
            var secondaryColor = ElementColorPalettes.GetSecondaryColor(element);
            GUI.backgroundColor = secondaryColor;
            GUI.contentColor = color;
            
            base.OnInspectorGUI();
            
            
            GUI.backgroundColor = prevBgColor;
            GUI.color = prevColor;
        }

    }

#endif
}