using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public delegate void HealthChanged(float current, float max);
    public event HealthChanged OnHealthChanged; // Событие для UI или других систем

    public delegate void Death();
    public event Death OnDeath; // Событие для логики смерти
    public GameObject explosion;
    public SceneReloader sceneReloader;
    public bool isDead = false;
    

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем здоровье на максимум
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //public void Heal(float amount)
    //{
    //    currentHealth += amount;
    //    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    //    OnHealthChanged?.Invoke(currentHealth, maxHealth);
    //}

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        AudioManager.Instance.PlayExplosions();
        Instantiate(explosion, transform.position, Random.rotationUniform);// Вызываем событие смерти
        Destroy(gameObject); // Удаляем объект или обрабатываем как нужно
        Cursor.lockState = CursorLockMode.None; // Разблокирует курсор
        Cursor.visible = true;
    }
}
