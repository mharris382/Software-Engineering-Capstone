using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(HealthState))]
    public class HealthStateEditor : UnityEditor.Editor
    {
        private SerializedProperty _propMaxValue;
        
        private SerializedProperty _propContainer;
        private SerializedProperty _propElement;


        private bool _showEvents;
        private SerializedProperty _propHealEvent;
        private SerializedProperty _propKillEvent;
        private SerializedProperty _propDamageEvent;

        private void OnEnable()
        {
            _propMaxValue = serializedObject.FindProperty("maxValue");
            
            _propContainer = serializedObject.FindProperty("container");
            _propElement = serializedObject.FindProperty("element");
            
            _propHealEvent = serializedObject.FindProperty("OnHealed");
            _propKillEvent = serializedObject.FindProperty("OnKilled");
            _propDamageEvent = serializedObject.FindProperty("OnDamaged");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_propMaxValue);
            
            
            
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            var c1 = GUI.backgroundColor;
            var c0 = GUI.contentColor;
            GUI.backgroundColor = Color.green/0.7f;
            GUI.contentColor = Color.green;
            
            EditorGUILayout.LabelField("Element", GUILayout.MaxWidth(120));
            EditorGUILayout.PropertyField(_propContainer, new GUIContent(), GUILayout.MaxWidth(300));
            if (_propContainer.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(_propElement, new GUIContent(), GUILayout.MaxWidth(200));
            }
            else
            {
                var elementContainer = _propContainer.objectReferenceValue as ElementContainer;
                var value = elementContainer.Element.ToString();
                EditorGUILayout.LabelField(value, GUILayout.MaxWidth(50));
            }
            
            GUILayout.EndHorizontal();
            
            GUI.backgroundColor = c1;
            GUI.contentColor = c0;
            
            GUILayout.Space(20);
            
            
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            _showEvents = EditorGUILayout.Foldout(_showEvents, "Events");
            if (_showEvents)
            {
                EditorGUILayout.PropertyField(_propHealEvent);
                EditorGUILayout.PropertyField(_propKillEvent);
                EditorGUILayout.PropertyField(_propDamageEvent);
            }
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}