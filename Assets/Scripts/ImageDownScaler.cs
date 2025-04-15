using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDownScaler : MonoBehaviour
{
    public float delay = 2f;      // Задержка перед началом уменьшения
    public float speed = 10f;     // Скорость уменьшения
    private bool hasStarted = false;
    public bool isAnimating = false;
    private float startTime;

    void Start()
    {
        startTime = Time.time + delay; // Фиксируем момент времени, когда должно начаться уменьшение
    }

    void Update()
    {
        // Ждем окончания задержки
        if (!hasStarted && Time.time >= startTime)
        {
            hasStarted = true;
        }

        // Если анимация запущена, уменьшаем размер
        if (hasStarted)
        {
            if (transform.localScale.x > 0.01f) // Оставляем небольшой запас, чтобы избежать проблем с плавающей точкой
            {
                isAnimating = true;
                float newScale = Mathf.Max(transform.localScale.x - (3f * Time.deltaTime * speed), 0f);
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
            else
            {
                transform.localScale = Vector3.zero; // Полностью скрываем объект
                isAnimating = false; // Завершаем анимацию
                hasStarted = false;  // Останавливаем дальнейшее уменьшение
            }
        }
    }
}
