using System;
using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class EnemyAIState : MonoBehaviour, IAIState
    {
        
        public float maxMoveSpeed = 10;
        public float moveSpeed = 5;
        
        public Vector2 position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Vector2 velocity { get; set; }

        public float speed
        {
            get => moveSpeed;
            set => Mathf.Clamp(value, 0, maxMoveSpeed);
        }

        private AIState _state = AIState.Idle;
        
        public AIState State
        {
            get => _state;
            set => _state = value;
        }

        public IAITarget Target
        {
            get;
            set;
        }

        private void Start()
        {
            if (Target == null)
            {
                Target = GetComponentInChildren<IAITarget>();
                if (Target == null)
                {
                    Debug.LogError("Enemy AI must have IAITarget component on gameobject or child gameobject",this);
                    throw new NullReferenceException("Enemy AI must have IAITarget component on gameobject or child gameobject");
                }
            }
        }

        
    }
}