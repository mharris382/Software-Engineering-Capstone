using System;
using UniRx;
using UnityEngine;

namespace Elements.Totem.UI
{
    public class UITotemController : MonoBehaviour
    {
   

        public ElementContainer playerElement;
        public ElementContainer uiSelectedElement;
        
        private TotemUI _uiLogic;
        private ITotemInputHandler _inputHandler;

        
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
        
        public void Initialize()
        {
            Debug.Assert(StateData != null);
            Debug.Assert(InputHandler != null);
            
            IObservable<TotemStates> uiStateStream = StateData.GetStateStream();
            _uiLogic = new TotemUI(() => playerElement.Element, newElement => playerElement.Element = newElement, InputHandler, uiStateStream);
            
            _uiLogic.TakeUntilDestroy(this).Subscribe(element => uiSelectedElement.Element = element);
        }

        public class TotemUI : IObservable<Element>, IDisposable
        {
            private readonly Subject<Element> uiSelectionChanged;
            private int _index;
            private ElementContainer[] _elements;
            private IDisposable _disposable;
            
            public TotemUI(Func<Element> readActiveElement, Action<Element> writeActiveElement, ITotemInputHandler inputHandler, IObservable<TotemStates> uiState)
            {
                //Init all internal dependencies
                #region [Initialization]

                ReactiveProperty<Element> uiElement = new ReactiveProperty<Element>();
                var compositeDisposable = (_disposable = new CompositeDisposable()) as CompositeDisposable; //cast is needed b/c _disposable is interface
                uiSelectionChanged = new Subject<Element>();
                ElementContainer[] _element = new ElementContainer[5] {
                    ElementContainer.Fire,
                    ElementContainer.Water,
                    ElementContainer.Thunder,
                    ElementContainer.Earth,
                    ElementContainer.Air
                };
                

                #endregion

                //handle reading/writing logic for selected and active element 
                #region [Sync UI and gameplay logic]

                void ConfirmUIDecision()
                {
                    writeActiveElement(_element[_index].Element);
                }
                void SyncTotemWithActiveElement()
                {
                    //even though the player should only be able to change elements while totem is active, there may be more than one instance of the totem so the element may have been changed by a different totem
                    _index = (int) readActiveElement();
                    Debug.Log($"UI Element is now {(Element)_index}:{_index}");
                }
                
                uiState.DistinctUntilChanged().Where(t => t ==TotemStates.Charging).Subscribe(_ => ConfirmUIDecision()).AddTo(compositeDisposable);
                uiState.DistinctUntilChanged().Where(t => t !=TotemStates.Charging).Subscribe(_ => SyncTotemWithActiveElement()).AddTo(compositeDisposable);

                #endregion

                //handle ui input while UI is in use
                #region [UI Input]

                var activeInputStream = Observable.EveryUpdate().Select(_ => inputHandler.GetElementSelectionInputAxis()).DistinctUntilChanged();
                var nullInputStream = Observable.Never<int>();
                
                //create a meta-stream which emits an input stream when the ui state changes. emits the real stream when the UI is opened, and a null object stream otherwise 
                uiState.Select(state => state == TotemStates.InUse).DistinctUntilChanged() //when totem becomes in use start reading input axis
                    .Select(uiOpen => uiOpen ? activeInputStream : nullInputStream)
                    .Switch().Subscribe(OnUIInputAxis).AddTo(compositeDisposable);

                void OnUIInputAxis(int inputAxis)
                {
                    if (inputAxis > 0)
                        NextElement();
                    else if (inputAxis < 0)
                        PrevElement();
                }

                #endregion
            }

            void NextElement()
            {
                _index++;
                _index %= 5;
                uiSelectionChanged.OnNext(_elements[_index].Element);
            }

            void PrevElement()
            {
                _index--;
                if (_index < 0) 
                    _index = 4;
                uiSelectionChanged.OnNext(_elements[_index].Element);
            }


            public IDisposable Subscribe(IObserver<Element> observer) => uiSelectionChanged.Subscribe(observer);

            public void Dispose()
            {
                uiSelectionChanged?.Dispose();
                _disposable?.Dispose();
            }
        }
    }
}