using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  

public class CameraFollowPlane : MonoBehaviour
{
    public string targetTag = "Plane"; // Тэг целевого объекта
    public float rotationSpeed = 5f; // Скорость поворота камеры

    private Transform target;

    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("Объект с тэгом '" + targetTag + "' не найден!");
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

