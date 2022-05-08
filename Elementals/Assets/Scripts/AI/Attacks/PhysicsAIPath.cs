using System;
using Pathfinding;
using UnityEngine;

namespace AI.Attacks
{
    public class PhysicsAIPath : AIPath
    {
        private Rigidbody2D rb;
        public bool useForces;

        protected override void Awake()
        {
            
            rb = GetComponent<Rigidbody2D>();
            base.Awake();
        }

        public override void Move(Vector3 deltaPosition)
        {
            if (useForces)
                throw new NotImplementedException();
            else
                rb.velocity = deltaPosition;
        }
    }
}