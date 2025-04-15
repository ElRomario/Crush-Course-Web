using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI text; // —сылка на UI Text

    public float blinkInterval = 0.5f; // »нтервал мигани€

    void Start()
    {

        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            text.enabled = !text.enabled; // ¬ключаем/выключаем текст
            yield return new WaitForSeconds(blinkInterval); // ∆дЄм перед следующим изменением
        }
    }
}
