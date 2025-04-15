using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelector : MonoBehaviour
{


    public string ModelName; // ����� � ���������� ��� ������� (��� ".prefab")
    public int ModelPrice;
    MoneyAndPlaneDisplay moneyAndPlaneDisplay;

    private HashSet<string> purchasedPlanes = new HashSet<string>();

    private void Start()
    {
        moneyAndPlaneDisplay = FindObjectOfType<MoneyAndPlaneDisplay>();

        // ��������� ��������� �������
        LoadPurchasedPlanes();

        // ���� ���� ������ ��� ������, ����� ������� ��� �����
        if (purchasedPlanes.Contains(ModelName))
        {
            Debug.Log($"{ModelName} ��� ������");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (purchasedPlanes.Contains(ModelName))
            {
                // ���� ������ ��� ������ � ������ �������� ���
                Select();
            }
            else
            {
                // ���� ��� �� ������ � �������� ������
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
            purchasedPlanes.Add(ModelName); // ��������� � ������ ���������
            SavePurchasedPlanes();

            Select();
            Debug.Log($"{ModelName} ������");

            moneyAndPlaneDisplay.UpdateUI();
            GameManager.Instance.SaveGameData();
        }
    }

    private void Select()
    {
        GameManager.Instance.SelectModel(ModelName);
        Debug.Log($"{ModelName} ������");
        GameManager.Instance.SaveGameData();
        moneyAndPlaneDisplay.UpdateUI();
    }

    private void LoadPurchasedPlanes()
    {
        // ��������� ������ ��������� �������� �� PlayerPrefs
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
        // ��������� ������ ��������� �������� � PlayerPrefs
        string savedData = string.Join(",", purchasedPlanes);
        PlayerPrefs.SetString("PurchasedPlanes", savedData);
        PlayerPrefs.Save();
    }
}
