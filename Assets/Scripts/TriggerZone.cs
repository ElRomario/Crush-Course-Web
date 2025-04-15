using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject objectToActivate; // Ссылка на объект, который нужно активировать
    public Color gizmoColor = Color.green; // Цвет для отображения зоны

    private BoxCollider boxCollider;

    private void Awake()
    {
        // Получаем компонент BoxCollider и убеждаемся, что он является триггером
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что в триггер вошёл игрок
        {
            objectToActivate.SetActive(true); // Активируем объект
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что из триггера вышел игрок
        {
            Destroy(gameObject); // Уничтожаем текущий объект (триггер)
        }
    }

    private void OnDrawGizmos()
    {
        // Настраиваем цвет и рисуем рамку коллайдера
        Gizmos.color = gizmoColor;

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
    }
}
