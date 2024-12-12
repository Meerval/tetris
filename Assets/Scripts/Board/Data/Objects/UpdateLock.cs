using System.Collections.Generic;
using Systems.Parsers.Object;
using Systems.Storage;
using Systems.Storage.POCO;

namespace Board.Data.Objects
{
    public class UpdateLock : TetrisDataSingleton<bool, UpdateLock>
    {
        protected override IStorable StorableTetrisData => new UpdateLockStorable();
        protected override bool IsResettableByNewGame => true;

        protected override void SubscribeDataAction()
        {
            EventsHub.OnWaitCoroutineStart.AddSubscriber(Lock);
            EventsHub.OnWaitCoroutineEnd.AddSubscriber(Unlock);
        }

        protected override void UnsubscribeDataAction()
        {
            EventsHub.OnWaitCoroutineStart.RemoveSubscriber(Lock);
            EventsHub.OnWaitCoroutineEnd.RemoveSubscriber(Unlock);
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
            private readonly UpdateLock _updateLock = Instance;
            public string Id => Key.Id;

            public StorableData StorableData => new(Id, new Dictionary<string, object>
                {
                    { Key.IsLocked, _updateLock.CurrentValue.ToString() }
                }
            );

            public void Load(StorableData data)
            {
                _updateLock.CurrentValue = ObjectToBool.TryParse(data.Data[Key.IsLocked]).OrElse(InitialData.IsLocked);
            }

            public void LoadInitial()
            {
                _updateLock.CurrentValue = InitialData.IsLocked;
            }

            private struct Key
            {
                public const string Id = "UpdateLock";
                public const string IsLocked = "IsLocked";
            }
        }

        private struct InitialData
        {
            public const bool IsLocked = false;
        }
    }
}