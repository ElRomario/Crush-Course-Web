using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMission : MonoBehaviour
{
    MoneyAndPlaneDisplay moneyAndPlaneDisplay;


    private void Start()
    {
        moneyAndPlaneDisplay = FindObjectOfType<MoneyAndPlaneDisplay>();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    SceneManager.LoadScene("Hub");
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    GameManager.Instance.SetMoney(100);
        //    moneyAndPlaneDisplay.UpdateUI();
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    PlayerPrefs.DeleteAll();
        //    moneyAndPlaneDisplay.UpdateUI();
        //    GameManager.Instance.LoadDefault();
        //}
    }
}
