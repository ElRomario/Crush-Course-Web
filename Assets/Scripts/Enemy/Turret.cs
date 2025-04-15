using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject missilePrefab; // Префаб ракеты
    public Transform missileSpawnPoint; // Точка спавна ракеты
    public Transform turretHead; // Часть турели, которая поворачивается
    public Transform player; // Цель — самолёт игрока
    public float fireRate = 3f; // Интервал стрельбы
    public float rotationSpeed = 5f; // Скорость поворота турели

    private float nextFireTime = 0f;

    void Update()
    {
        if (player != null)
        {
            RotateToPlayer();
            FireMissileIfReady();
        }
    }

    void RotateToPlayer()
    {
        // Направляем турель в сторону игрока
        Vector3 direction = (player.position - turretHead.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void FireMissileIfReady()
    {
        // Проверяем, может ли турель стрелять
        if (Time.time >= nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireMissile()
    {
        //// Создаём ракету и задаём ей цель
        //GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        //HomingMissile homingMissile = missile.GetComponent<HomingMissile>();

        //if (homingMissile != null && player != null)
        //{
        //    homingMissile.SetTarget(player); // Передаём цель ракете
        //}
    }
}
