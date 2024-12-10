using System.Collections.Generic;
using System.Text.RegularExpressions;
using Board.Pieces;
using Systems.Pretty;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board
{
    public class TilemapController : MonoBehaviour
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
            return _tilemap.GetTile((Vector3Int)position);
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

        public void Load(string codes, RectInt bounds)
        {
            string rgx = $"^[0CBOYGPR]{{{bounds.height * bounds.width}}}$";
            if (string.IsNullOrEmpty(codes))
            {
                Debug.LogError($"{name} codes cannot be converted to TilemapController because its null or empty");
                ClearAll();
                return;
            }
            
            if (!Regex.IsMatch(codes, rgx))
            {
                Debug.LogError($"{name} codes cannot be converted to TilemapController in right way:\n" +
                               $"String of codes='{codes}' isn't match to regex '{rgx}'");
            }

            TileBase InitTile(char ch)
            {
                return TileProvider.Instance.Get(ETileParser.Parse(ch));
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