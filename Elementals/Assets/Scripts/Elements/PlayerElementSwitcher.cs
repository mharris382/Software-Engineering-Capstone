using System;
using UnityEngine;

namespace Elements
{
    public class PlayerElementSwitcher : MonoBehaviour
    {
        public ElementContainer playerElement;
        public ElementContainer[] elementSlots;
        public BoolContainer hasElementContainer;

        private IElementSlots _fallbackSlots;
        
        private IElementSlots Slots
        {
            get
            {
                if (ElementManager.Instance.PlayerSlots != null)
                    return ElementManager.Instance.PlayerSlots;
                
                if (_fallbackSlots == null)
                {
                    _fallbackSlots = new Slots(elementSlots.Length);
                    for (int i = 0; i < elementSlots.Length; i++)
                    {
                        _fallbackSlots.TryAssignNewElement(elementSlots[i].Element);
                    }
                }
                return _fallbackSlots;
            }
        }
        
        
        private int _index = 0;
        private float _timestamp;
        [SerializeField]
        private float swapDelay = 0.5f;

        private int GetNumSlots()
        {
            return Slots.AssignedSlots;
        }

        private Element GetElementInSlot(int index)
        {
            if (Slots.TryGetElementAtIndex(index, out var element))
            {
                return element;
            }
            Debug.LogError($"Could not get element at index {index}");
            throw new IndexOutOfRangeException();
            return elementSlots[index].Element;
        }
        
        private void Awake()
        {
            if (playerElement == null) playerElement = Resources.Load<ElementContainer>("PlayerElement");
            if(playerElement == null) Debug.LogError("Couldn't find player element");
            ElementManager.Instance.OnPlayerSlotsAssigned += _ =>
            {
                _index = 0;
                UpdateActiveElement();
            };
        }

        void UpdateActiveElement()
        {
            hasElementContainer.Value = Slots.AssignedSlots > 0;
            if(!hasElementContainer.Value)
                return;
            var prevIndex = _index;
            _index = Mathf.Clamp(_index, 0, GetNumSlots() - 1);
            playerElement.Element = GetElementInSlot(_index);
        }

        private void Update()
        {
            if (!ElementManager.Instance.PlayerCanSwitchElements) 
                return;
            
            var input = Input.GetAxis("Mouse ScrollWheel");
            if (input > 0.1f)
            {
                SwitchToNextElement();
            }
            else if (input < -0.1f)
            {
                SwitchToPreviousElement();
            }
        }

        private void SwitchToPreviousElement()
        {
            _index -= 1;
            if (_index == -1) _index = GetNumSlots() - 1;
            playerElement.Element = GetElementInSlot(_index);
        }

     
        private void SwitchToNextElement()
        {
            _index += 1;
            if (_index == GetNumSlots()) _index = 0;
            playerElement.Element = GetElementInSlot(_index);
        }
    }
}