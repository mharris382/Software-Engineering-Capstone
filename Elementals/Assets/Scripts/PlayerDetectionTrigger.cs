using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// reusable class which is used to detect whether the player is in a certain area or not. Must be attached to a trigger collider
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class PlayerDetectionTrigger : MonoBehaviour
{
    public bool ignorePlayerTriggerCollider = true;

    [Tooltip("Event called when the player enters/exits the trigger area.")]
    public UnityEvent<bool> onPlayerDetectionChanged;


    
    private bool _detected;
    private HashSet<Collider2D> _playerColliders = new HashSet<Collider2D>();

    /// <summary>
    /// true if the player is in the trigger area, otherwise false
    /// </summary>
    public bool IsPlayerDetected
    {
        get => _detected;
        set
        {
            if (_detected != value)
            {
                _detected = value;
                onPlayerDetectionChanged?.Invoke(value);
            }
        }
    }

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