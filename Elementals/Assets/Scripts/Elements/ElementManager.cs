using System;
using UnityEngine;

namespace Elements
{
    public delegate void ActiveElementChanged(GameObject elementalGameObject, Element previousElement, Element newElement);
    public class ElementManager : MonoBehaviour
    {
        public ElementContainer activeElement;
        private static ElementManager _instance;

        public static ElementManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<ElementManager>();
                    if (_instance == null)
                    {
                        var go = new GameObject("Element Manager");
                        _instance = go.AddComponent<ElementManager>();
                        _instance.activeElement = Resources.Load<ElementContainer>("PlayerElement");
                    }
                }
                return _instance;
            }
        }

        public bool PlayerCanSwitchElements
        {
            get; 
            set;
        }

        private IElementSlots _playerSlots;
        public IElementSlots PlayerSlots
        {
            get => _playerSlots;
            private set
            {
                if (value != null && _playerSlots!=value)
                {
                    _playerSlots = value;
                    OnPlayerSlotsAssigned?.Invoke(_playerSlots);
                }
            }
        }

        public event Action<IElementSlots> OnPlayerSlotsAssigned;

        public event ActiveElementChanged OnPlayerActiveElementChanged;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }


        public void AssignPlayerSlots(IElementSlots elementSlots)
        {
            _playerSlots = elementSlots;
            OnPlayerSlotsAssigned?.Invoke(elementSlots);
        }
    }
}