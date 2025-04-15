using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // Центр вращения
    [SerializeField] private float radius = 5f;     // Радиус вращения
    [SerializeField] private float rotationSpeed = 30f; // Скорость вращения (градусы в секунду)

    private float currentAngle = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        if (centerPoint == null)
        {
            Debug.LogWarning("Center Point не назначен!");
            return;
        }

        // Инициализируем позицию объекта на окружности с заданным радиусом
        UpdatePosition();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (centerPoint == null) return;

        // Обновляем угол в зависимости от времени и скорости вращения
        currentAngle += rotationSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        // Обновляем позицию объекта
        UpdatePosition();

        // Поворачиваем объект в сторону движения
        Vector3 direction = (transform.position - lastPosition).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Запоминаем последнюю позицию
        lastPosition = transform.position;
    }

    private void UpdatePosition()
    {
        // Рассчитываем новую позицию на окружности
        float x = centerPoint.position.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float z = centerPoint.position.z + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
