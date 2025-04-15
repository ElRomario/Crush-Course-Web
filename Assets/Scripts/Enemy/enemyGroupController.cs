using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float appearDuration = 3f; // Время, которое группа противников проходит через экран
    public float pathDuration = 10f; // Время движения по пути
    public float pathSpeed = 3f; // Скорость движения по пути
    public float moveSpeed = 5f; // Скорость движения к игроку
    public Transform[] waypoints; // Точки пути для движения
    public Transform player; // Ссылка на игрока
    public float delayBeforeTargetingPlayer = 2f; // Задержка перед движением к игроку

    private bool movingOnPath = false; // Флаг для движения по пути
    private bool movingToPlayer = false; // Флаг для движения к игроку
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Найти игрока
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Запустить появление и движение
        StartCoroutine(MoveThroughScreen());
    }

    IEnumerator MoveThroughScreen()
    {
        Vector3 startPosition = GetRandomAppearPosition();
        Vector3 endPosition = CalculateOffscreenPosition();

        float elapsedTime = 0f;

        while (elapsedTime < appearDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / appearDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        // Начать движение по пути
        StartMovingOnPath();
    }

    Vector3 GetRandomAppearPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-6f, 6f);
        return new Vector3(randomX, randomY, 0f);
    }

    Vector3 CalculateOffscreenPosition()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        return new Vector3(screenBounds.x + 2f, transform.position.y, 0f);
    }

    void StartMovingOnPath()
    {
        movingOnPath = true;
        Invoke("StartTargetingPlayer", pathDuration);
    }

    void StartTargetingPlayer()
    {
        movingOnPath = false;
        movingToPlayer = true;
    }

    void Update()
    {
        if (movingOnPath)
        {
            MoveAlongPath();
        }
        else if (movingToPlayer)
        {
            MoveToPlayer();
        }
    }

    void MoveAlongPath()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, pathSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
