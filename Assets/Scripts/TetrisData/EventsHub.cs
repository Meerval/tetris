using Board;
using Board.Pieces;
using Systems.Events;
using UnityEngine;

namespace TetrisData
{
    public static class EventsHub
    {
        public static readonly IEvent<ETetrisScene> OnStageChanged = new TetrisEvent<ETetrisScene>();
        public static readonly IEvent OnNewGameStart = new TetrisEvent();
        public static readonly IEvent OnLockBoard = new TetrisEvent();
        public static readonly IEvent OnUnlockBoard = new TetrisEvent();

        public static readonly IEvent<TilemapController, RectInt> OnGridUpdated
            = new TetrisEvent<TilemapController, RectInt>();

        public static readonly IEvent OnWaitForPiece = new TetrisEvent();
        public static readonly IEvent<IPiece> OnPieceSpawn = new TetrisEvent<IPiece>();
        public static readonly IEvent<Vector2Int> OnPieceShift = new TetrisEvent<Vector2Int>();
        public static readonly IEvent<Vector2Int[]> OnPieceRotate = new TetrisEvent<Vector2Int[]>();
        public static readonly IEvent OnLevelUp = new TetrisEvent();
        public static readonly IEvent<int> OnScoreUp = new TetrisEvent<int>();
        public static readonly IEvent<EGameOverReason> OnGameOver = new TetrisEvent<EGameOverReason>();
    }
}