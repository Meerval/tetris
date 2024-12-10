using System.Collections.Generic;
using Board.Pieces;
using Systems.Pretty;
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
        
        public ITilemapController Load(string codes, RectInt bounds)
        {
            if (codes.Length != bounds.height * bounds.width)
            {
                Debug.LogError("Tilemap codes cannot be converted to ITilemapController in right way:\n" +
                               $"Expected size='{bounds.height * bounds.width}' but actual='{codes.Length}'");
            }

            TileBase InitTile(char c)
            {
                return TileProvider.Instance.Get(ETileParser.Parse(c));
            }

            int charIdx = 0;

            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Set(new Vector2Int(x, y), InitTile(codes[charIdx]));
                    charIdx++;
                }
            }

            return this;
        }

        public string ToStorableString(RectInt bounds)
        {
            List<char> map = new List<char>();
            
            char GetTileCode(int x, int y)
            {
                TileBase tile = Get(new Vector2Int(x, y));
                return tile == null ? '0' : tile.name[0];
            }

            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    map.Add(GetTileCode(x, y));
                }
            }

            if (map.Count != bounds.height * bounds.width)
            {
                Debug.LogError("Tilemap weren't converted to string in right way:\n" +
                               $"Expected size='{bounds.height * bounds.width}' but actual='{map.Count}'\n" +
                               $"Result is:{new PrettyArray<char>(map)}");
            }

            return string.Concat(map);
        }
    }
}