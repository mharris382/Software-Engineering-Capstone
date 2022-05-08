using UnityEngine;

namespace AI
{
    public interface IAIState
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float speed { get; set; }
        public AIState State { get; }
        public IAITarget Target { get; }
    }

    public enum AIState
    {
        Idle,
        Retreat,
        Aggressive,
        Attacking
    }
}