using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerDebug : MonoBehaviour
{
    public float lineLength = 10f;
    public Color lineColor = Color.red;

    void Update()
    {
        // ������� ������
        Vector3 startPosition = transform.position;

        // ����������� ������
        Vector3 endPosition = startPosition + transform.forward * lineLength;

        // ������ ����� � ����������� ������
        Debug.DrawLine(startPosition, endPosition, lineColor);
    }
}
