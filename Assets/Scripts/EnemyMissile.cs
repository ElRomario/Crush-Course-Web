using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Missile
{
    [SerializeField] private float detonationDistance = 100f; // Расстояние до игрока для детонации
    private bool isDodged = false; // Флаг уворота
    private bool isMovingChaotically = false; // Флаг хаотичного движения
    [SerializeField] private float chaosDuration = 3f; // Длительность хаотичного движения
    [SerializeField] private float chaosDirectionChangeInterval = 0.5f; // Интервал смены направления
    [SerializeField] private float damage = 10f;
    private float chaosTimer; // Таймер хаотичного движения
    public WarningTextController warningTextController;
    public GameObject hit;

    protected override void Start()
    {


        warningTextController = GameObject.FindGameObjectWithTag("MissileInd").GetComponent<WarningTextController>();
        // Эти параметры можно редактировать в инспекторе для ракеты противника
        dropTime = 0; // Время падения ракеты противника
        speed = 130f; // Скорость ракеты противника
        turnSpeed = 4f; // Скорость поворота ракеты противника

        base.Start();
        RailPlaneController.OnBarrelRoll += OnPlayerBarrelRoll;
    }

    protected override void OnHitTarget(GameObject target)
    {
        // Специфическое поведение при попадании для ракеты противника
        Debug.Log("Enemy missile hit " + target.name);
        base.OnHitTarget(target);
    }

    protected override void Update()
    {
        base.Update();

        if (isMovingChaotically)
        {
            ChaosMovement();
        }
        else if (target != null && !isDodged)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= detonationDistance)
            {
                Debug.Log("Missile is close to the player. Waiting for dodge...");
                warningTextController.StartBlinking();
            }
        }
    }

    private void OnPlayerBarrelRoll()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= detonationDistance)
            {
                Debug.Log("Player performed barrel roll! Missile is dodged.");
                isDodged = true;
                StartChaosMovement();
                StartCoroutine(SelfDestructAfterDelay());
            }
        }
    }

    private System.Collections.IEnumerator SelfDestructAfterDelay()
    {
        // Ракета сбивается с курса (например, отклонение)
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; // Ограничить отклонение по вертикали
        transform.rotation = Quaternion.LookRotation(randomDirection);

        // Через 2 секунды ракета уничтожается
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void StartChaosMovement()
    {
        isMovingChaotically = true;
        chaosTimer = chaosDuration; // Устанавливаем длительность хаотичного движения
        InvokeRepeating(nameof(ChangeChaosDirection), 0f, chaosDirectionChangeInterval); // Регулярно меняем направление
        warningTextController.StopBlinking();
    }

    private void OnDestroy()
    {
        RailPlaneController.OnBarrelRoll -= OnPlayerBarrelRoll; // Отписка от события
    }

    private void ChaosMovement()
    {
        // Считаем оставшееся время хаотичного движения
        chaosTimer -= Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (chaosTimer <= 0)
        {
            StopChaosMovement();
        }
    }

    private void ChangeChaosDirection()
    {
        // Случайно изменяем направление ракеты
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0; // Отключаем вертикальное направление
        transform.rotation = Quaternion.LookRotation(randomDirection);
    }

    private void StopChaosMovement()
    {
        isMovingChaotically = false;
        CancelInvoke(nameof(ChangeChaosDirection)); // Останавливаем смену направления
        Destroy(gameObject); // Уничтожаем ракету
    }

    public float GetDamageAmount()
    {
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, есть ли у объекта компонент Health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            AudioManager.Instance.PlayExplosions();
            Instantiate(hit, transform.position, Random.rotationUniform);
            health.TakeDamage(GetDamageAmount());
            Destroy(gameObject); // Удаляем пулю после попадания
        }
    }

}

