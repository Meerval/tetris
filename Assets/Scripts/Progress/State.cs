using Event;
using Pieces;
using UnityEngine;

namespace Progress
{
    public class State : Progress<EState>
    {
        protected override void StartNewProgress()
        {
            CurrentValue = EState.WaitForActivePiece;
            Debug.Log($"[STATE UPDATE: {CurrentValue}] Game Started!");
        }

        protected override void SubscribeProgressAction()
        {
            EventHab.OnWaitForActivePiece.AddSubscriber(WaitForActivePiece);
            EventHab.OnSpawnPiece.AddSubscriber(PieceInProgress);
            EventHab.OnGameOver.AddSubscriber(SetOverGame);
        }

        protected override void UnsubscribeProgressAction()
        {
            EventHab.OnWaitForActivePiece.RemoveSubscriber(WaitForActivePiece);
            EventHab.OnSpawnPiece.RemoveSubscriber(PieceInProgress);
            EventHab.OnGameOver.RemoveSubscriber(SetOverGame);
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