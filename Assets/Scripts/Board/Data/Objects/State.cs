using Board.Pieces;
using Systems.Storage;
using UnityEngine;

namespace Board.Data.Objects
{
    public class State : TetrisDataSingleton<EState, State>
    {
        protected override IStorable StorableTetrisData => null;
        
        protected override void StartNewProgress()
        {
            CurrentValue = EState.WaitForActivePiece;
            Debug.Log($"[STATE UPDATE: {CurrentValue}] Game Started!");
        }

        protected override void SubscribeProgressAction()
        {
            EventsHub.OnWaitForPiece.AddSubscriber(WaitForActivePiece);
            EventsHub.OnSpawnPiece.AddSubscriber(PieceInProgress);
            EventsHub.OnGameOver.AddSubscriber(SetOverGame);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventsHub.OnWaitForPiece.RemoveSubscriber(WaitForActivePiece);
            EventsHub.OnSpawnPiece.RemoveSubscriber(PieceInProgress);
            EventsHub.OnGameOver.RemoveSubscriber(SetOverGame);
        }

        private void WaitForActivePiece()
        {
            CurrentValue = EState.WaitForActivePiece;
            Debug.Log($"[STATE UPDATE: {CurrentValue}] Wait for active piece");
        }

        private void PieceInProgress(IPiece activePiece)
        {
            CurrentValue = EState.PieceInProgress;
            Debug.Log($"[STATE UPDATE: {CurrentValue}] {activePiece} in progress");
        }

        private void SetOverGame(EGameOverReason _)
        {
            CurrentValue = EState.GameOver;
            Debug.Log($"[STATE UPDATE: {CurrentValue}] Game Over!");
        }
    }
}