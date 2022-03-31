using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StupidEnemyMover : MonoBehaviour
    {
       
        public float force = 2f;
        private Rigidbody2D _rb;
        private CharacterState _state;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _state = GetComponent<CharacterState>();
        }

        private void Update()
        {
            var direction = _state.MovementInput.MoveInput;  // target.position - transform.position;
            var f = direction.normalized * _state.CheckForSpeedModifiers(force);
            _rb.AddForce(f);
            
        }
    }
}