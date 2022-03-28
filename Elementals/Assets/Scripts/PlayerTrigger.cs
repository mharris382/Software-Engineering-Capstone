using UnityEngine;
using UnityEngine.Events;

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