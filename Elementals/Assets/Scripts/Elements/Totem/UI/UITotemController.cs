using System;
using System.Collections;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Elements.Totem.UI
{
    public class UITotemController : MonoBehaviour
    {
        public ElementContainer playerElement;
        public ElementContainer uiSelectedElement;
        
        private TotemInputProcessor inputProcessorLogic;
        private ITotemInputHandler _inputHandler;

        private Coroutine waitForChange;
        private bool _uiDirty = false;


        public bool debugInput;
        public bool debugElement;
        private ReactiveProperty<bool> _uiVisible = new ReactiveProperty<bool>();
        public TotemStateData StateData
        {
            get;
            set;
        }
        public ITotemInputHandler InputHandler
        {
            get => _inputHandler ?? NullTotemInput.Instance;
            set => _inputHandler = value;
        }

        private void OnDestroy()
        {
            inputProcessorLogic?.Dispose();
        }

        public void SetUIActive(bool active)
        {
            if(active)OpenUI();
            else HideUI();
        }

        void HideUI()
        {
            if (_uiDirty)
            {
                Debug.Assert(inputProcessorLogic.CurrentSelection == uiSelectedElement.Element);
                
                playerElement.Element = uiSelectedElement.Element;//apply change to player
                StateData.ConsumeTotem(); //consume the totem, starts recharge timer
            }
            else if(waitForChange != null)
            {
                StopCoroutine(waitForChange);
            }
        }

        void OpenUI()
        {
            inputProcessorLogic.CurrentSelection = uiSelectedElement.Element = playerElement.Element;
            waitForChange = StartCoroutine(WaitForChange(playerElement.Element));
        }

        IEnumerator WaitForChange(Element initialElement, Action onBecameDirty = null)
        {
            _uiDirty = false;
            inputProcessorLogic.CurrentSelection = uiSelectedElement.Element = initialElement;
            while (!_uiDirty)
            {
                if (inputProcessorLogic.CurrentSelection != initialElement)
                {
                    onBecameDirty?.Invoke();
                    _uiDirty = true;
                }
                yield return null;
            }
        }

        private void Update()
        {
            if (inputProcessorLogic == null) return;
            _uiVisible.Value = StateData.CurrentState == TotemStates.InUse;
            inputProcessorLogic.State = StateData.CurrentState;
        }

        public void Initialize()
        {
            Debug.Assert(StateData != null);
            Debug.Assert(InputHandler != null);
            inputProcessorLogic = new TotemInputProcessor(uiSelectedElement, InputHandler, debugInput);//this will automatically update itself
            
            _uiVisible = new ReactiveProperty<bool>();
            _uiDirty = false;
            _uiVisible.TakeUntilDestroy(this).Subscribe(SetUIActive);
            StateData.InRangeStream.TakeUntilDestroy(this).Subscribe(inRange =>
            {
                if (inRange)
                {
                    //Debug.Log("UI Visible = true");
                    inputProcessorLogic.CurrentSelection = uiSelectedElement.Element = playerElement.Element;
                    //_uiVisible.Value = true;
                }
                else
                {
                    // Debug.Log("UI Visible = false");
                    //_uiVisible.Value = false;
                }
            });
        }

        /// <summary>
        /// handles processing the input and tracking the state of the selected element
        /// </summary>
        public class TotemInputProcessor : IDisposable
        {
            private int _index;
            private ElementContainer[] _elements;
            private ElementContainer _selection;
            
            
            public Element CurrentSelection
            {
                get => _elements[_index].Element;
                set => _index = (int)value;
            }

            public TotemStates State
            {
                get;
                set;
            }
            private IDisposable _disposable;

            public TotemInputProcessor(ElementContainer uiElement, ITotemInputHandler inputHandler, bool debugInput = false)
            {
                //Init all internal dependencies
                var compositeDisposable = (CompositeDisposable)(_disposable = new CompositeDisposable());
                _selection = uiElement;
                this._elements = new ElementContainer[5] {
                    ElementContainer.Fire,
                    ElementContainer.Water,
                    ElementContainer.Thunder,
                    ElementContainer.Earth,
                    ElementContainer.Air
                };
                


                //handle ui input while UI is in use
                var activeInputStream = Observable.EveryUpdate()
                    .Where(t => State == TotemStates.InUse)
                    .Select(_ => inputHandler.GetElementSelectionInputAxis());
                
                activeInputStream.Subscribe(OnUIInputAxis).AddTo(compositeDisposable);
                if (debugInput) activeInputStream.Subscribe(value => Debug.Log($"Input Stream= {value}")).AddTo(compositeDisposable);

            }
            
            void OnUIInputAxis(int inputAxis)
            {
                if (inputAxis > 0) NextElement();
                else if (inputAxis < 0) PrevElement();
            }
            
            void NextElement()
            {
                _index++;
                _index %= 5;
                _selection.Element = _elements[_index].Element;
            }

            void PrevElement()
            {
                _index--;
                if (_index < 0) 
                    _index = 4;
                _selection.Element = _elements[_index].Element;
            }


            public void Dispose()
            {
                _disposable?.Dispose();
            }
        }
    }
}