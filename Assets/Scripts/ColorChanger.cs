using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    [Header("Цвета для анимации")]
    public Color color1 = Color.white;  // Первый цвет
    public Color color2 = Color.red;    // Второй цвет
    public Color color3 = Color.blue;   // Финальный цвет перед исчезновением

    [Header("Настройки анимации")]
    public float duration = 2f;         // Время смены цвета
    public float pauseBetweenColors = 1f; // Промежуток между сменами цветов
    public float fadeDuration = 2f;     // Время исчезновения объекта

    private Renderer objRenderer;       // Рендерер объекта
    private Material objMaterial;       // Материал объекта

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            StartCoroutine(AnimateColors());
        }
    }

    IEnumerator AnimateColors()
    {
        yield return ChangeColor(color1, color2, duration);
        yield return new WaitForSeconds(pauseBetweenColors);

        yield return ChangeColor(color2, color3, duration);
        yield return new WaitForSeconds(pauseBetweenColors);

        yield return FadeOut(fadeDuration); // Запускаем исчезновение
        Destroy(transform.parent.gameObject); // Удаляем родительский объект
    }

    IEnumerator ChangeColor(Color fromColor, Color toColor, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            objMaterial.color = Color.Lerp(fromColor, toColor, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objMaterial.color = toColor;
    }

    IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0;
        Color startColor = objMaterial.color;

        while (elapsedTime < time)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / time);
            objMaterial.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objMaterial.color = new Color(startColor.r, startColor.g, startColor.b, 0f); // Гарантируем полную прозрачность
    }
}
