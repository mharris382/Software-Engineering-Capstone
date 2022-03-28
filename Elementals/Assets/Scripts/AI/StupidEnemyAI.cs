using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StupidEnemyAI : MonoBehaviour
    {
        private bool autoTargetPlayer = true;
       [SerializeField] private Transform target;
        private CharacterState _state;
        [SerializeField]
        private float radius = 5f;

        private Vector3 _initialPosition = new Vector2(0,0);

        public Transform Target
        {
            get => target;
            set => target = value;
        }


        private void Awake()
        {
            _state = GetComponent<CharacterState>();
            _initialPosition = transform.position;
            AutoTargetPlayerTransform();
        }

        private void Update()
        { 
            AutoTargetPlayerTransform(); //rider gives expensive warning, but in practice it is only expensive if no player exists in the scene
            if (IsTargetOutsideRadius())
            {
                SteerTowards(_initialPosition);
            }
            else
            {
                SteerTowards(target.position);
            }
        }

        private void SteerTowards(Vector3 targetPosition)
        {
            _state.MovementInput.MoveInput = targetPosition - transform.position;
        }

        private bool IsTargetOutsideRadius()
        {
            return Vector2.Distance(target.position, transform.position) > (radius *2);
        }

        private void AutoTargetPlayerTransform()
        {
            if (autoTargetPlayer && target == null)
            {
                var go = GameObject.FindWithTag("Player");
                if (go == null)
                {
                    Debug.LogError("No Player found in scene.  Player GameObject must have the <b>Player</b> tag assigned!");
                    return;
                }

                target = go.transform;
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}