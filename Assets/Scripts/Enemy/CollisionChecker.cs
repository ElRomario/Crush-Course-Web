using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public Health health;

    private void Start()
    {
        
        health = GetComponent<Health>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            health.TakeDamage(1000);
            Debug.Log("You hit the wall!");
        }
    }

}
