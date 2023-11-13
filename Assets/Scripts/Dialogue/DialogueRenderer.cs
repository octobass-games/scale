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
    private string TextToWrite;
    private bool IsWriting;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(WriteRate);
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    public bool ShowDialogue(string text, string speaker)
    {
        if (IsWriting)
        {
            StopCoroutine(Coroutine);
            WriteFull();
            return false;
        }
        else
        {
            Name.text = speaker;
            Coroutine = Write(text);
            StartCoroutine(Coroutine);
            Time.timeScale = 0;
            canvas.SetActive(true);
            return true;
        }
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
        IsWriting = true;

        TextToWrite = text;
        Text.maxVisibleCharacters = 0;
        Text.text = text;

        for (int i = 0; i < TextToWrite.Length; i++)
        {
            Text.maxVisibleCharacters = i + 1;
            yield return Timer;
        }

        IsWriting = false;
    }

    private void WriteFull()
    {
        Text.maxVisibleCharacters = TextToWrite.Length;
        Text.text = TextToWrite;
        IsWriting = false;
    }
}
