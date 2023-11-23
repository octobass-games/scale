using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectable")]
public class CollectableScriptable : ScriptableObject
{
    public Sprite ItemInGame;
    public Sprite ItemInGameWithOutline;
    public Sprite ItemBig;
}