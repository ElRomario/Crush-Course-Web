using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public float maxHealth = 100f; // Максимальное здоровье
    public float currentHealth;

    [Header("UI элементы")]
    public RectTransform healthBarFill; // Ссылка на RectTransform заливки

    private float originalWidth; // Исходная ширина HealthBarFill

    void Start()
    {
        // Устанавливаем начальные значения здоровья
        currentHealth = maxHealth;

        // Сохраняем исходную ширину HealthBarFill
        if (healthBarFill != null)
        {
            originalWidth = healthBarFill.sizeDelta.x;
        }
    }

    public void TakeDamage(float damage)
    {
        // Уменьшаем здоровье
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Обновляем размер HealthBarFill
        UpdateHealthBar();

        // Проверяем смерть
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            // Рассчитываем новую ширину
            float newWidth = (currentHealth / maxHealth) * originalWidth;

            // Устанавливаем новую ширину HealthBarFill
            healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
        }
    }

    void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Персонаж умер!");
        // Логика смерти
    }
}
