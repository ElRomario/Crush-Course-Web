using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Transform[] waypoints; // Массив точек пути
    private float moveToPathSpeed; // Скорость движения к ближайшей точке пути
    private float pathSpeed; // Скорость движения по пути
    private bool movingToPath = false; // Флаг движения к пути
    private bool movingOnPath = false; // Флаг движения по пути
    private int currentWaypointIndex = 0; // Индекс текущей точки пути

    public void Initialize(Transform[] waypoints, float moveToPathSpeed, float pathSpeed)
    {
        this.waypoints = waypoints;
        this.moveToPathSpeed = moveToPathSpeed;
        this.pathSpeed = pathSpeed;

        if (waypoints != null && waypoints.Length > 0)
        {
            movingToPath = true; // Начинаем движение к ближайшей точке пути
            currentWaypointIndex = FindClosestWaypointIndex();
        }
    }

    void Update()
    {
        if (movingToPath)
        {
            MoveToPath();
        }
        else if (movingOnPath)
        {
            MoveAlongPath();
        }
    }

    private int FindClosestWaypointIndex()
    {
        // Находим индекс ближайшей точки пути
        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private void MoveToPath()
    {
        // Двигаемся к ближайшей точке пути
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveToPathSpeed * Time.deltaTime);

        // Если достигли ближайшей точки пути, переключаемся на движение по пути
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            movingToPath = false;
            movingOnPath = true;
        }
    }

    private void MoveAlongPath()
    {
        // Двигаемся по точкам пути
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, pathSpeed * Time.deltaTime);

        // Если достигли текущей точки пути, переключаемся на следующую
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Зацикливаем движение по пути
            }
        }
    }
}
