using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// similar behavior to PlayerDetectionTrigger , but does not track the detected state only fires event when the player
/// enters the trigger
/// <see cref="PlayerDetectionTrigger"/>
/// </summary>
public class PlayerTrigger : MonoBehaviour
{
    public UnityEvent onPlayerEntered;
    public bool ignorePlayerTriggerCollider = true;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ignorePlayerTriggerCollider && other.isTrigger) return;
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        if(rb.gameObject.CompareTag("Player"))
            onPlayerEntered?.Invoke();
    }
}