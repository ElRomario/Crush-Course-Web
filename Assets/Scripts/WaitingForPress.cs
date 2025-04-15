using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingForPress : MonoBehaviour
{

    public Image pressedImage;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            pressedImage.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.laserShoot);
        }
    }
}
