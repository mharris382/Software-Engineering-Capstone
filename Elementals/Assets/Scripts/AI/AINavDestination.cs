using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class AINavDestination : MonoBehaviour
    {
        
        public UnityEvent<Transform> onDestinationTransformCreated;
        private Transform _movementDestination;

        public Vector2 Destination
        {
            get => _movementDestination.position;
            set => _movementDestination.position = value;
        }

        
        private void Awake()
        {
            var go = new GameObject($"{name}-Movement Destination");
            _movementDestination = go.transform;
            onDestinationTransformCreated?.Invoke(_movementDestination);
        }
    }
}