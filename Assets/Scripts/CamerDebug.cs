using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerDebug : MonoBehaviour
{
    public float lineLength = 10f;
    public Color lineColor = Color.red;

    void Update()
    {
        // Позиция камеры
        Vector3 startPosition = transform.position;

        // Направление камеры
        Vector3 endPosition = startPosition + transform.forward * lineLength;

        // Рисуем линию в направлении камеры
        Debug.DrawLine(startPosition, endPosition, lineColor);
    }
}
