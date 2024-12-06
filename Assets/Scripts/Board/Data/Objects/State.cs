using System;
using System.Collections.Generic;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;
using UnityEngine;

namespace Board.Data.Objects
{
    public class State : TetrisDataSingleton<EState, State>
    {
        protected override IStorable StorableTetrisData => new StateStorable();

        protected override void StartNewProgress()
        {
            CurrentValue = InitialData.State;
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
            CurrentValue = InitialData.State;
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

        private class StateStorable : IStorable
        {
            private readonly State _state = Instance;
            public string Id => "State";

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.State, _state.CurrentValue.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                string stateName = ObjectToString.TryParse(data.Data[Key.State]).OrElse(InitialData.State.ToString());
                _state.CurrentValue = Enum.TryParse(stateName, out EState state) ? state : InitialData.State;
            }

            public void LoadInitial()
            {
                _state.CurrentValue = InitialData.State;
            }

            private struct Key
            {
                public const string State = "State";
            }
        }

        private struct InitialData
        {
            public const EState State = EState.WaitForActivePiece;
        }
    }
}