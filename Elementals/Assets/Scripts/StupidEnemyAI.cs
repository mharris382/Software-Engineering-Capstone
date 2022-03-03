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

            /*if (Vector2.Distance(target.position, transform.position) > radius)
            {
                _state.Input.MoveInput =target.position - transform.position;
            }
            else
            {
                 //_state.Input.MoveInput = new Vector2(0,0);
                 _state.Input.MoveInput = _initialPosition - transform.position;
            }*/
            
            if (Vector2.Distance(target.position, transform.position) > (radius *2))
            {
                _state.Input.MoveInput = _initialPosition - transform.position;
            }
            else
            {
                _state.Input.MoveInput =target.position - transform.position;
            }
            
            
        }
    }
}