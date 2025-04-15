using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Transform[] waypoints; // Массив точек пути

    // Этот метод рисует путь в редакторе с помощью Gizmos
    public void DrawPath()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        // Пройдем по всем точкам пути и рисуем линии между ними
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.color = Color.red; // Задаем цвет линии (можно изменить)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position); // Рисуем линию между точками
            }
        }
    }
}
