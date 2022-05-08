using System;
using Pathfinding;
using UnityEngine;

namespace AI.Attacks
{
    public class PhysicsFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform followTarget;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            var diff =(Vector2) followTarget.position - rb.position;
            rb.velocity = diff ;
        }
    }
}