using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Vector3 spawnOffset = Vector3.zero;
    public float projectileSpeed = 20f;
    public float projectileLifetime = 5f;
    public bool aiming = false;

    private Camera mainCamera;
    private AudioManager audioManager;

    private void Awake()
    {
        mainCamera = Camera.main;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

   

    void Update()
    {
        
            AimTowardsMouse();
        

        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void AimTowardsMouse()
    {
        if (mainCamera == null) return;

        Vector3 inputPosition;

        // ���������� ������� ����� (���� ��� �����)
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position; // ����� ������ ����� �������
        }
        else
        {
            inputPosition = Input.mousePosition; // ���� ��� �������, ���������� ����
        }

        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = (hit.point - shootPoint.position).normalized;
            shootPoint.rotation = Quaternion.LookRotation(direction, Vector3.up); // ������������, �� �� ������ �������
        }
    }

    void ShootProjectile()
    {
        audioManager.PlaySFX(audioManager.laserShoot);

        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("�� ����� projectilePrefab ��� shootPoint!");
            return;
        }

        int layerMask = ~LayerMask.GetMask("PlayerLayer");

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("Enemy")) // ��������� �� �����
            {
                Debug.Log("Hit enemy: " + hit.collider.name);
                // ������� ���� �����
            }
        }

        // ������� � ����� � ��� �� �����, ���������� �� �����������
        Vector3 spawnPosition = shootPoint.position + shootPoint.TransformDirection(spawnOffset);
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, shootPoint.rotation);

        ProjectileBehavioiur projectileScript = projectile.GetComponent<ProjectileBehavioiur>();
        if (projectileScript != null)
        {
            projectileScript.speed = projectileSpeed;
            projectileScript.lifetime = projectileLifetime;
            projectileScript.SetDirection(shootPoint.forward);
        }
        else
        {
            Debug.LogWarning("� ������� ����������� ��������� Projectile!");
        }
    }
}
