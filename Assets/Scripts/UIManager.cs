using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // ��������
    public Health health;
    public Canvas uiCanvas; // ������ �� Canvas
    public Image deathImage;
    public Canvas uiDeath;
    public SceneReloader sceneReloader;


    private void Start()
    {
        health.OnDeath += HandleDeath;
    }
    private void Awake()
    {
        
        // ���������, ���� �� ��� ���������
        if (Instance == null)
        {
            Instance = this; // ������������� ������� ���������
        }
        else
        {
            Destroy(gameObject); // ���������� ����������� ������
            return;
        }

        
    }

    public void HandleDeath()
    {
        uiCanvas.enabled = false;
        uiDeath.GetComponent<Canvas>().enabled = true;
        deathImage.GetComponent<ImageScaler>().enabled = true;
        sceneReloader.enabled = true;
        
    }
}
