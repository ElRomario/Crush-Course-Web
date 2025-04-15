using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isPaused = false;
    private GameObject uimanager;
    private GameObject player;
    private CrossHairController crossHairController;
    
    void Start()
    {
        // Скрываем меню при запуске
        pausePanel.SetActive(false);

        // Устанавливаем значения с сохранённых
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);


       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {

       
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        uimanager = GameObject.FindGameObjectWithTag("UIManager");
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.GetComponent<PlayerMissileShooter>().enabled = false;
            player.GetComponent<ProjectileShooter>().enabled = false;
        }

        Transform uiManager = GameObject.Find("UIManager").transform;

        Transform crossHair = uiManager.Find("UI/CrossHair");
        crossHairController = crossHair.GetComponent<CrossHairController>();
        if (crossHairController != null)
        {
            crossHairController.stopAiming();
        }
        else
        {
            Debug.Log("WTF UO DOING");
        }

        if (uimanager != null)
        {
            uimanager.SetActive(false);
        }
        


    }

   public void Resume()
    {
        
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
      
        if (player != null)
        {
            player.GetComponent<PlayerMissileShooter>().enabled = true;
            player.GetComponent<ProjectileShooter>().enabled = true;
        }
        if (crossHairController != null)
        {
            crossHairController.startAiming();
        }
        else
        {
            Debug.Log("WTF UO DOING");
        }

        if (uimanager != null)
        {
            uimanager.SetActive(true);
        }
        
    }

    void OnMusicChanged(float value)
    {
        Debug.Log("Music slider value: " + value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }

    void OnSFXChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
    }
}
