using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup fadeCanvas;  // UI-������ � CanvasGroup
    public float fadeDuration = 1.5f;  // ������������ ����������
    public string nextScene = "Win";  // �������� ��������� �����
    public SceneLoaderLeg sceneLoaderLeg;

    void Start()
    {
        sceneLoaderLeg = FindObjectOfType<SceneLoaderLeg>();
        fadeCanvas.alpha = 0;  // ��������, ��� ������ �������� � ������
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            yield return null;
        }

        sceneLoaderLeg.LoadScene(nextScene); // ������� �� ����� ������
    }
}
