using System;
using System.Collections;
using Elements.Totem.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Elements.Totem
{
    /// <summary>
    /// Main controller for all totem logic.  Handles all totem logic which is not view related.
    /// View related code is handled indirectly through the TotemDisplayHandler <see cref="totemDisplayHandler"/>.
    /// </summary>
    public class Totem : MonoBehaviour
    {
        [SerializeField] private ElementContainer _playerElement;
        
        [SerializeField] private ElementContainer uiElement;

        [SerializeField] private bool useElementSlots;
        [SerializeField] private IntContainer playerSelectedElementSlot;
        [SerializeField] private ElementContainer[] playerElementSlots;
        
        public TotemDisplayHandler totemDisplayHandler;
        public PlayerDetectionTrigger totemPlayerTrigger;

        private ElementContainer playerElement
        {
            get
            {
                if (!useElementSlots)
                {
                    return _playerElement;
                }
                var slot = playerElementSlots[playerSelectedElementSlot.Value];
                return slot;
            }
        }
        public float resetDuration = 50;

        public float activeRadius = 20;

        public TotemEvents events;
        [Serializable]
        public class TotemEvents
        {
            public UnityEvent OnOpened;
            public UnityEvent OnClosed;
            public UnityEvent OnConsumed;
            public UnityEvent<float> OnRadiusChanged;
        }

        private float _lastUseTime;
        private ITotemInputHandler _input;
        private UIUsage _totemUseage;


        private CircleCollider2D trigger;
        private bool ReadyToUse => (Time.time - _lastUseTime) > resetDuration;

        private bool IsInUse => _totemUseage != null && !_totemUseage.IsDisposed;

        private bool _isOpen;
        private void Start()
        {
            _input = new TotemMouseWheelInput();
            
            totemPlayerTrigger.onPlayerDetectionChanged.AddListener(OnPlayerEnteredZone);
            trigger = totemPlayerTrigger.GetComponent<CircleCollider2D>();
            _lastUseTime = float.MinValue;
        }

        enum TotemState
        {
            Charging,
            InUse,
            Ready
        }

        private TotemState state
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                }
            }
        }
        private TotemState _state;
        private void Update()
        {
            totemDisplayHandler.TargetRadius = trigger.radius =activeRadius;
            if (!ReadyToUse)
            {
                state = TotemState.Charging;
            }
            else
            {
                state = totemPlayerTrigger.IsPlayerDetected ? TotemState.InUse : TotemState.Ready;
            }

            switch (state)
            {
                case TotemState.Charging:
                    totemDisplayHandler.SetDisplayState(TotemDisplayHandler.DisplayState.Charging);
                    totemDisplayHandler.SetDisplayElement(playerElement.Element);
                    break;
                case TotemState.InUse:
                    totemDisplayHandler.SetDisplayState(TotemDisplayHandler.DisplayState.InUse);
                    totemDisplayHandler.SetDisplayElement(uiElement.Element);
                    break;
                case TotemState.Ready:
                    totemDisplayHandler.SetDisplayState(TotemDisplayHandler.DisplayState.Ready);
                    totemDisplayHandler.SetDisplayElement(playerElement.Element);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDestroy()
        {
            totemPlayerTrigger.onPlayerDetectionChanged.RemoveListener(OnPlayerEnteredZone);
        }

        void OnPlayerEnteredZone(bool entered)
        {
            if (entered)
            {
                if (_totemUseage != null && !_totemUseage.IsDisposed)
                {
                    _totemUseage.Dispose();
                    _totemUseage.IsKilled = true;
                }
                Debug.Assert(_totemUseage == null || _totemUseage.IsDisposed, "Player Entered");
                uiElement.Element = playerElement.Element;
                _totemUseage = new UIUsage(uiElement, _input, ConfirmElementChange);
                StartCoroutine(DoUI());
            }
        }
        
    
        IEnumerator DoUI()
        {
            while (!ReadyToUse)
            {
                yield return null;
            }

            if (!totemPlayerTrigger.IsPlayerDetected)
                yield break;
            
            while (totemPlayerTrigger.IsPlayerDetected && !_totemUseage.IsDisposed)
            {
                _totemUseage.Tick();
                yield return null;
            }
            _totemUseage.Dispose();
        }

        void ConfirmElementChange(Element newElement)
        {
            playerElement.Element = newElement;
            _lastUseTime = Time.time;
            totemDisplayHandler.SetDisplayState(TotemDisplayHandler.DisplayState.Charging);
            totemDisplayHandler.Consume();
        }
        
        public class UIUsage : IDisposable
        {
            private int index;
            private int changes;
            private Element[] elements;
            private Action<Element> consumeCallback;
            private readonly ElementContainer container;

            public bool IsKilled
            {
                get;
                set;
            }
            public bool IsDisposed { get; private set; }
            public Element CurrentElement
            {
                get => elements[index];
            }
            public UIUsage(ElementContainer uiElementContainer, ITotemInputHandler inputHandler, Action<Element> OnConsume)
            {
                this.container = uiElementContainer;
                IsKilled = false; 
                IsDisposed = false;
                this.consumeCallback = OnConsume;
                this.changes = 0;
                this.index = (int) uiElementContainer.Element;
                this.InputHandler = inputHandler;
                elements = new Element[]
                {
                    Element.Fire, Element.Water, Element.Thunder, Element.Earth, Element.Air
                };
            }

            public ITotemInputHandler InputHandler { get; set; }

            void ChangeNext()
            {
                if (IsKilled) return;
                if (index == 4) index = 0;
                else index++;
                changes++;
                container.Element = CurrentElement;
            }

            void ChangePrev()
            {
                if (IsKilled) return;
                if (index == 0) index = 4;
                else index--;
                changes++;
                container.Element = CurrentElement;
            }
            
            public void Tick()
            {
                if (IsKilled) return;
                int input = InputHandler.GetElementSelectionInputAxis();
                if (input == 0) return;
                if (input > 0)
                    ChangeNext();
                else
                    ChangePrev();
            }

            public void Dispose()
            {
                if (IsKilled) return;
                if (IsDisposed) return;
                IsDisposed = true;
                if (changes > 0)
                {
                    consumeCallback(CurrentElement);
                }
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.Lerp(Color.blue, Color.red, 0.5f);
            Gizmos.DrawWireSphere(transform.position, activeRadius);
        }
    }
}