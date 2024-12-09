﻿using Board.Pieces;
using Systems.Events;

namespace Board.Data
{
    public static class EventsHub
    {
        public static readonly IEvent OnNewGameStart = new TetrisEvent();
        public static readonly IEvent OnWaitCoroutineStart = new TetrisEvent();
        public static readonly IEvent OnWaitCoroutineEnd = new TetrisEvent();
        public static readonly IEvent OnWaitForPiece = new TetrisEvent();
        public static readonly IEvent<IPiece> OnSpawnPiece = new TetrisEvent<IPiece>();
        public static readonly IEvent OnLevelUp = new TetrisEvent();
        public static readonly IEvent<int> OnScoreUp = new TetrisEvent<int>();
        public static readonly IEvent<EGameOverReason> OnGameOver = new TetrisEvent<EGameOverReason>();
    }
}