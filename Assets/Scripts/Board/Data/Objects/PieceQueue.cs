﻿using System.Collections.Generic;
using System.Linq;
using Board.Pieces;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;

namespace Board.Data.Objects
{
    public class PieceQueue : TetrisDataSingleton<Queue<IPiece>, PieceQueue>
    {
        protected override IStorable StorableTetrisData => new PieceQueueStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void InitForNewGame()
        {
            CurrentValue = InitialData.RandomQueue;
        }

        protected override void SubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.AddSubscriber(UpdateQueue);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnPieceSpawn.RemoveSubscriber(UpdateQueue);
        }

        private void UpdateQueue(IPiece _)
        {
            CurrentValue ??= InitialData.RandomQueue;
            while (CurrentValue.Count < 2)
            {
                CurrentValue.Enqueue(PieceProvider.Instance.GetRandom());
            }
            
            Storage.Instance.SaveGame();
        }

        private class PieceQueueStorable : IStorable
        {
            private readonly PieceQueue _queue = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    {
                        Key.Pieces,
                        string.Concat(_queue.CurrentValue.ToList().Select(piece => piece.PieceType.ToString()))
                    }
                }
            );

            public void Load(StorableData data)
            {
                string charList = ObjectToString.TryParse(data.Data[Key.Pieces]).OrElse(InitialData.EmptyQueueStr);

                IPiece CreateIPiece(char c)
                {
                    IPiece piece = PieceProvider.Instance.Get(EPieceParser.Parse(c));
                    return piece;
                }

                _queue.CurrentValue = charList.Equals(InitialData.EmptyQueueStr)
                    ? InitialData.RandomQueue
                    : new Queue<IPiece>(charList.Select(CreateIPiece).ToList());
            }

            public void LoadInitial()
            {
                _queue.CurrentValue = InitialData.RandomQueue;
            }

            private struct Key
            {
                public const string Id = "PieceQueue";
                public const string Pieces = "Pieces";
            }
        }

        private struct InitialData
        {
            public const string EmptyQueueStr = "";
            public static readonly Queue<IPiece> RandomQueue = CreateQueue(2);

            private static Queue<IPiece> CreateQueue(int count)
            {
                Queue<IPiece> queue = new();
                while (queue.Count < count)
                {
                    queue.Enqueue(PieceProvider.Instance.GetRandom());
                }

                return queue;
            }
        }
    }
}