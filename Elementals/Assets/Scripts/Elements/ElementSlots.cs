using System;
using UnityEngine;

namespace Elements
{
    
    public class ElementSlots : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 5)]
        private int numberOfSlots = 3;

        [SerializeField] private bool startWithElements;
        [SerializeField] private ElementContainer[] startingElements;

        




        private void Start()
        {
            var slots = new Slots(numberOfSlots);
            if (startWithElements && startingElements.Length > 0)
            {
                for (int j = 0; j < startingElements.Length; j++)
                {
                    if (-1==slots.TryAssignNewElement(startingElements[j].Element))
                    {
                        Debug.LogWarning($"Not Enough slots to assign all {startingElements.Length} starting elements");
                        break;
                    }
                }
            }
            ElementManager.Instance.AssignPlayerSlots(slots);
        }
        
      
    }
  public class Slots : IElementSlots
        {
            private int _numberOfSlots = 3;
            private int _emptySlots;

            private Element[] _elements;
            
            public Slots(int numberOfSlots)
            {
                _numberOfSlots = Mathf.Clamp( numberOfSlots, 1, 5);
                _emptySlots = _numberOfSlots;
                _elements = new Element[_numberOfSlots];
            }

            public bool CanAssignNewElement => _emptySlots > 0;
            public int AssignedSlots => _numberOfSlots - _emptySlots;
            public int TotalSlots => _numberOfSlots;
            public int EmptySlots => _emptySlots;
            

            public bool TryGetElementAtIndex(int index, out Element element)
            {
                element = (Element) 0;
                if (index >= AssignedSlots)
                    return false;
                element = _elements[index];
                return true;
            }

            
            public int TryAssignNewElement(Element element)
            {
                
                for (int i = 0; i < AssignedSlots; i++)
                {
                    var e = _elements[i];
                    if (e == element)
                    {
                        return i;
                    }
                }
                if (_emptySlots <= 0) return -1;
                var nextEmptyIndex = _numberOfSlots - _emptySlots;
                _emptySlots--;
                _elements[nextEmptyIndex] = element;
                OnElementAdded?.Invoke(nextEmptyIndex);
                return nextEmptyIndex;
            }

            public event ElementSlotsChanged OnElementAdded;
            
        }
    public interface IElementSlots
    {
        public bool CanAssignNewElement { get; }
        public int AssignedSlots { get; }
        
        public int EmptySlots { get; }
        public int TotalSlots { get; }
        
        /// <summary>
        ///  gets the element in the given slot index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool TryGetElementAtIndex(int index, out Element element);
        /// <summary>
        ///  adds a new element if there is space and the element is not already in the slots
        /// </summary>
        /// <param name="element"></param>
        /// <returns>returns the index of the element, if the element was not added or already exists the index will be -1</returns>
        public int TryAssignNewElement(Element element);

        public event ElementSlotsChanged OnElementAdded;
    }
    
    public delegate void ElementSlotsChanged(int changedIndex);
    
}