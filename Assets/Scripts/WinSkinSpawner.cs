using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSpawner : MonoBehaviour
{

    public Transform spawnPoint; // ����� ������
    private GameObject currentSkin;
    [SerializeField] string skinController;
    void Start()
    {
        SpawnSkin();
        Debug.Log("=============DEBUG==============");
        Debug.Log(GameManager.Instance.GetSelectedModelPrefab());
        Debug.Log(GameManager.Instance.GetSelectedSkin());
        Debug.Log(GameManager.Instance.SelectedModelName);
    }

    void SpawnSkin()
    {
        if (GameManager.Instance == null || string.IsNullOrEmpty(GameManager.Instance.SelectedSkinName))
        {
            Debug.LogError("GameManager ��� ��������� ���� �� ����������!");
            return;
        }

        GameObject skinPrefab = Resources.Load<GameObject>(GameManager.Instance.SelectedSkinName);
        if (skinPrefab == null)
        {
            Debug.LogError("���� �� ������ � Resources: " + GameManager.Instance.SelectedSkinName);
            return;
        }

        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }

        currentSkin = Instantiate(skinPrefab, spawnPoint.position, spawnPoint.rotation);
        SetAnimatorController(currentSkin);
    }

    void SetAnimatorController(GameObject skin)
    {
        Animator animator = skin.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("� ����� ��� ���������� Animator!");
            return;
        }

        RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(skinController);
        if (controller == null)
        {
            Debug.LogError("������������ ���������� 'WinDance' �� ������!");
            return;
        }

        animator.runtimeAnimatorController = controller;
    }
}

