using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class UnityAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private EventFireMode eventFireMode = EventFireMode.Always;

    private int _timesEventFired;
    
    enum EventFireMode
    {
        Never,
        OnceEver,
        OncePerEnabled,
        Always
    }
    public UnityEvent onAnimationEvent;
    
    
    
    [UsedImplicitly]
    public void TriggerEvent()
    {
        onAnimationEvent?.Invoke();
    }
}