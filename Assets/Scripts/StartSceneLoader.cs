using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{


    public ImageScaler imageScaler; // ������ �� ImageScaler
    public Image image;
    public string sceneToLoad;// UI-�����������, ������� �����������

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) // ��������� ������� ������� Enter
        {
            // �������� ������ � ������������
            image.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.roll);
            // ����������, ��� imageScaler ������� � ���������� ���������
            imageScaler.enabled = true;
            imageScaler.isAnimating = true;

            // ��������� �������� ��� �������� ���������� ��������
            StartCoroutine(WaitForImageScalerAndLoadScene());
        }
    }

    IEnumerator WaitForImageScalerAndLoadScene()
    {
        // ����, ���� imageScaler ��������� �����������
        while (imageScaler.isAnimating)
        {
            yield return null; // �������� �� ������ ����
        }

        // ��������� ����� ����� ���������� ��������
        SceneManager.LoadScene(sceneToLoad);
    }
}
