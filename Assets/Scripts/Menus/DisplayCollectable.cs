using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCollectable : MonoBehaviour
{
    public CollectableScriptable CollectableScriptable;
    public SpriteRenderer ObjectInGameNonAnimated;
    public SpriteRenderer ObjectInGame;
    public SpriteRenderer OutlineInGame;
    public SpriteRenderer Enlarged;

    public void InitCollectable(CollectableScriptable s)
    {
        CollectableScriptable= s;
        ObjectInGame.sprite = s.ItemInGame;
        ObjectInGameNonAnimated.sprite = s.ItemInGame;
        Enlarged.sprite = s.ItemBig;
        OutlineInGame.sprite = s.ItemInGameWithOutline;
    }
}
