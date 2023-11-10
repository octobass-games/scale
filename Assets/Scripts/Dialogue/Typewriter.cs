using System.Collections;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public float WriteRate;

    private WaitForSecondsRealtime Timer;

    void Awake()
    {
        Timer = new WaitForSecondsRealtime(WriteRate);
    }

    public IEnumerator Write(TextMeshProUGUI writeSurface, string text) {
        writeSurface.maxVisibleCharacters = 0;
        writeSurface.text = text;

        for (int i = 0; i < text.Length; i++) {
            writeSurface.maxVisibleCharacters = i + 1;
            yield return Timer;
        }
    }
}
