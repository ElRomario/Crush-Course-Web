using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{


    public ImageScaler imageScaler; // Ссылка на ImageScaler
    public Image image;
    public string sceneToLoad;// UI-изображение, которое анимируется

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) // Проверяем нажатие клавиши Enter
        {
            // Включаем объект с изображением
            image.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.roll);
            // Убеждаемся, что imageScaler включен и сбрасываем параметры
            imageScaler.enabled = true;
            imageScaler.isAnimating = true;

            // Запускаем корутину для ожидания завершения анимации
            StartCoroutine(WaitForImageScalerAndLoadScene());
        }
    }

    IEnumerator WaitForImageScalerAndLoadScene()
    {
        // Ждем, пока imageScaler анимирует изображение
        while (imageScaler.isAnimating)
        {
            yield return null; // Ожидание на каждый кадр
        }

        // Загружаем сцену после завершения анимации
        SceneManager.LoadScene(sceneToLoad);
    }
}
