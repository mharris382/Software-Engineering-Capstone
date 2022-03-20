using Elements;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ElementEntity))]
    public class ElementEntityEditor : UnityEditor.Editor
    {
        private SerializedProperty _propContainer;
        private SerializedProperty _propElement;
       
        
        private void OnEnable()
        {
            _propContainer = serializedObject.FindProperty("container");
            _propElement = serializedObject.FindProperty("element");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal(EditorStyles.largeLabel);
            {
                EditorGUILayout.LabelField("Element", GUILayout.MaxWidth(120));
                EditorGUILayout.PropertyField(_propContainer, new GUIContent(), GUILayout.MaxWidth(300));
                if (_propContainer.objectReferenceValue == null)
                {
                    EditorGUILayout.PropertyField(_propElement, new GUIContent(), GUILayout.MaxWidth(200));
                }
                else
                {
                    var elementContainer = _propContainer.objectReferenceValue as ElementContainer;
                    var value = elementContainer != null ? elementContainer.Element.ToString() : "ERROR";
                    EditorGUILayout.LabelField(value, GUILayout.MaxWidth(200));
                }
            } GUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}