using UnityEngine;

namespace Utilities
{
    public class FlipOnVelocity : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _defaultScale = transform.localScale;
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var velX = _rb.velocity.x;
            var absVelX = Mathf.Abs(velX);
            if(absVelX < 0.1f)
                return;
            if (velX > 0.1f)
            {
                transform.localScale = new Vector3(_defaultScale.x, _defaultScale.y, _defaultScale.z);
            }
            else
            {
                transform.localScale = new Vector3(_defaultScale.x, -_defaultScale.y, _defaultScale.z);
            }
        }
    }
}