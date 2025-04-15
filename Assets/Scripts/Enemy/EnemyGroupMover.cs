using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupMover : MonoBehaviour
{

    private Transform[] waypoints; // Точки пути
    private float moveToPathSpeed; // Скорость перемещения к начальной точке пути
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
            movingToPath = true; // Начинаем движение к первой точке пути
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

    private void MoveToPath()
    {
        // Двигаемся к первой точке пути
        Transform targetWaypoint = waypoints[0];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveToPathSpeed * Time.deltaTime);

        // Если достигли первой точки пути, переходим к движению по пути
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            movingToPath = false;
            movingOnPath = true;
        }
    }

    private void MoveAlongPath()
    {
        // Двигаемся по точкам пути
        if (waypoints == null || waypoints.Length == 0) return;

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
