using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DefaultNamespace
{
    public class StupidEnemyAI : MonoBehaviour
    {
        public Transform target;
        private CharacterState _state;
        [SerializeField]
        private float radius = 5f;

        private Vector3 _initialPosition = new Vector2(0,0);

        private void Awake()
        {
            _state = GetComponent<CharacterState>();
            _initialPosition = transform.position;
        }

        private void Update()
        { 
            
            
            if (IsTargetOutsideRadius())
            {
                _state.MovementInput.MoveInput = _initialPosition - transform.position;
            }
            else
            {
                _state.MovementInput.MoveInput =target.position - transform.position;
            }
            
            
        }

        private bool IsTargetOutsideRadius()
        {
            return Vector2.Distance(target.position, transform.position) > (radius *2);
        }
    }
}