using System;
using Elements;
using UnityEditor;
using UnityEngine;

namespace MyEditor
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
        private SerializedProperty _propOnPreMagicDamaged;
        private SerializedProperty _propOnPostMagicDamaged;

        private void OnEnable()
        {
            _propMaxValue = serializedObject.FindProperty("maxValue");
            
            _propContainer = serializedObject.FindProperty("container");
            _propElement = serializedObject.FindProperty("element");
            
            _propHealEvent = serializedObject.FindProperty("OnHealed");
            _propKillEvent = serializedObject.FindProperty("OnKilled");
            _propDamageEvent = serializedObject.FindProperty("OnDamaged");
            _propOnPreMagicDamaged = serializedObject.FindProperty("onPreMagicDamaged");
            _propOnPostMagicDamaged = serializedObject.FindProperty("onPostMagicDamaged");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_propMaxValue, new GUIContent("Max Health"));

            var health = target as HealthState;
            if(health.GetComponent<ElementInjector>()==null)
                DrawElementGUI();

            DrawEventsGUI();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawEventsGUI()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            _showEvents = EditorGUILayout.Foldout(_showEvents, "Events");
            if (_showEvents)
            {
                EditorGUILayout.PropertyField(_propHealEvent);
                EditorGUILayout.PropertyField(_propKillEvent);
                EditorGUILayout.PropertyField(_propDamageEvent);
                EditorGUILayout.PropertyField(_propOnPreMagicDamaged);
                EditorGUILayout.PropertyField(_propOnPostMagicDamaged);
            }

            GUILayout.EndVertical();
        }

        private void DrawElementGUI()
        {
            void DrawPropContainer()
            {
                EditorGUILayout.PropertyField(_propContainer, new GUIContent(), GUILayout.MaxWidth(300));
            }

            GUILayout.Space(5);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Element", GUILayout.MaxWidth(120));

            if (_propContainer.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(_propElement, new GUIContent(), GUILayout.MaxWidth(200));
                DrawPropContainer();
                GUILayout.EndHorizontal();
            }
            else
            {
                var propElementInContainer = _propContainer.FindPropertyRelative("element");
                if (propElementInContainer != null)
                {
                    Element value = (Element) propElementInContainer.enumValueIndex;
                    value = (Element) EditorGUILayout.EnumPopup("", value, GUILayout.MaxWidth(200));
                    propElementInContainer.enumValueIndex = (int) value;
                    DrawPropContainer();
                    GUILayout.EndHorizontal();
                }
                else
                {
                    DrawPropContainer();
                    GUILayout.EndHorizontal();
                    EditorGUI.indentLevel++;
                    var editor = CreateEditor(_propContainer.objectReferenceValue);
                    editor.OnInspectorGUI();
                    EditorGUI.indentLevel--;
                }
            }

            GUILayout.EndVertical();
        }
    }
}