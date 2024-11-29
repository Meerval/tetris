using System;
using System.Collections.Generic;
using Settings;
using Systems.Storage;

namespace Templates.TypeSelectors
{
    public class DriveSelector : TypeSelector<IDrive, string>
    {
        protected override Dictionary<string, Type> TypesMap { get; } = new ()
        {
            { "Local", typeof(LocalDrive) }
        };
        
        protected override string Key { get; set; } = TetrisSettings.Drive;
    }
}