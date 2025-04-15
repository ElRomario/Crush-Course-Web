using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб врага
    public Transform[] spawnPoints; // Точки спавна
    public PathFinder[] possiblePaths; // Массив путей (массив объектов класса Path)

    private void Start()
    {
        // Получаем все дочерние объекты объекта, к которому привязан EnemySpawner (SpawnPoints)
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnPoints = System.Array.FindAll(spawnPoints, point => point != transform); // Убираем сам объект

        // Начинаем спавн врагов
        InvokeRepeating(nameof(SpawnEnemy), 0f, 2f);
    }

    void SpawnEnemy()
    {
        // Случайным образом выбираем точку для спавна
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Случайным образом выбираем один из путей
        PathFinder selectedPath = possiblePaths[Random.Range(0, possiblePaths.Length)];

        // Создаём врага в выбранной точке
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Передаём путь врагу для его движения, начиная с точки ближайшей к спавну
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            // Находим ближайшую точку пути к спавну
            Transform startPoint = FindNearestWaypoint(selectedPath.waypoints, spawnPoint.position);
            // Получаем путь начиная с этой точки
            Transform[] newPath = GetPathFromStartPoint(selectedPath.waypoints, startPoint);

            // Передаем новый путь врагу
            enemyMovement.waypoints = newPath;
        }

        Debug.Log($"Spawned enemy at: {spawnPoint.position}, Path: {selectedPath.waypoints.Length} waypoints");
    }

    // Метод для поиска ближайшей точки пути к точке спавна
    Transform FindNearestWaypoint(Transform[] waypoints, Vector3 spawnPosition)
    {
        Transform nearestWaypoint = waypoints[0];
        float nearestDistance = Vector3.Distance(spawnPosition, nearestWaypoint.position);

        foreach (var waypoint in waypoints)
        {
            float distance = Vector3.Distance(spawnPosition, waypoint.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestWaypoint = waypoint;
            }
        }

        return nearestWaypoint;
    }

    // Метод для получения пути начиная с точки, которая ближайше к спавну
    Transform[] GetPathFromStartPoint(Transform[] waypoints, Transform startPoint)
    {
        int startIndex = System.Array.IndexOf(waypoints, startPoint);
        Transform[] newPath = new Transform[waypoints.Length - startIndex];
        System.Array.Copy(waypoints, startIndex, newPath, 0, newPath.Length);
        return newPath;
    }

    // Этот метод будет вызываться для отображения путей в редакторе
    private void OnDrawGizmos()
    {
        if (possiblePaths == null) return;

        // Пройдем по всем путям и нарисуем их
        foreach (var path in possiblePaths)
        {
            if (path != null)
            {
                path.DrawPath(); // Рисуем путь для каждого объекта Path
            }
        }
    }
}
