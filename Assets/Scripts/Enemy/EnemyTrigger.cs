using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    public GameObject enemyGroupPrefab; // Префаб группы врагов
    public Transform spawnPoint; // Точка спавна группы врагов
    public float spawnDelay = 0f; // Задержка перед спавном группы врагов

    private bool hasTriggered = false; // Флаг, чтобы сработать один раз

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что триггер пересёк игрок
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Invoke("SpawnEnemyGroup", spawnDelay);
        }
    }

    void SpawnEnemyGroup()
    {
        // Создаём группу врагов в указанной точке
        if (enemyGroupPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyGroupPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
