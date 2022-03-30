using UnityEngine;

namespace Elements
{
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
}