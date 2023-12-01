using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectable")]
public class CollectableScriptable : ScriptableObject
{
    public string Name;
    public Sprite ItemInGame;
    public Sprite ItemInGameWithOutline;
    public Sprite ItemBig;
    public Sprite ItemBigOutline;
}