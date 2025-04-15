using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class helthbarMovement : MonoBehaviour
{

    public RectTransform crosshair;

    void Update()
    {
        transform.position = crosshair.position;
    }
}
