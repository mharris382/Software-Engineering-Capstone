using System;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsEvents2D : MonoBehaviour
{
    public UnityEvent onCollisionEnter;
    public UnityEvent onTriggerEnter;
    
    
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
    
    
    private void OnEnable()
    {
        if(eventFireMode != EventFireMode.OnceEver)
            _timesEventFired = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (eventFireMode)
        {
            case EventFireMode.Never:
                return;
            case EventFireMode.OnceEver:
            case EventFireMode.OncePerEnabled:
                if (_timesEventFired > 0)
                    return;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _timesEventFired++;
        onTriggerEnter?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (eventFireMode)
        {
            case EventFireMode.Never:
                return;
            case EventFireMode.OnceEver:
            case EventFireMode.OncePerEnabled:
                if (_timesEventFired > 0)
                    return;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _timesEventFired++;
        onCollisionEnter?.Invoke();
    }
}