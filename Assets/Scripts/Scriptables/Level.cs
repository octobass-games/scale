using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public string LevelName;
    public List<SubLevel> SubLevels;

    [TextArea]
    public string EndingDialogue;

    public GameObject LevelSelectBreakdown;
}