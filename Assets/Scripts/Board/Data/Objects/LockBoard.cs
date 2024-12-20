using System.Collections.Generic;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;

namespace Board.Data.Objects
{
    public class LockBoard : TetrisDataSingleton<bool, LockBoard>
    {
        protected override IStorable StorableTetrisData => new UpdateLockStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnLockBoard.AddSubscriber(Lock);
            EventsHub.OnUnlockBoard.AddSubscriber(Unlock);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnLockBoard.RemoveSubscriber(Lock);
            EventsHub.OnUnlockBoard.RemoveSubscriber(Unlock);
        }

        private void Lock()
        {
            CurrentValue = true;
            Storage.Instance.SaveGame();
        }

        private void Unlock()
        {
            CurrentValue = false;
            Storage.Instance.SaveGame();
        }

        private class UpdateLockStorable : IStorable
        {
            private readonly LockBoard _lockBoard = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.IsLocked, "False" }
                }
            );

            public void Load(StorableData data)
            {
                _lockBoard.CurrentValue = ObjectToBool.TryParse(data.Data[Key.IsLocked]).OrElse(InitialData.IsLocked);
            }

            public void LoadInitial()
            {
                _lockBoard.CurrentValue = InitialData.IsLocked;
            }

            private struct Key
            {
                public const string Id = "LockBoard";
                public const string IsLocked = "IsLocked";
            }
        }

        private struct InitialData
        {
            public const bool IsLocked = false;
        }
    }
}