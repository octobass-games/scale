using System;

[Serializable]
public class LevelData
{
    public string Name;
    public bool IsComplete;

    public LevelData(string name, bool isComplete)
    {
        Name = name;
        IsComplete = isComplete;
    }
}
