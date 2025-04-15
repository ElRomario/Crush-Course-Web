using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEntryTrigger : MonoBehaviour
{
    public static bool canTakeDamage = false;


    public MeshCollider tunnelCollider; // Ссылка на MeshCollider туннеля

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelCollider.enabled = true; // Включаем MeshCollider
            Debug.Log("Коллизия туннеля включена!");

            // Принудительно вызываем урон, если игрок уже в туннеле
            if (tunnelCollider.bounds.Contains(other.transform.position))
            {
                 
                Debug.Log("Игрок уже внутри туннеля - наносим урон.");
            }
        }
    }
}
