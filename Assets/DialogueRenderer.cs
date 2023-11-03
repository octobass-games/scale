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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogue(List<string> text, string speaker)
    {
        Name.text = speaker;
        Text.text = text[0];
        canvas.SetActive(true);

    }
}
