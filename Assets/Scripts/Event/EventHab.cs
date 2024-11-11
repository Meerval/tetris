using Pieces;
using Progress;

namespace Event
{
    public static class EventHab
    {
        public static readonly IEvent OnGameStart = new TetrisEvent();
        public static readonly IEvent<IPiece> OnSpawnPiece = new TetrisEvent<IPiece>();
        public static readonly IEvent OnLevelUp = new TetrisEvent();
        public static readonly IEvent<int> OnScoreUp = new TetrisEvent<int>();
        public static readonly IEvent<EGameOverReason> OnGameOver = new TetrisEvent<EGameOverReason>();
    }
}