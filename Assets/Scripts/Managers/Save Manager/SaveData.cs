using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<LevelData> LevelData;

    public SaveData(List<LevelData> Levels)
    {
        LevelData = Levels;
    }
}
