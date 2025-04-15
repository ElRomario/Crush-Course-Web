using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI text; // ������ �� UI Text

    public float blinkInterval = 0.5f; // �������� �������

    void Start()
    {

        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            text.enabled = !text.enabled; // ��������/��������� �����
            yield return new WaitForSeconds(blinkInterval); // ��� ����� ��������� ����������
        }
    }
}
