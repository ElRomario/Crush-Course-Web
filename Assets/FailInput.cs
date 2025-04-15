using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class FailInput : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
    }
    public void LoadHub()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene("Hub");
    }
    public void ReloadLevel()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
