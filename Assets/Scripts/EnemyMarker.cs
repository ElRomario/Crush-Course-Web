using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyMarker : MonoBehaviour
    {
    public GameObject markerPrefab; // Префаб маркера
    private GameObject markerInstance;
    private RectTransform canvasRect; // RectTransform Canvas'а

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();

            // Создаем маркер и добавляем его в Canvas
            if (markerPrefab != null)
            {
                markerInstance = Instantiate(markerPrefab, canvas.transform);
            }
        }
    }

    void Update()
    {
        if (markerInstance != null)
        {
            // Преобразуем мировые координаты врага в экранные
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            // Если враг в поле зрения
            if (screenPos.z > 0)
            {
                markerInstance.SetActive(true);

                // Преобразуем экранные координаты в координаты UI (Canvas)
                Vector2 uiPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    screenPos,
                    null, // Камера не нужна для Screen Space - Overlay
                    out uiPosition
                );

                // Обновляем позицию маркера
                markerInstance.GetComponent<RectTransform>().anchoredPosition = uiPosition;
            }
            else
            {
                // Скрываем маркер, если враг вне камеры
                markerInstance.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // Удаляем маркер при уничтожении врага
        if (markerInstance != null)
        {
            Destroy(markerInstance);
        }
    }
}

