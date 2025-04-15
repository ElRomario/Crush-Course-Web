using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup fadeCanvas;  // UI-панель с CanvasGroup
    public float fadeDuration = 1.5f;  // Длительность затемнения
    public string nextScene = "Win";  // Название следующей сцены
    public SceneLoaderLeg sceneLoaderLeg;

    void Start()
    {
        sceneLoaderLeg = FindObjectOfType<SceneLoaderLeg>();
        fadeCanvas.alpha = 0;  // Убедимся, что панель невидима в начале
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

        sceneLoaderLeg.LoadScene(nextScene); // Переход на сцену победы
    }
}
