using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagRotation : MonoBehaviour
{

    private Vector3 rotatiom = new Vector3(0, 0, 10f);
    
    void Update()
    {
        transform.Rotate(rotatiom);
    }
}
