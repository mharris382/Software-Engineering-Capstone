using Elements;
using UnityEngine;

namespace AI
{
    public class PlayerAITarget : MonoBehaviour, IAITarget, IElementalDependent
    {
        public ElementContainer playerElement;
        private Transform _playerTransform;
        private Rigidbody2D _playerRB;

        public Transform playerTransform
        {
            set
            {
                _playerTransform = value;
                if (value != null) 
                    _playerRB = _playerTransform.GetComponent<Rigidbody2D>();
            }
            get => _playerTransform;
        }

        public bool HasTarget => _playerTransform != null;

        public Vector2 TargetPosition => HasTarget ? _playerTransform.position : transform.position;

        public Vector2 TargetVelocity => HasTarget ? _playerRB.velocity : Vector2.zero;
        public Element TargetElement => HasTarget ? playerElement.Element : Element;
        public Element Element { get; set; }
    }
}