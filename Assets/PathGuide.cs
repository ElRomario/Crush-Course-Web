using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class PathGuide : MonoBehaviour
{

    public Transform player; // ������ �� ������
    public Transform target; // ������ �� ����
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // ���������, ������ �� ��� ������
        if (PlayerPrefs.GetInt("FirstLaunch", 1) == 1)
        {
            lineRenderer.enabled = true;
            PlayerPrefs.SetInt("FirstLaunch", 0); // ��������, ��� ������ ������ ���
            PlayerPrefs.Save(); // ��������� ���������
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (lineRenderer.enabled)
        {
            // ������ ����� �� ������ �� ����
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
        }
    }

}
