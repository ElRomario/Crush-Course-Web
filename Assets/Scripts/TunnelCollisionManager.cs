using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelCollisionManager : MonoBehaviour
{
    public MeshCollider tunnelMeshCollider; // Ссылка на MeshCollider туннеля

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelMeshCollider.enabled = false; // Отключаем коллизию туннеля
            Debug.Log("Игрок вошёл в туннель - коллизия отключена.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tunnelMeshCollider.enabled = true; // Включаем коллизию туннеля
            Debug.Log("Игрок покинул входной триггер - коллизия включена.");
        }
        
    }
}
