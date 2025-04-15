using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ������ ���
        {
            Cursor.lockState = CursorLockMode.Locked; // ��������� ������
            Cursor.visible = false; // ������ ���������
        }
        else if (Input.GetMouseButtonUp(1)) // ��������� ���
        {
            Cursor.lockState = CursorLockMode.None; // ������������
            Cursor.visible = true; // ������ �������
        }
    }
}
