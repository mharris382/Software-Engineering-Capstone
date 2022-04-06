using System;
using SOHandlers;
using UnityEngine;

namespace Damage
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerFallKillbox : MonoBehaviour
    {
        [SerializeField]
        private PlayerDeathHandler fallDeathHandler;

        private BoxCollider2D _boxCollider2D;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            if (rb.gameObject.CompareTag("Player"))
            {
                TriggerPlayerFallDeath(rb.gameObject);
            }
        }

        void TriggerPlayerFallDeath(GameObject playerGameObject)
        {
            fallDeathHandler.HandlePlayerDied(playerGameObject);
        }
        
        private void OnDrawGizmos()
        {
            if (_boxCollider2D == null) _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.offset = Vector2.zero;
            var center = _boxCollider2D.offset + (Vector2)transform.position;
            var size = _boxCollider2D.size * (Vector2)transform.localScale;
            
            var c = Color.red;
            Gizmos.color = c;
            Gizmos.DrawWireCube(center, size);
            c.a = 0.45f;
            Gizmos.color = c;
            Gizmos.DrawCube(center, size);
        }
    }
}