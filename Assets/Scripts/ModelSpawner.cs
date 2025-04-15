using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    public Transform ModelContainer; // ��������� ��� ������

    private void Start()
    {
        GameObject selectedModelPrefab = GameManager.Instance.GetSelectedModelPrefab();

        if (selectedModelPrefab != null)
        {
            Vector3 oldPosition = Vector3.zero;
            Quaternion oldRotation = Quaternion.identity;
            Vector3 oldScale = Vector3.one;

            // ���������, ���� �� ������ ������
            if (ModelContainer.childCount > 0)
            {
                Transform oldModel = ModelContainer.GetChild(0);

                oldPosition = oldModel.localPosition;
                oldRotation = oldModel.localRotation;
                oldScale = oldModel.localScale;

                // ������� ������ ������
                Destroy(oldModel.gameObject);
            }

            // ������ ����� ������
            GameObject newModel = Instantiate(selectedModelPrefab, ModelContainer);

            // ������������� ��������� ������ ������
            newModel.transform.localPosition = oldPosition;
            newModel.transform.localRotation = oldRotation;
            newModel.transform.localScale = oldScale;
        }
        else
        {
            Debug.LogWarning("������ �� �������! ��������� ������������.");
        }
    }
}
