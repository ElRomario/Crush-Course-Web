using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // ����� ��������
    [SerializeField] private float radius = 5f;     // ������ ��������
    [SerializeField] private float rotationSpeed = 30f; // �������� �������� (������� � �������)

    private float currentAngle = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        if (centerPoint == null)
        {
            Debug.LogWarning("Center Point �� ��������!");
            return;
        }

        // �������������� ������� ������� �� ���������� � �������� ��������
        UpdatePosition();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (centerPoint == null) return;

        // ��������� ���� � ����������� �� ������� � �������� ��������
        currentAngle += rotationSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        // ��������� ������� �������
        UpdatePosition();

        // ������������ ������ � ������� ��������
        Vector3 direction = (transform.position - lastPosition).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // ���������� ��������� �������
        lastPosition = transform.position;
    }

    private void UpdatePosition()
    {
        // ������������ ����� ������� �� ����������
        float x = centerPoint.position.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float z = centerPoint.position.z + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
