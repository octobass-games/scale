using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueRenderer : MonoBehaviour
{
    private WaitForSecondsRealtime Timer;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject canvas;
    public bool IsWriting;

    private IEnumerator Coroutine;
    private string TextToWrite;
    private float WriteRate = 0.025f;
    private CharacterSwitcher CharacterSwitcher;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(WriteRate);
    }

    void Start()
    {
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();
    }

    public void ShowDialogue(string text, string speaker)
    {
        CharacterSwitcher?.SetEnableSwithing(false);
        Name.text = speaker;
        Coroutine = Write(text);
        StartCoroutine(Coroutine);
        Time.timeScale = 0;
        canvas.SetActive(true);
    }

    public void closeDialogue()
    {
        CharacterSwitcher?.SetEnableSwithing(true);
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

    public void WriteFull()
    {
        StopCoroutine(Coroutine);
        Text.maxVisibleCharacters = TextToWrite.Length;
        Text.text = TextToWrite;
        IsWriting = false;
    }
}
