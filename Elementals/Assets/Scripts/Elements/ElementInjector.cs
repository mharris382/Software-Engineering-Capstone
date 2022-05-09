using UnityEngine;
   
#if UNITY_EDITOR
using Elements.Utilities;
using UnityEditor;
    
    
#endif
namespace Elements
{
    /// <summary>
    /// single entry point for an entities element.  Will inject that element into all IElementDependents in children
    /// Custom editor uses colors to make it easier to tell which element this is 
    /// </summary>
    [DisallowMultipleComponent]
    public class ElementInjector : MonoBehaviour
    {
        public Element element;

        private IElementalDependent[] _elementalDependents;
                
        private void OnEnable() => InjectElementToDependents();

        public void InjectElementToDependents()
        {
            this._elementalDependents = GetComponentsInChildren<IElementalDependent>();
            foreach (var elemental in _elementalDependents)
            {
                elemental.Element = element;
            }
        }
    }
    
    #if UNITY_EDITOR

    
    [CanEditMultipleObjects()]
    [CustomEditor(typeof(ElementInjector))]
    public class ElementInjectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
           
            var prevBgColor = GUI.backgroundColor;
            var prevColor = GUI.color;
            
            var elementInjector = target as ElementInjector;
            var element = elementInjector.element;
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