using System;
using System.Collections.Generic;
using Settings;
using Systems.Storage;

namespace Templates.Selectors
{
    public class DriveSelector : Selector<IDrive, string>
    {
        protected override Dictionary<string, Type> TypesMap { get; } = new ()
        {
            { "Local", typeof(LocalDrive) }
        };
        
        protected override string Key { get; set; } = TetrisSettings.Drive;
    }
}