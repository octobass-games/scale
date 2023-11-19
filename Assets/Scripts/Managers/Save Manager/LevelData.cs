using System;

[Serializable]
public class LevelData
{
    public string Name;
    public bool IsComplete;
    public string Collectable;
    public bool CollectableFound;

    public LevelData(string name, bool isComplete, string collectable, bool collectableFound)
    {
        Name = name;
        IsComplete = isComplete;
        Collectable = collectable;
        CollectableFound = collectableFound;
    }
}
