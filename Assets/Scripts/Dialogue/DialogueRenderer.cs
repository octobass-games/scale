using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueRenderer : MonoBehaviour
{
    public float WriteRate;
    private WaitForSecondsRealtime Timer;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject canvas;

    private IEnumerator Coroutine;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(WriteRate);
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    public void ShowDialogue(string text, string speaker)
    {
        Name.text = speaker;
        Coroutine = Write(text);
        StartCoroutine(Coroutine);
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

    private IEnumerator Write(string text)
    {
        Text.maxVisibleCharacters = 0;
        Text.text = text;

        for (int i = 0; i < text.Length; i++)
        {
            Text.maxVisibleCharacters = i + 1;
            yield return Timer;
        }
    }
}
