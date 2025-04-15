using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{


    [SerializeField] private Transform shootPoint; // Убираем public, если ссылки присваиваются через инспектор
    [SerializeField] private RectTransform crosshairUI;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RailPlaneController planeController;

    public float distance = 100f;

    private Vector3 lockedCrosshairPosition; // Сохранённая позиция прицела
    private Vector3 lockedShootPointPosition; // Сохранённая позиция shootPoint
    private Quaternion lockedShootPointRotation; // Сохранённая ориентация shootPoint
    private bool isLocked = false;
    public bool aiming = false;

    void Start()
    {
        if (shootPoint == null)
        {
            shootPoint = GameObject.FindGameObjectWithTag("ShootPoint")?.transform;
            if (shootPoint == null) Debug.LogError("ShootPoint не найден!");
        }

        if (crosshairUI == null)
        {
            crosshairUI = GameObject.Find("CrosshairUI")?.GetComponent<RectTransform>();
            if (crosshairUI == null) Debug.LogError("CrosshairUI не найден!");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) Debug.LogError("Main camera не найден!");
        }

        if (planeController == null)
        {
            planeController = FindObjectOfType<RailPlaneController>();
            if (planeController == null) Debug.LogError("PlaneController не найден!");
        }
    }

    public void setCursour()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void startAiming()
    {
        aiming = true;
        setCursour();
    }

    public void stopAiming()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
        

    void Update()
    {
        if (crosshairUI == null)
        {
            Debug.LogWarning("CrosshairUI не установлен!");
            return;
        }

        // Используем координаты мыши или касания вместо расчёта в мире
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position; // Берём первую точку касания
        }
        else
        {
            inputPosition = Input.mousePosition; // Используем мышь
        }

        // Привязываем UI-прицел к позиции ввода
        crosshairUI.position = inputPosition;
    }

    private void LockCrosshairAndShootPoint()
    {
        lockedCrosshairPosition = crosshairUI.position; // Фиксируем позицию прицела
        lockedShootPointPosition = shootPoint.position; // Фиксируем позицию shootPoint
        lockedShootPointRotation = shootPoint.rotation; // Фиксируем ориентацию shootPoint
        isLocked = true; // Устанавливаем флаг фиксации
    }

    private void UnlockCrosshairAndShootPoint()
    {
        isLocked = false; // Сбрасываем фиксацию
    }
}