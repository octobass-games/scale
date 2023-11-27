using System;

[Serializable]
public class LevelData
{
    public string Name;
    public bool IsComplete;
    public string Collectable;
    public bool CollectableFound;
    public string Clue;
    public bool ClueFound;

    public LevelData(string name, bool isComplete, string collectable, bool collectableFound, string clue, bool clueFound)
    {
        Name = name;
        IsComplete = isComplete;
        Collectable = collectable;
        CollectableFound = collectableFound;
        Clue = clue;
        ClueFound = clueFound;
    }
}
