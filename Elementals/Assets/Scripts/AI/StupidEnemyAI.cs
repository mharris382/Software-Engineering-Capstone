using UnityEngine;

namespace AI
{
    /// <summary>
    /// prototoype enemy behavior. follows player when in range, otherwise returns to it's initial position
    /// </summary>
    public class StupidEnemyAI : MonoBehaviour
    {
        private bool autoTargetPlayer = true;
       
        private CharacterState _state;
       
        [Tooltip("The target to attack, if not set will automatically target the player")]
        [SerializeField] private Transform target;
        
        [Tooltip("The distance at which the enemy will attack the player")]
        [SerializeField]
        private float radius = 5f;

        [SerializeField, Tooltip("The point from which the enemy will measure the distance to the player. if null, the enemy will use their own transform.")]
        private Transform radiusCenter;
        
        private Vector3 _initialPosition = new Vector2(0,0);

        public float Radius
        {
            get => radius;
            set => radius = value;
        }

        public void AssignRadiusCenter(Transform center)
        {
            radiusCenter = center;
        }
        public void ClearRadiusCenter()
        {
            radiusCenter = null;
        }
        public bool HasRadiusCenter()
        {
            return radiusCenter != null;
        }
        public Transform RadiusCenter => radiusCenter ? radiusCenter : transform;

        public Transform Target
        {
            get => target;
            set => target = value;
        }


        protected virtual void Awake()
        {
            _state = GetComponent<CharacterState>();
            _initialPosition = transform.position;
            AutoTargetPlayerTransform();
        }

        protected virtual Vector2 GetSteeringDestination()
        {
            if (IsTargetOutsideRadius()) {
                return _initialPosition;
            }
            else {
                return target.position;
            }
        }

        protected virtual void Update()
        { 
            AutoTargetPlayerTransform(); //rider gives expensive warning, but in practice it is only expensive if no player exists in the scene
            var steeringDestination = GetSteeringDestination();
            SteerTowards(steeringDestination);
        }

        private void SteerTowards(Vector3 targetPosition)
        {
            _state.MovementInput.MoveInput = targetPosition - transform.position;
        }

        protected bool IsTargetOutsideRadius()
        {
            return Vector2.Distance(target.position, RadiusCenter.position) > (radius *2);
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