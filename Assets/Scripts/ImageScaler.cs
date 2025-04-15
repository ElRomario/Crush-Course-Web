using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ImageScaler : MonoBehaviour
{

    public float delay = 2f;      // �������� ����� ������� ��������
    public float speed = 10f;     // �������� ����������
    private bool hasStarted = false;
    public bool isAnimating = false;

    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(WaitAndStartScaling()); // ������ �������� ��������
    }

    void Update()
    {
        if (hasStarted)
        {
            if (transform.localScale.x < 30)
            {
                isAnimating = true;

                // ����������� ������, �� �� ��������� 30
                float newScale = Mathf.Min(transform.localScale.x + (3f * Time.deltaTime * speed), 30f);
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
            else
            {
                isAnimating = false; // �������� ���������
                hasStarted = false;  // ������������� ���������� ����������
            }
        }
    }

    IEnumerator WaitAndStartScaling()
    {
        yield return new WaitForSeconds(delay); // ��� ����� ������� ��������
        hasStarted = true;  // ��������� ��������
    }
}
