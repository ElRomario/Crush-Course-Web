using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicSetter : MonoBehaviour
{
    [Header("Музыка для этой сцены")]
    public AudioClip sceneMusic;

    void Start()
    {
        if (AudioManager.Instance != null && sceneMusic != null)
        {
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.musicSource.clip = sceneMusic;
            AudioManager.Instance.musicSource.loop = true;
            AudioManager.Instance.musicSource.Play();
        }
    }
}
