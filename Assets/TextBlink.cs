using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private Text blinkingText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            blinkingText.text = "- Press any button to start -";
            yield return new WaitForSeconds(0.5f);
            blinkingText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
