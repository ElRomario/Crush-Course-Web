using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform[] waypoints; // Точки пути для движения
    private GameObject player; // Ссылка на игрока
    public float speed = 5f; // Базовая скорость движения врага
    public float maxDistanceZFromPlayer = 10f; // Максимальное расстояние по оси Z от игрока

    private int currentWaypointIndex = 0; // Текущая точка пути
    private Vector3 currentSpeed; // Текущая скорость движения
    private Vector3 lastPlayerPosition; // Последняя позиция игрока
    private float playerSpeed; // Скорость игрока

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSpeed = Vector3.zero;
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        if (waypoints.Length > 0 && player != null)
        {
            CalculatePlayerSpeed();
            MoveEnemy();
        }
    }

    // Вычисление скорости игрока по разнице в позициях
    void CalculatePlayerSpeed()
    {
        // Разница в позиции игрока между кадрами
        Vector3 playerVelocity = (player.transform.position - lastPlayerPosition) / Time.deltaTime;
        playerSpeed = playerVelocity.magnitude; // Получаем величину скорости игрока

        // Обновляем последнюю позицию игрока для следующего кадра
        lastPlayerPosition = player.transform.position;
    }

    void MoveEnemy()
    {
        // Получаем текущую и целевую позиции
        Vector3 currentPosition = transform.position;
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Рассчитываем направление движения к текущей точке пути
        Vector3 directionToWaypoint = (targetWaypoint.position - currentPosition).normalized;

        // Проверяем расстояние по оси Z до игрока
        float distanceZToPlayer = Mathf.Abs(currentPosition.z - player.transform.position.z);

        if (distanceZToPlayer > maxDistanceZFromPlayer)
        {
            // Если враг выходит за пределы, изменяем направление движения
            float clampedZ = Mathf.Clamp(
                currentPosition.z,
                player.transform.position.z - maxDistanceZFromPlayer,
                player.transform.position.z + maxDistanceZFromPlayer
            );

            // Новая скорость направляет врага обратно в допустимую область
            Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, clampedZ);
            Vector3 correctionDirection = (targetPosition - currentPosition).normalized;

            currentSpeed = correctionDirection * playerSpeed; // Скорость врага = скорость игрока
        }
        else
        {
            // Если враг в пределах допустимой дистанции, направляем его к точке пути
            currentSpeed = directionToWaypoint * speed;
        }

        // Обновляем позицию врага
        transform.position += currentSpeed * Time.deltaTime;

        // Если достигли текущей точки пути, переходим к следующей
        if (Vector3.Distance(currentPosition, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Зацикливаем путь
        }
    }
}

