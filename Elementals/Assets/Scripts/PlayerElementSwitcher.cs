using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerElementSwitcher : MonoBehaviour
    {
        public ElementContainer activeElement;
        
        public IntContainer activeElementSlot;
        public ElementContainer[] elementSlots;
        
        public string switchElementButton = "Switch Element";

        public void Update()
        {
            if (Input.GetButtonDown(switchElementButton) || Input.GetKeyDown(KeyCode.F2))
            {
                var current = activeElementSlot.Value;
                current++;
                current %= elementSlots.Length;
                activeElementSlot.Value = current;
            }
            activeElement.Element = elementSlots[activeElementSlot.Value].Element;
        }
    }
}