using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetectionTrigger : MonoBehaviour
{
    public bool ignorePlayerTriggerCollider = true;
    private bool detected;
    public UnityEvent<bool> onPlayerDetectionChanged;
    
    public bool IsPlayerDetected
    {
        get => detected;
        set
        {
            if (detected != value)
            {
                detected = value;
                onPlayerDetectionChanged?.Invoke(value);
            }
        }
    }

    private HashSet<Collider2D> _playerColliders = new HashSet<Collider2D>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ignorePlayerTriggerCollider && other.isTrigger) return;
        if (other.attachedRigidbody == null) return;
        if (other.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            if (_playerColliders.Count == 0)
            {
                IsPlayerDetected = true;
            }
            _playerColliders.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody == null) return;
        if (_playerColliders.Contains(other))
        {
            _playerColliders.Remove(other);
            if (_playerColliders.Count == 0)
            {
                IsPlayerDetected = false;
            }
        }
    }

    private void OnDisable()
    {
        _playerColliders.Clear();
        IsPlayerDetected = false;
    }
}