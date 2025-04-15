using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealthManager : MonoBehaviour
{
    private Health health;

    [Header("UI ��������")]
    public RectTransform healthBarFill; // ������ �� ������� HealthBar

    private float originalWidth; // �������� ������ HealthBarFill

    void Awake()
    {
        health = GetComponent<Health>();

        // ������������� �� ������� ��������
        health.OnHealthChanged += UpdateUI;
        health.OnDeath += HandleDeath;

        // ��������� �������� ������ HealthBarFill
        if (healthBarFill != null)
        {
            originalWidth = healthBarFill.sizeDelta.x;
        }
    }

    void UpdateUI(float current, float max)
    {
        // ���������, ��� healthBarFill ����������
        if (healthBarFill == null) return;

        // ������������ ����� ������
        float newWidth = (current / max) * originalWidth;

        // ��������� ������ HealthBarFill
        healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
    }

    void HandleDeath()
    {
        Debug.Log("����� ����. ���������� ����� ���������.");
    }
}
