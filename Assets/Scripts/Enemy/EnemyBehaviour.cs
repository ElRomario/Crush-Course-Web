using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Vector3 zoneCenter; // Центр зоны
    private Vector3 zoneSize; // Размер зоны
    private float escapeSpeed;
    private Transform player;// Скорость ухода

    private bool isEscaping = false;
    private Vector3 escapeDirection;// Флаг ухода

    public void Initialize(Vector3 center, Vector3 size, float speed, Transform playerTransform)
    {
        zoneCenter = center;
        zoneSize = size;
        escapeSpeed = speed;
        player = playerTransform;
    }

    void Update()
    {
        if (!isEscaping)
        {
            CheckZone();
        }
        else
        {
            Escape();
        }
    }

    void CheckZone()
    {
        // Проверяем, находится ли враг в пределах зоны
        Vector3 halfSize = zoneSize / 2;
        if (transform.position.x < zoneCenter.x - halfSize.x || transform.position.x > zoneCenter.x + halfSize.x ||
            transform.position.y < zoneCenter.y - halfSize.y || transform.position.y > zoneCenter.y + halfSize.y ||
            transform.position.z < zoneCenter.z - halfSize.z || transform.position.z > zoneCenter.z + halfSize.z)
        {
            StartEscape();
        }
    }

    void StartEscape()
    {
        isEscaping = true;

        // Отключаем взаимодействие
       

        // Вычисляем направление ухода к игроку по оси Z
        escapeDirection = (player.position - transform.position).normalized;
        escapeDirection.x = 0; // Убираем смещение по X
        escapeDirection.y = 0; // Убираем смещение по Y
    }


    void Escape()
    {
        // Двигаем врага в сторону игрока по оси Z
        transform.position += escapeDirection * escapeSpeed * Time.deltaTime;

        // Уничтожаем врага, если он достигает игрока
        float distanceToPlayer = Mathf.Abs(transform.position.z - player.position.z);
        if (distanceToPlayer < 1f) // Условие для исчезновения
        {
            Destroy(gameObject); // Уничтожаем объект (или возвращаем в пул объектов)
        }
    }
}
