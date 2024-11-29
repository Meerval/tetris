using System;
using System.Collections.Generic;
using Settings;
using Templates.TypeSelectors;

namespace Systems.Storage
{
    public class DriveSelector : SettingsDependantSelector<IDrive, string>
    {
        protected override Dictionary<string, Type> TypesMap { get; } = new ()
        {
            { "Local", typeof(LocalDrive) }
        };
        
        protected override string Key { get; set; } = TetrisSettings.Drive;
    }
}