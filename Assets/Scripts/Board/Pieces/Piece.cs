using System;
using Board.PiecePosition;
using Board.PiecePosition.RotationMath;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board.Pieces
{
    public abstract class Piece : MonoBehaviour, IPredictedPiece
    {
        [SerializeField] private TileBase tile;
        protected readonly RotationAngle RotationAngle = new();
        private string _id;

        private Vector2Int[] _shapeMapTmp;

        private readonly Vector2Int[,] _wallKicks =
        {
            { new(0, 0), new(-1, 0), new(-1, 1), new(0, -2), new(-1, -2) },
            { new(0, 0), new(1, 0), new(1, -1), new(0, 2), new(1, 2) },
            { new(0, 0), new(1, 0), new(1, -1), new(0, 2), new(1, 2) },
            { new(0, 0), new(-1, 0), new(-1, 1), new(0, -2), new(-1, -2) },
            { new(0, 0), new(1, 0), new(1, 1), new(0, -2), new(1, -2) },
            { new(0, 0), new(-1, 0), new(-1, -1), new(0, 2), new(-1, 2) },
            { new(0, 0), new(-1, 0), new(-1, -1), new(0, 2), new(-1, 2) },
            { new(0, 0), new(1, 0), new(1, 1), new(0, -2), new(1, -2) },
        };

        private void Awake()
        {
            _shapeMapTmp = (Vector2Int[])ShapeMap().Clone();
            _id = Guid.NewGuid().ToString();
        }

        public TileBase Tile()
        {
            return tile;
        }

        public bool HasNoWallKick(Func<Vector2Int, bool> checkFreePlace)
        {
            int wallKickIndex = RotationAngle.GetWallKickIdx();

            for (int i = 0; i < WallKicks().GetLength(1); i++)
            {
                Vector2Int translation = WallKicks()[wallKickIndex, i];

                if (!checkFreePlace.Invoke(translation))
                {
                    Debug.Log($"{this} has a wall kick on new position and can't be rotated");
                    return false;
                }
            }

            return true;
        }


        protected virtual Vector2Int[,] WallKicks()
        {
            return _wallKicks;
        }

        public void AddRotation(Direction direction)
        {
            RotationAngle.Add(direction);
            _shapeMapTmp = RotationCalculator().GetRotatedShapeMap();
        }

        public void SubRotation(Direction direction)
        {
            RotationAngle.Sub(direction);
            _shapeMapTmp = RotationCalculator().GetRotatedShapeMap();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual Vector2Int MonitorOffset()
        {
            return new Vector2Int(0, 0);
        }

        protected virtual IShapeMap RotationCalculator()
        {
            return new RotationJLSTZ(ShapeMap(), RotationAngle);
        }

        public virtual Vector2Int[] CurrentShapeMap()
        {
            return _shapeMapTmp;
        }

        public override string ToString()
        {
            string id = _id == null || _id.Length < 8 ? "" : $"#{_id[..8]}...";
            return $"{GetType().Name} {id}".Replace("Piece", "Piece ");
        }

        public abstract EPiece PieceType { get; }
        protected abstract Vector2Int[] ShapeMap();
    }
}