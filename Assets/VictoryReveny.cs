using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryReveny : MonoBehaviour
{
    public TextMeshProUGUI revenueText; // Перетащи сюда UI-текст

    void Start()
    {
        revenueText.text = "Вы заработали: " + GameManager.Instance.revenue + " монет";
        GameManager.Instance.ResetRevenue(); // Обнуляем после показа
        
    }
}
