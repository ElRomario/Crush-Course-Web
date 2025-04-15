using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    public GameObject uiPrefab; // Префаб для Image
    public float visibilityDistance = 50f; // Дистанция видимости (настраиваемая в инспекторе)

    private GameObject uiElement; // Ссылка на созданный элемент UI
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Получаем Canvas через синглтон
        Canvas uiCanvas = UIManager.Instance?.uiCanvas;
        if (uiCanvas != null && uiPrefab != null)
        {
            uiElement = Instantiate(uiPrefab, uiCanvas.transform);
            uiElement.SetActive(false); // Скрываем до появления врага
        }
    }

    void Update()
    {
        if (uiElement != null)
        {
            // Конвертируем мировые координаты в экранные
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

            // Проверяем, виден ли враг и находится ли в пределах дистанции
            float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
            if (screenPosition.z > 0 && distanceToCamera <= visibilityDistance)
            {
                uiElement.SetActive(true);
                uiElement.GetComponent<RectTransform>().position = screenPosition;
            }
            else
            {
                uiElement.SetActive(false);
            }
        }
        else
        {
            Debug.Log("uiELelemt is null!");
        }
    }

    void OnDestroy()
    {
        // Удаляем UI-элемент при уничтожении врага
        if (uiElement != null)
        {
            Destroy(uiElement);
        }
    }
}