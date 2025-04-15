using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    public Transform ModelContainer; // Контейнер для модели

    private void Start()
    {
        GameObject selectedModelPrefab = GameManager.Instance.GetSelectedModelPrefab();

        if (selectedModelPrefab != null)
        {
            Vector3 oldPosition = Vector3.zero;
            Quaternion oldRotation = Quaternion.identity;
            Vector3 oldScale = Vector3.one;

            // Проверяем, есть ли старая модель
            if (ModelContainer.childCount > 0)
            {
                Transform oldModel = ModelContainer.GetChild(0);

                oldPosition = oldModel.localPosition;
                oldRotation = oldModel.localRotation;
                oldScale = oldModel.localScale;

                // Удаляем старую модель
                Destroy(oldModel.gameObject);
            }

            // Создаём новую модель
            GameObject newModel = Instantiate(selectedModelPrefab, ModelContainer);

            // Устанавливаем параметры старой модели
            newModel.transform.localPosition = oldPosition;
            newModel.transform.localRotation = oldRotation;
            newModel.transform.localScale = oldScale;
        }
        else
        {
            Debug.LogWarning("Модель не выбрана! Оставляем оригинальную.");
        }
    }
}
