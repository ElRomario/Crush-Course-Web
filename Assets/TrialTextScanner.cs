using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrialTextScanner : MonoBehaviour
{
    void Start()
    {
        var texts = FindObjectsOfType<Text>(true);
        foreach (var t in texts)
        {
            if (t.text.ToLower().Contains("trial"))
            {
                Debug.LogWarning("Trial text found: " + t.text, t.gameObject);
            }
        }

        var tmpTexts = FindObjectsOfType<TextMeshProUGUI>(true);
        foreach (var t in tmpTexts)
        {
            if (t.text.ToLower().Contains("trial"))
            {
                Debug.LogWarning("TMP Trial text found: " + t.text, t.gameObject);
            }
        }
    }
}