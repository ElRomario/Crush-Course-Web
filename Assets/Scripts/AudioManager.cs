using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Clip")]
    public AudioClip background;

    [Header("SFX Clips")]
    public AudioClip missileLaunch;
    public AudioClip laserShoot;
    public AudioClip roll;
    public AudioClip blip;
    public AudioClip missileDropDown;
    public AudioClip enemySpawn;
    public AudioClip jump;
    public AudioClip buy;
    public AudioClip fly;
    public AudioClip win;

    [Header("Explosion Variants")]
    public AudioClip[] explosions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Установка громкости из PlayerPrefs
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;
    }

    private void Start()
    {
        if (background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic()
    {
        if (background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayExplosions()
    {
        if (explosions.Length > 0)
        {
            sfxSource.PlayOneShot(explosions[Random.Range(0, explosions.Length)]);
        }
    }





}
