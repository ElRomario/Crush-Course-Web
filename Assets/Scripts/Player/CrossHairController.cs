using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour
{


    [SerializeField] private Transform shootPoint; // ������� public, ���� ������ ������������� ����� ���������
    [SerializeField] private RectTransform crosshairUI;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RailPlaneController planeController;

    public float distance = 100f;

    private Vector3 lockedCrosshairPosition; // ���������� ������� �������
    private Vector3 lockedShootPointPosition; // ���������� ������� shootPoint
    private Quaternion lockedShootPointRotation; // ���������� ���������� shootPoint
    private bool isLocked = false;
    public bool aiming = false;

    void Start()
    {
        if (shootPoint == null)
        {
            shootPoint = GameObject.FindGameObjectWithTag("ShootPoint")?.transform;
            if (shootPoint == null) Debug.LogError("ShootPoint �� ������!");
        }

        if (crosshairUI == null)
        {
            crosshairUI = GameObject.Find("CrosshairUI")?.GetComponent<RectTransform>();
            if (crosshairUI == null) Debug.LogError("CrosshairUI �� ������!");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) Debug.LogError("Main camera �� ������!");
        }

        if (planeController == null)
        {
            planeController = FindObjectOfType<RailPlaneController>();
            if (planeController == null) Debug.LogError("PlaneController �� ������!");
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
            Debug.LogWarning("CrosshairUI �� ����������!");
            return;
        }

        // ���������� ���������� ���� ��� ������� ������ ������� � ����
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position; // ���� ������ ����� �������
        }
        else
        {
            inputPosition = Input.mousePosition; // ���������� ����
        }

        // ����������� UI-������ � ������� �����
        crosshairUI.position = inputPosition;
    }

    private void LockCrosshairAndShootPoint()
    {
        lockedCrosshairPosition = crosshairUI.position; // ��������� ������� �������
        lockedShootPointPosition = shootPoint.position; // ��������� ������� shootPoint
        lockedShootPointRotation = shootPoint.rotation; // ��������� ���������� shootPoint
        isLocked = true; // ������������� ���� ��������
    }

    private void UnlockCrosshairAndShootPoint()
    {
        isLocked = false; // ���������� ��������
    }
}