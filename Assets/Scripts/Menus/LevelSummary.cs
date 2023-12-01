using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSummary : MonoBehaviour
{
    public Animator Animator;
    public bool Friends;
    public bool Everyone;
    public Level level;
    private DialogueController DialogueController;
    private DialogueRenderer DialogueRenderer;
    private bool DialogueDone = false;
    public UnityEvent OnEndOfDialogue;
    private LevelBoxes breakdown;
    public GameObject boxesParent;
    public SceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        DialogueRenderer = FindObjectOfType<DialogueRenderer>();
        DialogueController = new DialogueController(level.EndingDialogue.ParseToDialogue(), OnEndOfDialogue, DialogueRenderer);
        breakdown = LoadLevelBreakdown();
        OnEndOfDialogue.AddListener(OnDialogueDone);
        DialogueController.HandleProgressDialogue();

        if (Animator != null)
        {
            Animator.SetBool("friends", Friends);
            Animator.SetBool("everyone", Everyone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && !DialogueDone)
        {
            DialogueController.HandleProgressDialogue();
        }
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
        DialogueDone = true;
        breakdown.gameObject.SetActive(true);

        var lastSubLevel = level.SubLevels[level.SubLevels.Count - 1];  
        breakdown.LoadBoxes(true, () => sceneManager.ChangeLevelScene(lastSubLevel.NextLevel));
    }
}
