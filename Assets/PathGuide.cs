using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class PathGuide : MonoBehaviour
{

    public Transform player; // Ссылка на игрока
    public Transform target; // Ссылка на цель
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Проверяем, первый ли это запуск
        if (PlayerPrefs.GetInt("FirstLaunch", 1) == 1)
        {
            lineRenderer.enabled = true;
            PlayerPrefs.SetInt("FirstLaunch", 0); // Помечаем, что первый запуск был
            PlayerPrefs.Save(); // Сохраняем состояние
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (lineRenderer.enabled)
        {
            // Строим линию от игрока до цели
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
        }
    }

}
