using System;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Elements.Totem
{
    public enum TotemStates
    {
        Charging,
        ReadyToUse,
        InUse
    }
    public class TotemStateData
    {
        private readonly TotemConfig config;
        private IDisposable disposable;
        private Subject<Unit> consumeUI= new Subject<Unit>();
        private ReactiveProperty<bool> _isCharging = new ReactiveProperty<bool>();
        private ReactiveProperty<bool> _playerInRange = new ReactiveProperty<bool>();
        
        public float TotalRechargeTime => config.rechargeDuration;
  
        public float MaxRadius => config.totemRadius;
        public float MinRadius => 0;








        public bool IsCharging
        {
            get => _isCharging.Value;
            set => _isCharging.Value = value;
        }

        public bool PlayerInRange
        {
            get=> _playerInRange.Value;
            set => _playerInRange.Value = value;
        }

 
        public TotemStateData(TotemConfig config)
        {
            this.config = config;
            consumeUI = new Subject<Unit>();
            _isCharging = new ReactiveProperty<bool>(false);
            _playerInRange = new ReactiveProperty<bool>(false);
        }
        
       public IObservable<TotemStates> GetStateStream() => _playerInRange.ZipLatest(_isCharging, GetCurrentState);

       static TotemStates GetCurrentState(bool playerInRange, bool isFullyCharged)
       {
           if (!isFullyCharged) return TotemStates.Charging;
           return playerInRange ? TotemStates.InUse : TotemStates.ReadyToUse;
       }

       public TotemStates CurrentState => GetCurrentState(_playerInRange.Value, !_isCharging.Value);

       
    
       public void ConsumeTotem()
       {
           //totem is already charging
           if (_isCharging.Value) return;
           _isCharging.Value = true;
           Observable.Timer(TimeSpan.FromSeconds(config.rechargeDuration)).First().Subscribe(_ => { }, () => _isCharging.Value = false);
       }
    

 
    
    // public class UIStateData
    // {
    //     private IntReactiveProperty _selectedElementIndex = new IntReactiveProperty();
    //     private Element[] _elements ;
    //     private readonly ElementContainer _uiSelectedElement;
    //     
    //     public UIStateData(ElementContainer playerElementContainer, ElementContainer uiElementContainer, IObservable<bool> playerInRange, IObservable<TotemChargeInfo> totemChargeState)
    //     {
    //         _elements = new Element[]
    //         {
    //             Element.Fire,
    //             Element.Water,
    //             Element.Thunder,
    //             Element.Earth,
    //             Element.Air
    //         };
    //
    //         this._uiSelectedElement = uiElementContainer;
    //         IObservable<bool> isUIinUse = playerInRange
    //             .ZipLatest(totemChargeState, (inRange, chargeInfo) => inRange && chargeInfo.isReadyToUse)
    //             .DistinctUntilChanged();
    //
    //         isUIinUse.Where(opened => opened)
    //             .Select(_ => (int) playerElementContainer.Element);
    //     }
    //
    //     private void OnUIOpened(Element currentElement)
    //     {
    //         _uiSelectedElement.Element = currentElement;
    //         
    //     }
    // }


    }
}
