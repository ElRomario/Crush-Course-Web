using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrialSniffer : MonoBehaviour
{
    void Start()
    {
        foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (go.name.ToLower().Contains("trial"))
                Debug.LogWarning("GameObject name contains 'trial': " + go.name, go);

            var text = go.GetComponent<Text>();
            if (text && text.text.ToLower().Contains("trial"))
                Debug.LogWarning("Text contains 'trial': " + text.text, go);

            var tmp = go.GetComponent<TextMeshProUGUI>();
            if (tmp && tmp.text.ToLower().Contains("trial"))
                Debug.LogWarning("TMP text contains 'trial': " + tmp.text, go);
        }
    }
}
