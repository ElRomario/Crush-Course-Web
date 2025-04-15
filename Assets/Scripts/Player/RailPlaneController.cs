using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlaneController : MonoBehaviour
{

    [Header("Plane Settings")]
    public float speed = 10f;
    public float tiltSpeed = 5f;
    public float maxTiltAngle = 30f;

    [Header("Barrel Roll Settings")]
    public float barrelRollSpeed = 720f; // Скорость выполнения бочки в градусах в секунду.
    public float barrelRollDistance = 2f; // Дальность смещения по оси X при выполнении бочки.
    public float barrelRollEasePower = 2f; // Степень сглаживания замедления (чем больше, тем сильнее замедление).

    public float smoothTransitionSpeed = 5f; // Скорость плавного перехода после бочки.
    public bool isBarrelRolling = false; // Флаг, указывающий, выполняется ли бочка.
    private float barrelRollProgress = 0f; // Текущий прогресс выполнения бочки (в градусах).
    private float barrelRollDirection = 0f; // Направление бочки (-1 для вправо, 1 для влево).
    private Vector3 initialPosition; // Начальная позиция самолёта перед бочкой.
    private float savedZPosition;
    private bool isTransitioning;
    private Vector3 targetPosition; // Целевая позиция для перехода после бочки.
    private Quaternion targetRotation; // Целевая ротация для перехода после бочки.

    public delegate void BarrelRollAction();
    public static event BarrelRollAction OnBarrelRoll;

    public bool isMovingVerticaL;
    public bool isMovingHorizontal;

    private AudioManager audioManager;


    [Header("Bounds Settings")]
    public Vector2 boundsSize = new Vector2(10f, 6f);
    public Vector3 boundsOffset = new Vector3(0f, 0f, 10f);

    [Header("Path Settings")]
    public Transform[] waypoints;
    public float pathSpeed = 5f;

    public int currentWaypointIndex = 0;
    public Vector3 boundsCenter;
    private bool movingAlongPath = true;

    private Quaternion defaultRotation;
    private Vector3 currentTilt;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
       
        
    }
    private void Start()
    {
        if (waypoints.Length > 0)
        {

            boundsCenter = transform.position + boundsOffset;
        }


        defaultRotation = transform.rotation;
    }

    public float GetSpeed()
    {
        return pathSpeed;
    }

    private void Update()
    {

        // Двигаем boundsCenter по пути, даже если выполняется бочка.
        if (waypoints.Length > 0)
        {
            MoveBoundsAlongPath();
        }

        if (isBarrelRolling)
        {
            PerformBarrelRoll(); // Выполняем бочку.
        }
        else
        {
            HandlePlaneMovement(); // Управление самолётом.
            HandlePlaneTilt();     // Наклон самолёта.

            // Запуск бочки.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                audioManager.PlaySFX(audioManager.roll);
                StartBarrelRoll(-1); // Бочка влево.
                OnBarrelRoll?.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                audioManager.PlaySFX(audioManager.roll);
                StartBarrelRoll(1); // Бочка вправо.
                OnBarrelRoll?.Invoke();
            }
        }

        //if (isTransitioning)
        //{
        //    SmoothTransitionToNormalState(); // Плавный возврат после бочки.
        //}
    }

    //private void SmoothTransitionToNormalState()
    //{
    //    // Плавное возвращение позиции и наклона к обычному состоянию.
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTransitionSpeed * Time.deltaTime);
    //    transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothTransitionSpeed * Time.deltaTime);

    //    // Завершение перехода, если достаточно близко к целевым значениям.
    //    if (Vector3.Distance(transform.position, targetPosition) < 0.01f &&
    //        Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
    //    {
    //        isTransitioning = false; // Завершаем переход.
    //    }
    //}

    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical"); // Или любая другая обработка ввода
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal"); // Или любая другая обработка
    }

    void MoveBoundsAlongPath()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];


        boundsCenter = Vector3.MoveTowards(boundsCenter, targetWaypoint.position, pathSpeed * Time.deltaTime);


        if (Vector3.Distance(boundsCenter, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    void HandlePlaneMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0)
            isMovingHorizontal = true;
        else
        {
            isMovingHorizontal = false;
        }

        if (verticalInput != 0)
            isMovingHorizontal = true;
        else
        {
            isMovingHorizontal = false;
        }

        Vector3 moveDelta = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        Vector3 targetPosition = transform.position + moveDelta;


        float halfWidth = boundsSize.x / 2f;
        float halfHeight = boundsSize.y / 2f;

        targetPosition.x = Mathf.Clamp(targetPosition.x, boundsCenter.x - halfWidth, boundsCenter.x + halfWidth);
        targetPosition.y = Mathf.Clamp(targetPosition.y, boundsCenter.y - halfHeight, boundsCenter.y + halfHeight);
        targetPosition.z = boundsCenter.z;


        transform.position = targetPosition;
    }

    void HandlePlaneTilt()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        float tiltX = -verticalInput * maxTiltAngle;
        float tiltZ = -horizontalInput * maxTiltAngle;


        currentTilt = new Vector3(tiltX, 0f, tiltZ);

        Quaternion targetRotation = Quaternion.Euler(currentTilt);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, tiltSpeed * Time.deltaTime);
    }



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        float halfWidth = boundsSize.x / 2f;
        float halfHeight = boundsSize.y / 2f;

        Vector3 topLeft = new Vector3(boundsCenter.x - halfWidth, boundsCenter.y + halfHeight, boundsCenter.z);
        Vector3 topRight = new Vector3(boundsCenter.x + halfWidth, boundsCenter.y + halfHeight, boundsCenter.z);
        Vector3 bottomLeft = new Vector3(boundsCenter.x - halfWidth, boundsCenter.y - halfHeight, boundsCenter.z);
        Vector3 bottomRight = new Vector3(boundsCenter.x + halfWidth, boundsCenter.y - halfHeight, boundsCenter.z);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        Gizmos.color = Color.green;
        if (waypoints != null && waypoints.Length > 1)
        {
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }

    private void StartBarrelRoll(float direction)
    {
        isBarrelRolling = true;
        barrelRollProgress = 0f; // Сброс прогресса выполнения.
        barrelRollDirection = direction; // Направление бочки.
        initialPosition = transform.position; // Запоминаем начальную позицию самолёта.
        isTransitioning = false; // Отключаем плавный переход.
    }

    private void PerformBarrelRoll()
    {
        // Нормализованный прогресс выполнения бочки (от 0 до 1).
        float rollProgressNormalized = Mathf.Clamp01(barrelRollProgress / 360f);

        // Применяем нелинейную функцию сглаживания для замедления ближе к концу.
        float easedProgress = Mathf.Pow(1f - rollProgressNormalized, barrelRollEasePower);

        // Угол поворота для текущего кадра.
        float rotationStep = barrelRollSpeed * easedProgress * Time.deltaTime;
        barrelRollProgress += rotationStep;

        // Вращение вокруг локальной оси Z (самолёт делает бочку).
        transform.Rotate(0f, 0f, rotationStep * barrelRollDirection, Space.Self);

        // Линейное смещение по оси X во время выполнения бочки (от начальной позиции).
        float horizontalOffset = Mathf.Lerp(0f, barrelRollDistance * barrelRollDirection, rollProgressNormalized);

        // Постоянное движение вперёд по оси Z.
        float forwardMovement = pathSpeed * Time.deltaTime;

        // Обновляем позицию самолёта.
        transform.position = new Vector3(
            initialPosition.x + horizontalOffset, // Смещение от начальной позиции.
            initialPosition.y,                    // Фиксированное положение по Y.
            transform.position.z + forwardMovement // Постоянное движение вперёд.
        );

        // Завершение бочки после вращения на 360°.
        if (barrelRollProgress >= 360f)
        {
            isBarrelRolling = false; // Завершаем бочку.
            barrelRollProgress = 0f; // Сбрасываем прогресс.

            // Сохраняем текущую позицию (с учётом смещения) и восстанавливаем угол наклона.
            targetPosition = transform.position; // Позиция после завершения бочки.
            targetRotation = Quaternion.Euler(currentTilt); // Стандартный наклон.
            isTransitioning = true; // Начинаем плавный переход.
        }
    }
}


