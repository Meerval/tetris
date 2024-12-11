using System.Collections.Generic;
using System.Linq;
using Board.Data;
using Board.Pieces;
using Templates.Singleton;
using UnityEngine;

namespace Board
{
    public class PiecePredictor : MonoBehaviourSingleton<PiecePredictor>, IPredictor
    {
        [SerializeField] private TilemapController tilemapPrefab;

        private TilemapController _tilemap;

        private Vector2Int _size;
        private RectInt _bounds;

        private IPredictedPiece _predictedPiece;
        private readonly Vector2Int _spawnPosition = new(0, 0);

        public void Start()
        {
            _size = Vector2Int.RoundToInt(GetComponent<SpriteRenderer>().size);
            _bounds = new RectInt(new Vector2Int(-_size.x / 2, -_size.y / 2), _size);

            _tilemap = Instantiate(tilemapPrefab, gameObject.transform);

            _tilemap.gameObject.name = "PredictedTilemap";
            _tilemap.Load(TetrisInfo.Instance.TilesPosition()[_tilemap.gameObject.name], _bounds);

            Debug.Log
            (
                $"PiecePredictor started with size of {_size.ToString()} and bounds {_bounds.ToString()}"
            );
        }

        public void OnEnable()
        {
            EventsHub.OnPieceSpawn.AddSubscriber(Predict);
        }

        public void OnDisable()
        {
            EventsHub.OnPieceSpawn.RemoveSubscriber(Predict);
        }

        public void Predict(IPiece currentPiece)
        {
            Clear();
            Piece piece = Instantiate((Piece)TetrisInfo.Instance.PieceQueue().Peek(), gameObject.transform);
            piece.name = piece.ToString();
            _predictedPiece = piece;
            SetPiece();

            EventsHub.OnGridUpdated.Trigger(_tilemap, _bounds);
        }

        private void Clear()
        {
            _predictedPiece?.Destroy();
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