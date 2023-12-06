using UnityEngine;
using UnityEngine.Tilemaps;

namespace Damage
{
    public class TileOnCollision : MonoBehaviour
    {
        public TileBase tile;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var tm = other.collider.GetComponent<Tilemap>();
            if (tm == null)
                return;
            var normal = other.GetContact(0).normal;
            var impactPoint = other.GetContact(0).point;
            var cellPos = tm.WorldToCell(impactPoint + normal * 0.1f);
            tm.SetTile(cellPos, tile);
        }
    }
}