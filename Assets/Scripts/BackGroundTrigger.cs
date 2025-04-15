using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class TriggeredBackgroundColorChanger : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject[] canvasElements; // Элементы Canvas
    public float delayBetweenActivations = 1.0f; // Задержка между активациями
    public bool loop = false;
    public GameObject thanks;// Повторять цикл?

    [SerializeField] private Camera mainCamera; // Камера для изменения цвета фона
    [SerializeField] private Color[] colors; // Массив цветов, редактируемый в Inspector
    [SerializeField] private float transitionDuration = 2f; // Длительность смены цвета
    [SerializeField] private string targetTag = "Player";
    public Canvas uiCanvas;
    

    private int currentColorIndex = 0;
    private bool isChanging = false;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !isChanging && colors.Length > 0)
        {
            uiCanvas.gameObject.SetActive(false);
            StartCoroutine(ActivateElements());
            StartCoroutine(ChangeBackgroundColor());
            thanks.gameObject.SetActive(true);
            
        }
    }

    private IEnumerator ChangeBackgroundColor()
    {
        isChanging = true;

        while (true) // Цикл смены цветов
        {
            Color startColor = mainCamera.backgroundColor;
            Color targetColor = colors[currentColorIndex];

            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            mainCamera.backgroundColor = targetColor;
            yield return new WaitForSeconds(1f);

            currentColorIndex = (currentColorIndex + 1) % colors.Length;
        }
    }


    private IEnumerator ActivateElements()
    {
        do
        {
            foreach (GameObject element in canvasElements)
            {
                if (element != null)
                {
                    element.SetActive(true);
                    yield return new WaitForSeconds(delayBetweenActivations);
                }
            }

            if (!loop) break;

        } while (loop);
    }


}
