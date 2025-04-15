using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollisionChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
           

        }
    }
}   

   
