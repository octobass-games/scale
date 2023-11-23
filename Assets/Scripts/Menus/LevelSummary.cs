using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSummary : MonoBehaviour
{
    public Level level;
    private DialogueController DialogueController;
    private DialogueRenderer DialogueRenderer;
    private bool DialogueDone = false;
    public UnityEvent OnEndOfDialogue;
    private LevelBoxes breakdown;
    public GameObject boxesParent;

    // Start is called before the first frame update
    void Start()
    {
        DialogueRenderer = FindObjectOfType<DialogueRenderer>();
        DialogueController = new DialogueController(Parse(level.EndingDialogue), OnEndOfDialogue, DialogueRenderer);
        breakdown = LoadLevelBreakdown();
        OnEndOfDialogue.AddListener(OnDialogueDone);
        DialogueController.HandleProgressDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && !DialogueDone)
        {
            DialogueController.HandleProgressDialogue();
        }
    }

    private List<DialogueItem> Parse(string text)
    {
        var items = new List<DialogueItem>();
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            if (line.Trim() != "")
            {
                var split = line.Split(":");
                items.Add(new DialogueItem(split[0], split[1]));
            }
        }
        return items;
    }


    private LevelBoxes LoadLevelBreakdown()
    {
        var item = Instantiate(level.LevelSelectBreakdown);

        item.transform.SetParent(boxesParent.transform);
        item.transform.localPosition = new Vector2(0, 0);
        return item.GetComponent<LevelBoxes>();

    }

    private void OnDialogueDone()
    {
        Debug.Log("Dialogue Donn!");
        DialogueDone = true;
        breakdown.gameObject.SetActive(true);
        breakdown.LoadBoxes();
    }
}
