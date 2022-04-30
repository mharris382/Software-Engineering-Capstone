using System;
using UnityEngine;
using UniRx;
namespace Elements.Totem.Gameplay
{
    [Obsolete("Replaced with single totem controller")]
    public class GameplayTotemController : MonoBehaviour
    {
        public bool debug;
        
        private ITotemPlayerDetection _playerDetection;
        public ITotemPlayerDetection PlayerDetection
        {
            get => _playerDetection ?? NullTotemPlayerDetection.Instance;
            set => _playerDetection = value;
        }

        public TotemStateData StateData { get; set; }

        private float _lastUseTime = 0;

        public float RemainingChargeTime => StateData.IsCharging ? StateData.TotalRechargeTime - (Time.time - _lastUseTime) : 0;
        
        public float RemainingChargeTimeNormalized => RemainingChargeTime / StateData.TotalRechargeTime;
        
        
        public void Initialize()
        {
            Debug.Log("Initializing Totem Gameplay");
            PlayerDetection.PlayerIsDetected.TakeUntilDestroy(this).Subscribe(inRange => StateData.PlayerInRange = inRange);
            if (debug)
            {
                PlayerDetection.PlayerIsDetected.TakeUntilDestroy(this).Subscribe(inRange => Debug.Log($"InRange={inRange} : State= {StateData.CurrentState}"));
            }
            
            if (PlayerDetection is MonoBehaviour)
            {
                var mb = PlayerDetection as MonoBehaviour;
                mb.transform.localScale = Vector3.one * (StateData.MaxRadius*2);
            }
        }
    }
}