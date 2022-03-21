using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using UnityEngine.Events;


public class TriggerArea : MonoBehaviour
    {
     

        [SerializeField] private TriggerMode mode = TriggerMode.Always;
        [SerializeField] private EventMask events = EventMask.Enter | EventMask.Exit;

#if ODIN_INSPECTOR
        [ShowIf("UseEnter")]
#endif
        public UnityEvent OnEnter;
#if ODIN_INSPECTOR
        [ShowIf("UseExit")]
#endif
        public UnityEvent OnExit;
        
        

        [Flags]
        public enum EventMask
        {
            Enter = 1 << 1,
            Exit = 1 << 2,
            Stay = 1 << 3
        }

        public enum TriggerMode
        {
            Always,
            OncePerRun,
            OncePerDeath
        }


        private int _enterTriggerCount;
        private int _exitTriggerCount;
        
        private bool UseEnter => IsEvent(EventMask.Enter);
        private bool UseExit => IsEvent(EventMask.Exit);
        
        private void Awake()
        {
            _enterTriggerCount = 0;
            _exitTriggerCount = 0;
            if (mode == TriggerMode.OncePerRun)
            {
                Debug.LogWarning("Persistent Triggers are not yet implemented!");
                return;
            }
        }

        private void ResetSaveData()
        {
            if (mode != TriggerMode.OncePerRun)
                return;
            Debug.LogWarning("Persistent Triggers are not yet implemented!");
        }
        
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if ( IsPlayer(other) && IsEvent(EventMask.Enter))
            {
                if (ShouldTrigger(_enterTriggerCount))
                {
                    _enterTriggerCount++;
                    OnEnter?.Invoke();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsPlayer(other) && IsEvent(EventMask.Exit))
            {
                if (ShouldTrigger(_exitTriggerCount))
                {
                    _exitTriggerCount++;
                    OnExit?.Invoke();
                }
            }
        }

        private bool ShouldTrigger(int timeTriggered)
        {
            switch (mode)
            {
                case TriggerMode.Always:
                    return true;
                case TriggerMode.OncePerRun:
                case TriggerMode.OncePerDeath:
                    return timeTriggered < 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsEvent(EventMask target)
        {
            return (((int)events)&(int)target) > 0;
        }

        private static bool IsPlayer(Collider2D other)
        {
            return other.attachedRigidbody.CompareTag("Player");
        }
    }
