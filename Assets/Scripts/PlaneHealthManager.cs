using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealthManager : MonoBehaviour
{
    private Health health;

    [Header("UI элементы")]
    public RectTransform healthBarFill; // Ссылка на заливку HealthBar

    private float originalWidth; // Исходная ширина HealthBarFill

    void Awake()
    {
        health = GetComponent<Health>();

        // Подписываемся на события здоровья
        health.OnHealthChanged += UpdateUI;
        health.OnDeath += HandleDeath;

        // Сохраняем исходную ширину HealthBarFill
        if (healthBarFill != null)
        {
            originalWidth = healthBarFill.sizeDelta.x;
        }
    }

    void UpdateUI(float current, float max)
    {
        // Проверяем, что healthBarFill установлен
        if (healthBarFill == null) return;

        // Рассчитываем новую ширину
        float newWidth = (current / max) * originalWidth;

        // Обновляем размер HealthBarFill
        healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
    }

    void HandleDeath()
    {
        Debug.Log("Игрок умер. Показываем экран поражения.");
    }
}
