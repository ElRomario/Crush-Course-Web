using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 50f; // Скорость движения вперед
    public float pitchSpeed = 100f; // Скорость тангажа (наклон вверх/вниз)
    public float rollSpeed = 100f; // Скорость крена (наклон влево/вправо)
    public float yawSpeed = 50f; // Скорость рыскания (поворот вокруг вертикальной оси)
    public float stabilizationSpeed = 2f; // Скорость стабилизации после столкновения

    private Rigidbody rb;
    private bool shouldStabilize = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Выключаем гравитацию, если самолет должен летать
    }

    void Update()
    {
        // Управление игроком
        float pitch = Input.GetAxis("Vertical"); // W/S или стрелки
        float roll = Input.GetAxis("Horizontal"); // A/D или стрелки
        float yaw = Input.GetAxis("Yaw"); // Дополнительная ось
        float throttle = Mathf.Clamp01(Input.GetAxis("Throttle")); // Ускорение (0-1)

        // Вращение
        Vector3 rotation = new Vector3(pitch * pitchSpeed, yaw * yawSpeed, -roll * rollSpeed) * Time.deltaTime;
        transform.Rotate(rotation);

        // Движение вперед
        rb.velocity = transform.forward * speed;

        // Стабилизация после столкновения
        if (shouldStabilize)
        {
            Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * stabilizationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                shouldStabilize = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Включаем стабилизацию
        shouldStabilize = true;

        // Останавливаем случайное вращение
        rb.angularVelocity = Vector3.zero;
    }
}
