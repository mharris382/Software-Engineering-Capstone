
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Damage
{
    public class DestroyTileOnCollision : MonoBehaviour
    {
        public bool limitToSpecificTiles = true;
        public TileBase[] tiles;


        private void OnCollisionEnter2D(Collision2D other)
        {
            var tm = other.collider.GetComponent<Tilemap>();
            if (tm == null)
                return;
            var normal = other.GetContact(0).normal;
            var impactPoint = other.GetContact(0).point;
            var cellPos = tm.WorldToCell(impactPoint - normal * 0.1f);
            var tile = tm.GetTile(cellPos);
            if (!limitToSpecificTiles)
            {
                tm.SetTile(cellPos, null);
            }
            else if(TileIsInList(tile))
            {
                tm.SetTile(cellPos, null);
            }
        }

        bool TileIsInList(TileBase tileToCheck)
        {
            if(tileToCheck == null)
                return false;
            foreach (var tile in tiles)
            {
                if(tile == tileToCheck)
                    return true;
            }
            return false;
        }
    }
}