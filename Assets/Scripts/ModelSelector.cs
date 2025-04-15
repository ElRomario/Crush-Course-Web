using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelector : MonoBehaviour
{


    public string ModelName; // Укажи в инспекторе имя префаба (без ".prefab")
    public int ModelPrice;
    MoneyAndPlaneDisplay moneyAndPlaneDisplay;

    private HashSet<string> purchasedPlanes = new HashSet<string>();

    private void Start()
    {
        moneyAndPlaneDisplay = FindObjectOfType<MoneyAndPlaneDisplay>();

        // Загружаем купленные самолёты
        LoadPurchasedPlanes();

        // Если этот самолёт уже куплен, можно выбрать его сразу
        if (purchasedPlanes.Contains(ModelName))
        {
            Debug.Log($"{ModelName} уже куплен");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (purchasedPlanes.Contains(ModelName))
            {
                // Если самолёт уже куплен — просто выбираем его
                Select();
            }
            else
            {
                // Если ещё не куплен — пытаемся купить
                Purchase();
            }
        }
    }

    private void Purchase()
    {

        if (ModelPrice > GameManager.Instance.GetMoney())
        {
            Debug.Log("Too Expensive");
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buy);
            GameManager.Instance.SetMoney(GameManager.Instance.GetMoney() - ModelPrice);
            purchasedPlanes.Add(ModelName); // Добавляем в список купленных
            SavePurchasedPlanes();

            Select();
            Debug.Log($"{ModelName} куплен");

            moneyAndPlaneDisplay.UpdateUI();
            GameManager.Instance.SaveGameData();
        }
    }

    private void Select()
    {
        GameManager.Instance.SelectModel(ModelName);
        Debug.Log($"{ModelName} выбран");
        GameManager.Instance.SaveGameData();
        moneyAndPlaneDisplay.UpdateUI();
    }

    private void LoadPurchasedPlanes()
    {
        // Загружаем список купленных самолётов из PlayerPrefs
        string savedData = PlayerPrefs.GetString("PurchasedPlanes", "");
        if (!string.IsNullOrEmpty(savedData))
        {
            string[] planes = savedData.Split(',');
            foreach (string plane in planes)
            {
                if (!string.IsNullOrEmpty(plane))
                {
                    purchasedPlanes.Add(plane);
                }
            }
        }
    }

    private void SavePurchasedPlanes()
    {
        // Сохраняем список купленных самолётов в PlayerPrefs
        string savedData = string.Join(",", purchasedPlanes);
        PlayerPrefs.SetString("PurchasedPlanes", savedData);
        PlayerPrefs.Save();
    }
}
