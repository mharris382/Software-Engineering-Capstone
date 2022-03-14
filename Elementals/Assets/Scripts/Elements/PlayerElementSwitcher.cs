using System;
using UnityEngine;

namespace Elements
{
    public class PlayerElementSwitcher : MonoBehaviour
    {
        public ElementContainer playerElement;
        public ElementContainer[] elementSlots;

        private int _index = 0;
        private float _timestamp;
        [SerializeField]
        private float swapDelay = 0.5f;
        private void Awake()
        {
            if (playerElement == null) playerElement = Resources.Load<ElementContainer>("PlayerElement");
            if(playerElement == null) Debug.LogError("Couldn't find player element");
        }

        private void Update()
        {
          
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
            if (_index == -1) _index = elementSlots.Length - 1;
            playerElement.Element = elementSlots[_index].Element;
        }

     
        private void SwitchToNextElement()
        {
            _index += 1;
            if (_index == elementSlots.Length) _index = 0;
            playerElement.Element = elementSlots[_index].Element;
        }
    }
}