using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class MissileShooter : MonoBehaviour
{

    public GameObject missilePrefab; // Префаб ракеты
    public Transform[] missileSpawnPoints; // Точки спавна ракет
    public float fireRate = 1f; // Скорость стрельбы
    protected float nextFireTime;

    // Метод для получения целей — переопределяется в наследниках
    protected abstract List<GameObject> GetTargets();

    // Выстрел ракет
    public virtual void FireMissiles()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + 1f / fireRate;

        List<GameObject> targets = GetTargets();

        if (targets == null || targets.Count == 0) return;
        
        // Создаём очередь целей
        Queue<GameObject> targetQueue = new Queue<GameObject>(targets);
            if (CompareTag("Player"))
                 {
                  MissileUIManager.Instance.MissileCount--;
                 }
        // Выпускаем ракеты
        foreach (Transform spawnPoint in missileSpawnPoints)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.missileDropDown);
            
            GameObject missile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);

            // Назначаем цель ракете
            GameObject target = targetQueue.Count > 0 ? targetQueue.Dequeue() : null;
            missile.GetComponent<Missile>().SetTarget(target);

            // Если цели ещё остались, добавляем их обратно в очередь
            if (target != null) targetQueue.Enqueue(target);

            // Если это игрок, обновляем интерфейс
            
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.missileLaunch);
        Debug.Log("Missiles were shot!");
    }
}
