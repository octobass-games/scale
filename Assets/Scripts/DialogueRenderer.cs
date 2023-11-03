using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueRenderer : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    public void ShowDialogue(string text, string speaker)
    {
        Name.text = speaker;
        Text.text = text;
        Time.timeScale = 0;
    
        canvas.SetActive(true);
    }

    public void closeDialogue()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public bool IsOpen()
    {
        return canvas.activeSelf;
    }
}
