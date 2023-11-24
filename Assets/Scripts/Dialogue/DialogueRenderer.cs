using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Button SkipButton;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(WriteRate);
    }

    void Start()
    {
        CharacterSwitcher = FindObjectOfType<CharacterSwitcher>();
    }

    public void ShowDialogue(string text, string speaker, Action skip)
    {
        CharacterSwitcher?.SetEnableSwithing(false);
        SetAnimation(true);
        Name.text = speaker;
        Coroutine = Write(text);
        StartCoroutine(Coroutine);
        Time.timeScale = 0;
        canvas.SetActive(true);

        SkipButton.onClick.RemoveAllListeners();
        SkipButton.onClick.AddListener(() =>skip.Invoke());
    }

    public void closeDialogue()
    {
        CharacterSwitcher?.SetEnableSwithing(true);
        SetAnimation(false);
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public bool IsOpen()
    {
        return canvas.activeSelf;
    }

    private void SetAnimation(bool inDialogue)
    {
        GameObject obj = CharacterSwitcher.GetActiveCharacter();
        if (obj.activeSelf) {
            obj.GetComponentInChildren<Animator>().SetBool("InDialogue", inDialogue);
        }
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
