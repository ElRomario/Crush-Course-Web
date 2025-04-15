using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Зажали ПКМ
        {
            Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор
            Cursor.visible = false; // Делаем невидимым
        }
        else if (Input.GetMouseButtonUp(1)) // Отпустили ПКМ
        {
            Cursor.lockState = CursorLockMode.None; // Разблокируем
            Cursor.visible = true; // Делаем видимым
        }
    }
}
