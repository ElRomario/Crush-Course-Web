using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryReveny : MonoBehaviour
{
    public TextMeshProUGUI revenueText; // �������� ���� UI-�����

    void Start()
    {
        revenueText.text = "�� ����������: " + GameManager.Instance.revenue + " �����";
        GameManager.Instance.ResetRevenue(); // �������� ����� ������
        
    }
}
