using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoneyAndPlaneDisplay : MonoBehaviour
{

    public GameManager gameManager;
    public Text moneyText;
    public Text planeText;

    private void Start()
    {
        gameManager = GameManager.Instance;

        // ��������� UI ����� ��� ������
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (gameManager != null)
        {
            moneyText.text = gameManager.GetMoney().ToString();
            planeText.text = "����˨�: " + gameManager.SelectedModelName;
        }
    }
}
