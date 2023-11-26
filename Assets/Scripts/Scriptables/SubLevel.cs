using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SubLevel", menuName = "SubLevel")]
public class SubLevel : ScriptableObject
{
    public string Scene;
    public string Name;

    public Vector2 GnomePosition;
    public Vector2 GiantPosition;
    public bool PositionGnome;
    public bool PositionGiant;

    public SubLevel NextLevel;
}