using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СamerFollow : MonoBehaviour
{

    [Header("Camera Settings")]
    public Transform target; 
    public Vector3 offset = new Vector3(0f, 0f, -10f); 
    public float followSpeed = 5f; 

    [Header("Path Settings")]
    public float pathSpeed = 2f; 

    [Header("Bounds Settings")]
    public Vector2 screenBounds = new Vector2(5f, 3f); 

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        FollowTarget();
        
        
    }

    void FollowTarget()
    {
        if (target == null) return;

        
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        
        Vector3 topLeft = new Vector3(-screenBounds.x, screenBounds.y, transform.position.z);
        Vector3 topRight = new Vector3(screenBounds.x, screenBounds.y, transform.position.z);
        Vector3 bottomLeft = new Vector3(-screenBounds.x, -screenBounds.y, transform.position.z);
        Vector3 bottomRight = new Vector3(screenBounds.x, -screenBounds.y, transform.position.z);

        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}

