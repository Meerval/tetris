using System.Collections.Generic;
using System.Linq;
using Board.Actions;
using Board.Pieces;
using Templates.Singleton;
using UnityEngine;

namespace Board.Data.UI
{
    public class PiecePredictor : MonoBehaviourSingleton<PiecePredictor>, IPredictor
    {
        [SerializeField] private TilemapController tilemapPrefab;
        [SerializeField] private PiecePrefabs piecePrefabs;

        private ITilemapController _tilemap;

        private Vector2Int _size;
        private RectInt _bounds;

        private IPredictedPiece _predictedPiece;
        private readonly Vector2Int _spawnPosition = new(0, 0);

        public void Start()
        {
            _size = Vector2Int.RoundToInt(GetComponent<SpriteRenderer>().size);
            _bounds = new RectInt(new Vector2Int(-_size.x / 2, -_size.y / 2), _size);

            _tilemap = Instantiate(tilemapPrefab, gameObject.transform);

            ((TilemapController)_tilemap).gameObject.name = "Tilemap";

            Debug.Log
            (
                $"PredictionGrid awoke with size of {_size.ToString()} and bounds {_bounds.ToString()}"
            );
        }
        
        public void OnEnable()
        {
            EventsHub.OnSpawnPiece.AddSubscriber(Predict);
        }

        public void OnDisable()
        {
            EventsHub.OnSpawnPiece.RemoveSubscriber(Predict);
        }

        public void Predict(IPiece currentPiece)
        {
            Clear();
            _predictedPiece = (IPredictedPiece) piecePrefabs.Execute().Peek();
            SetPiece();
        }

        private void Clear()
        {
            _tilemap.ClearAll();
        }

        private IEnumerable<Vector2Int> CalculateTilesPosition()
        {
            return _predictedPiece.CurrentShapeMap()
                .Select(cell => cell + _spawnPosition + _predictedPiece.MonitorOffset())
                .ToArray();
        }

        private void SetPiece()
        {
            foreach (Vector2Int tilePosition in CalculateTilesPosition())
            {
                _tilemap.Set(tilePosition, _predictedPiece.Tile());
            }
        }
    }
}