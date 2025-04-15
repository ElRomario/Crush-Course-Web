using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public static PlayerSelection Instance;
    public string SelectedModelName; // ������ ������ ��� �������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectModel(string modelName)
    {
        SelectedModelName = modelName;
    }

    // ����� ��� ��������� �������
    public GameObject GetSelectedModelPrefab()
    {
        if (!string.IsNullOrEmpty(SelectedModelName))
        {
            return Resources.Load<GameObject>(SelectedModelName);
        }
        return null;
    }
}
