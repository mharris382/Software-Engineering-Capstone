using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Elements.Totem.Gameplay
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TotemPlayerTriggerDetection : MonoBehaviour, ITotemPlayerDetection
    {
        private CircleCollider2D _collider;

        private CircleCollider2D circleCollider
        {
            get
            {
                if (_collider == null)
                {
                    _collider = GetComponent<CircleCollider2D>();
                    _collider.isTrigger = true;
                }
                return _collider;
            }
        }

        private HashSet<Collider2D> _playerColliders = new HashSet<Collider2D>();
        private ReactiveProperty<bool> _playerIsDetected = new ReactiveProperty<bool>(false);
        public ReadOnlyReactiveProperty<bool> PlayerIsDetected => _playerIsDetected.ToReadOnlyReactiveProperty();


        public bool debug;
        private void Awake()
        {
            if (debug)
            {
                PlayerIsDetected.TakeUntilDestroy(this).Subscribe(detected =>
                {
                    if(detected)
                        Debug.Log("Player was detected!");
                    else
                        Debug.Log("Player was undetected!");
                });
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
            if (other.attachedRigidbody.gameObject.CompareTag("Player"))
            {
                if (_playerColliders.Count == 0)
                {
                    _playerIsDetected.Value = true;
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
                    _playerIsDetected.Value = false;
                }
            }
        }
    }
}