using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ImageScaler : MonoBehaviour
{

    public float delay = 2f;      // Задержка перед началом анимации
    public float speed = 10f;     // Скорость увеличения
    private bool hasStarted = false;
    public bool isAnimating = false;

    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(WaitAndStartScaling()); // Запуск корутины задержки
    }

    void Update()
    {
        if (hasStarted)
        {
            if (transform.localScale.x < 30)
            {
                isAnimating = true;

                // Увеличиваем размер, но не превышаем 30
                float newScale = Mathf.Min(transform.localScale.x + (3f * Time.deltaTime * speed), 30f);
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
            else
            {
                isAnimating = false; // Анимация завершена
                hasStarted = false;  // Останавливаем дальнейшее увеличение
            }
        }
    }

    IEnumerator WaitAndStartScaling()
    {
        yield return new WaitForSeconds(delay); // Ждём перед началом анимации
        hasStarted = true;  // Запускаем анимацию
    }
}
