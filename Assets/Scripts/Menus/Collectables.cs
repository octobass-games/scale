using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public DisplayCollectable CollectableItemPrefab;
    public List<CollectableScriptable> CollectableList;

    private int index = 0;

    void Start()
    {
        StartCoroutine(LoadCollectable());
    }


    private IEnumerator LoadCollectable()
    {
        var collectable = CollectableList[index];
        var item = Instantiate(CollectableItemPrefab);

        item.InitCollectable(collectable);
        item.transform.SetParent(this.transform);
        item.transform.localPosition = new Vector2(0, 0);
        item.gameObject.AddComponent<BoxCollider2D>();

        yield return new WaitForSeconds(0.5f);

        index += 1;
        if (index < CollectableList.Count)
        {
            StartCoroutine(LoadCollectable());
        }
        else
        {
            index = 0;
        }
    }
}
