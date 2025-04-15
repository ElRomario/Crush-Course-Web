using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Синглтон
    public Health health;
    public Canvas uiCanvas; // Ссылка на Canvas
    public Image deathImage;
    public Canvas uiDeath;
    public SceneReloader sceneReloader;


    private void Start()
    {
        health.OnDeath += HandleDeath;
    }
    private void Awake()
    {
        
        // Проверяем, есть ли уже экземпляр
        if (Instance == null)
        {
            Instance = this; // Устанавливаем текущий экземпляр
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующий объект
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
