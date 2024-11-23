using Board.Meta;
using Board.Pieces;
using Systems.Events;
using UnityEngine;

namespace Board.Meta
{
    public class State : Progress<EState, State>
    {
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