using System;
using UnityEngine;
using UnityEngine.UI;

    public class UIElementSlotDisplay : MonoBehaviour
    {
        public Image onElementSlotImage;
        public Image offElementSlotImage;

        public ElementContainer[] elementSlot;
        public IntContainer activeElementSlot;

        bool transitioning = false;
        public ElementIcons elementIcons;
        
        [Serializable]
        public class ElementIcons
        {
            public Sprite fire;
            public Sprite water;
            public Sprite earth;
            public Sprite air;
            public Sprite lightning;

            public Sprite GetIcon(Element element)
            {
                switch (element)
                {
                    case Element.Fire:
                        return fire;
                        break;
                    case Element.Water:
                        return water;
                        break;
                    case Element.Earth:
                        return earth;
                        break;
                    case Element.Air:
                        return air;
                        break;
                    case Element.Thunder:
                        return lightning;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(element), element, null);
                }
            }
        }
        
        private void Update()
        {
            int cur = activeElementSlot.Value;
            int next = (activeElementSlot.Value + 1) % elementSlot.Length;
            var currentElement = elementSlot[cur].Element;
            var nextElement = elementSlot[next].Element;
            onElementSlotImage.sprite = elementIcons.GetIcon(currentElement);
            offElementSlotImage.sprite = elementIcons.GetIcon(nextElement);
        }
    }
