using System;
using UnityEngine;
using UnityEngine.Events;

namespace Damage
{
    [RequireComponent(typeof(CharacterState))]
    public class FallDamage : MonoBehaviour
    {
        public float minDamageHeight = 1;
        public float maxDamageHeight = 20;

        public float minDamageAmount = 3;
        public float maxDamageAmount = 50;



        public UnityEvent<float> OnFallDamage;
        private CharacterState _state;
        private bool _isFalling = false;
        private float _heightFell = 0;
        private float _peakHeight = 0;
        private void Awake()
        {
            _state = GetComponent<CharacterState>();
        }

        private void FixedUpdate()
        {
            if (_isFalling)
            {
                _heightFell = _peakHeight - transform.position.y;
                if (_state.Movement.IsJumping)
                {
                    _isFalling = false;
                }
                else if (_state.Movement.IsGrounded)
                {
                    _isFalling = false;
                    CheckForFallDamage();
                }
            }
            else
            {
                void CheckForFallingStarted()
                {
                    if (_state.Movement.IsGrounded || _state.Movement.IsJumping)
                    {
                        _heightFell = 0;
                        _isFalling = false;
                    }
                    else
                    {
                        _isFalling = true;
                        _peakHeight = transform.position.y;
                    }
                }
                CheckForFallingStarted();
            }
            
        }

        private void CheckForFallDamage()
        {
            throw new NotImplementedException();
        }


        private void OnDrawGizmosSelected()
        {
            var p0 = (Vector2)transform.position + (Vector2.down * minDamageHeight);
            var p1 = (Vector2)transform.position + (Vector2.down * maxDamageHeight);
            
        }
    }
}