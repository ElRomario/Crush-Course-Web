using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    public GameObject bulletPrefab;  // Префаб пули
    public Transform shootPoint;     // Точка спавна пули
    private float fireRate = 1f;      // Скорость стрельбы (пули в секунду)
    private float bulletSpeed = 10f;  // Скорость полёта пули
    private float spread = 1f;        // Разброс выстрелов (в градусах)

    private Transform player;        // Ссылка на игрока
    private float nextFireTime = 0f;

    public void SetupShooting(Transform newShootPoint, GameObject newBulletPrefab, float newFireRate, float newBulletSpeed, float newSpread)
    {
        // Проверяем, был ли передан новый shootPoint
        if (newShootPoint != null)
        {
            shootPoint = newShootPoint;
        }

        bulletPrefab = newBulletPrefab;
        fireRate = newFireRate;
        bulletSpeed = newBulletSpeed;
        spread = newSpread;
    }

    void Start()
    {
        // Находим игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // Стреляем с заданной частотой
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        if (player == null || shootPoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("Параметры для стрельбы не заданы корректно.");
            return;
        }

        // Вычисляем направление на игрока
        Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;

        // Добавляем разброс
        directionToPlayer += new Vector3(
            Random.Range(-spread, spread) * 0.01f,
            Random.Range(-spread, spread) * 0.01f,
            Random.Range(-spread, spread) * 0.01f
        );

        // Создаём пулю
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(directionToPlayer));

        // Передаём в пулю параметры движения
        BulletMovement bulletScript = bullet.GetComponent<BulletMovement>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(directionToPlayer, bulletSpeed);
        }

        // Уничтожаем пулю через 5 секунд
        Destroy(bullet, 5f);
    }
    void OnDrawGizmos()
    {
        if (shootPoint != null && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(shootPoint.position, player.position);
        }
    }
}
