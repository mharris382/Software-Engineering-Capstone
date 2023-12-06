using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Damage
{
    public class DestroyTileOnOverlap : MonoBehaviour
    {
        public bool limitToSpecificTiles = true;
        public TileBase[] tiles;
        public Vector3[] checkPoints = new [] {
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0),
            new Vector3(-1, 1, 0),
        };

        public UnityEvent<Vector3> onTileDestroyed;
        
        [Header("Gizmos")]
        public Color drawColor = Color.red;
        public Vector3 drawSize = Vector3.one;

        private void OnTriggerStay2D(Collider2D other)
        {
            var tm = other.GetComponent<Tilemap>();
            if (tm == null)
                return;
            foreach (Vector3 pnt in checkPoints)
            {
                if(checkPoints ==null)continue;
                var pos = transform.TransformPoint(pnt);
                var cellPos = tm.WorldToCell(pos);
                var tile = tm.GetTile(cellPos);
                if (!limitToSpecificTiles)
                {
                    tm.SetTile(cellPos, null);
                    onTileDestroyed?.Invoke(tm.GetCellCenterWorld(cellPos));
                }
                else if(TileIsInList(tile))
                {
                    tm.SetTile(cellPos, null);
                    onTileDestroyed?.Invoke(tm.GetCellCenterWorld(cellPos));
                }
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


        private void OnDrawGizmos()
        {
            foreach (var checkPoint in checkPoints)
            {
                if(checkPoints ==null)continue;
                Gizmos.color = drawColor;
                var pos = transform.TransformPoint(checkPoint);
                Gizmos.DrawWireCube(pos, drawSize);
            }
        }
    }
}