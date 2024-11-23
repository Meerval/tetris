using Board.Pieces;
using Board.Progress;

namespace Systems.Events
{
    public static class EventsHub
    {
        public static readonly IEvent OnGameStart = new TetrisEvent();
        public static readonly IEvent OnWaitCoroutineStart = new TetrisEvent();
        public static readonly IEvent OnWaitCoroutineEnd = new TetrisEvent();
        public static readonly IEvent OnWaitForPiece = new TetrisEvent();
        public static readonly IEvent<IPiece> OnSpawnPiece = new TetrisEvent<IPiece>();
        public static readonly IEvent OnLevelUp = new TetrisEvent();
        public static readonly IEvent<int> OnScoreUp = new TetrisEvent<int>();
        public static readonly IEvent<EGameOverReason> OnGameOver = new TetrisEvent<EGameOverReason>();
    }
}