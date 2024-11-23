using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board
{
    class TilemapController : MonoBehaviour, ITilemapController
    {
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
        }

        public bool IsTaken(Vector2Int position)
        {
            return _tilemap.HasTile((Vector3Int)position);
        }

        public bool IsFree(Vector2Int position)
        {
            return !_tilemap.HasTile((Vector3Int)position);
        }

        public TileBase Get(Vector2Int position)
        {
            return _tilemap.GetTile((Vector3Int) position);
        }

        public void Clear(Vector2Int position)
        {
            _tilemap.SetTile((Vector3Int)position, null);
        }

        public void Set(Vector2Int position, TileBase tile)
        {
            _tilemap.SetTile((Vector3Int)position, tile);
        }

        public void ClearAll()
        {
            _tilemap.ClearAllTiles();
        }
    }
}