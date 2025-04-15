using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobloxCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float sensitivity = 5f;
    public float distanceMin = 2f;
    public float distanceMax = 10f;
    public float scrollSpeed = 2f;
    public float rotationSmoothTime = 0.12f;
    public Vector2 verticalClamp = new Vector2(-30, 60);

    public float collisionRadius = 0.3f; // Радиус для проверки коллизий
    public LayerMask collisionLayers;    // Слои, с которыми будет проверяться столкновение

    private Vector3 currentRotation;
    private Vector3 smoothVelocity;
    private float currentDistance;
    private float yaw;
    private float pitch;

    void Start()
    {
        currentDistance = offset.magnitude;
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Управление вращением камеры
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, verticalClamp.x, verticalClamp.y);
        }

        // Приближение и отдаление камеры
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        currentDistance = Mathf.Clamp(currentDistance, distanceMin, distanceMax);

        // Сглаживание вращения
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref smoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        // Позиционирование камеры с учётом коллизий
        Vector3 direction = new Vector3(0, 0, -currentDistance);
        Quaternion rotation = Quaternion.Euler(currentRotation);
        Vector3 desiredPosition = target.position + rotation * direction;

        // Проверка коллизий с помощью SphereCast
        RaycastHit hit;
        if (Physics.SphereCast(target.position, collisionRadius, desiredPosition - target.position, out hit, currentDistance, collisionLayers))
        {
            // Если есть столкновение, устанавливаем камеру перед препятствием
            currentDistance = hit.distance;
            desiredPosition = target.position + rotation * new Vector3(0, 0, -currentDistance);
        }

        transform.position = desiredPosition;
    }
}
