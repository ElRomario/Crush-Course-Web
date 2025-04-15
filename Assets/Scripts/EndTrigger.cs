using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] int revenue;
    SceneLoaderLeg sceneloader;
    FadeOut fadeOut;
    public int levelToComplete;
    // Update is called once per frame
    private void Start()
    {
        //sceneloader = GetComponent<SceneLoaderLeg>();   
        fadeOut = GetComponent<FadeOut>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<FadeOut>().StartFadeOut();
            GameManager.Instance.revenue = revenue;
            GameManager.Instance.SetMoney(GameManager.Instance.GetMoney() + revenue);
            GameManager.Instance.SaveGameData();
            PlayerPrefs.SetInt($"Level{levelToComplete}Completed", 1);
            int currentMax = PlayerPrefs.GetInt("MaxLevelReached", 0);
            if (levelToComplete > currentMax)
            {
                PlayerPrefs.SetInt("MaxLevelReached", levelToComplete);

            }
            
            PlayerPrefs.Save();
            GameManager.Instance.SetMaxLevelReached(levelToComplete);
        }

       
    }
}
