using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public DisplayCollectable CollectableItemPrefab;
    public List<CollectableScriptable> CollectableList;
    public List<CollectableBackgroundItem> CollectableBackgroundItems;
    public SaveManager SaveManager;

    private SaveData SaveData;

    private int index = 0;

    void Start()
    {
        SaveData = SaveManager.Load();

        for (int i = 0; i < CollectableList.Count; i++)
        {
            var collectable = CollectableList[i];

            if (SaveData != null)
            {
                var level = SaveData.LevelData.Find(levelData => levelData.Collectable == collectable.Name);

                if (level != null && !level.CollectableFound)
                {
                    var backgroundItem = CollectableBackgroundItems.Find(backgroundItem => backgroundItem.collectable == collectable);

                    if (backgroundItem != null)
                    {
                        backgroundItem.EmptyMain();
                    }
                }
            }
            else
            {
                var backgroundItem = CollectableBackgroundItems.Find(backgroundItem => backgroundItem.collectable == collectable);

                if (backgroundItem != null)
                {
                    backgroundItem.EmptyMain();
                }
            }
        }

        StartCoroutine(LoadCollectable());
    }


    private IEnumerator LoadCollectable()
    {
        if (SaveData != null)
        {
            var collectable = CollectableList[index];

            var level = SaveData.LevelData.Find(level => level.Collectable == collectable.Name);

            if (level != null && level.CollectableFound)
            {
                var item = Instantiate(CollectableItemPrefab);

                item.InitCollectable(collectable);
                item.transform.SetParent(this.transform);
                item.transform.localPosition = new Vector2(0, 0);
                item.gameObject.AddComponent<BoxCollider2D>();
        
                yield return new WaitForSeconds(0.5f);
            }

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
}
