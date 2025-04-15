using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Префаб врага
    public RailPlaneController planeController;
    public Transform player;// Ссылка на скрипт управления самолётом

    public float zoneMinZ = -10f;
    public float zoneMaxZ = 30f;
    public float zoneX = 20f;
    public float zoneY = 10f;
    public float escapeSpeed = 20f;

    public GameObject enemyPrefab;   // Префаб врага
    public GameObject bulletPrefab; // Префаб пули
    public Transform[] spawnPoints; // Точки спавна врагов
    public Transform[] shootPoints; // Точки стрельбы

    [Header("Параметры стрельбы врагов")]
    public float fireRate = 2f;      // Скорость стрельбы
    public float bulletSpeed = 15f; // Скорость пуль
    public float spread = 5f;       // Разброс пуль

    public Vector3 zoneCenter = new Vector3(0, 0, 20f); // Центр зоны
    public Vector3 zoneSize = new Vector3(40f, 20f, 40f); // Размер зоны (ширина, высота, глубина)
    // Цвет зоны для визуализации
    public Color zoneColor = new Color(0f, 1f, 0f, 0.2f);

    public Transform[] waypoints; // Массив точек пути
    public float moveToPathSpeed = 5f; // Скорость движения к ближайшей точке пути
    public float pathSpeed = 3f; // Скорость движения по пути


    [Header("Spawn Settings")]
    public float zOffset = 5f; // Смещение по оси Z для спавна врагов
    public float maxRandomOffset = 2f; // Максимальное случайное смещение относительно пути
    
    Quaternion spawnRotation = Quaternion.Euler(180,0, 0);
    private bool hasTriggered = false; // Флаг для однократного срабатывания триггера

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что триггер пересёк игрок
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemySpawn);
            // Рассчитываем точку спавна и движение врага
            Vector3 spawnPosition = CalculateRandomSpawnPosition();

            GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Создаем врага
            GameObject enemy = Instantiate(selectedPrefab, spawnPosition, spawnRotation);

            // Настроим врага для движения по пути
            EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
            if (enemyMover != null)
            {
                // Инициализируем врага, чтобы он начал двигаться по пути с учётом смещения
                enemyMover.Initialize(waypoints, moveToPathSpeed, pathSpeed);
            }
            else
            {
                Debug.Log("Enemy Mover is Null!");
            }

            EnemyShooting enemyShooting = enemy.GetComponentInChildren<EnemyShooting>();
            if (enemyShooting != null)
            {
                enemyShooting.SetupShooting(
                    enemyShooting.shootPoint, // shootPoint уже задан в префабе
                    bulletPrefab,
                    fireRate,
                    bulletSpeed,
                    spread
                 
                );
                // Передаем параметры врагу
                EnemyBehaviour enemyBehavior = enemy.GetComponent<EnemyBehaviour>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.Initialize(zoneCenter, zoneSize, escapeSpeed, player);
                }

            }
        }
    }


        // Рассчитываем точку спавна с учётом смещения
        Vector3 CalculateRandomSpawnPosition()
        {
            // Находим ближайшую точку пути к позиции врага
            Transform closestWaypoint = GetClosestWaypoint();

            // Получаем индекс следующей точки пути
            int nextWaypointIndex = (planeController.currentWaypointIndex + 1) % waypoints.Length;
            Transform nextWaypoint = waypoints[nextWaypointIndex];

            // Рассчитываем направление движения между текущей и следующей точкой пути
            Vector3 direction = (nextWaypoint.position - closestWaypoint.position).normalized;

            // Случайное смещение относительно пути по осям X и Y
            float randomOffsetX = Random.Range(-maxRandomOffset, maxRandomOffset);
            float randomOffsetY = Random.Range(-maxRandomOffset, maxRandomOffset);

            // Применяем смещение вдоль пути
            Vector3 offset = direction * zOffset;
            Vector3 spawnPosition = closestWaypoint.position + offset;
            spawnPosition.x += randomOffsetX;
            spawnPosition.y += randomOffsetY;

            return spawnPosition;
        }

        // Функция для поиска ближайшей точки пути
        Transform GetClosestWaypoint()
        {
            Transform closest = waypoints[0];
            float minDistance = Vector3.Distance(planeController.boundsCenter, closest.position);

            foreach (var waypoint in waypoints)
            {
                float distance = Vector3.Distance(planeController.boundsCenter, waypoint.position);
                if (distance < minDistance)
                {
                    closest = waypoint;
                    minDistance = distance;
                }
            }

            return closest;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = zoneColor;

            // Рисуем полупрозрачный куб
            Gizmos.DrawCube(zoneCenter, zoneSize);

            // Контур зоны
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(zoneCenter, zoneSize);
        }
}
        
         
