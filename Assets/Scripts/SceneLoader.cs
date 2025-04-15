using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public Canvas uiDeath;
    public Canvas uiGame;
    public Image deathImage;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Нажатие Enter
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            uiDeath.gameObject.SetActive(false);
            uiGame.gameObject.SetActive(true) ;

            uiGame.GetComponent<Canvas>().enabled = true;
            uiDeath.GetComponent<Canvas>().enabled = false;
            deathImage.GetComponent<ImageScaler>().enabled = false;
            deathImage.transform.localScale = Vector3.zero;
            
        }
    }
}
