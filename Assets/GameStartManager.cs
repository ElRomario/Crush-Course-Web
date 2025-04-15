using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameStartManager : MonoBehaviour
{
    public GameObject controlWindow;
    public GameObject gameUI;

    private bool gameStarted = false;

    private void Start()
    {
        Time.timeScale = 0;
        controlWindow.SetActive(true);
        gameUI.SetActive(false);

        // явное включение Canvas после загрузки
        Canvas canvas = controlWindow.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
            canvas.enabled = true;
        }
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            Invoke(nameof(EnableGame), 0.1f);
        }
    }

    public void EnableGame()
    {
        controlWindow.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }
}

