using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoaderLeg : MonoBehaviour
{


    [SerializeField] private string _sceneName;

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty!");
        }
    }
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(_sceneName))
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty!");
        }
    }
}
