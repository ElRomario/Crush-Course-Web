using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTiltController : MonoBehaviour
{

    public RectTransform indicator; // Ссылка на индикатор
    public RectTransform scale;     // Ссылка на шкалу
    public RailPlaneController planeController;
    public RectTransform crosshair;// Ссылка на контроллер самолёта

    public float maxTilt = 1f; // Максимальное значение наклона
    public float smoothSpeed = 5f; // Скорость плавного движения индикатора

    private float targetPositionY;

    void Update()
    { // Обновляем позицию шкалы так, чтобы она следовала за прицелом
        scale.position = crosshair.position;

        // Получаем ввод из RailPlaneController
        float tiltInput = planeController.GetVerticalInput();

        // Вычисляем позицию индикатора на шкале
        float halfHeight = scale.rect.height / 2; // Половина высоты шкалы
        targetPositionY = Mathf.Clamp(tiltInput * halfHeight / maxTilt, -halfHeight, halfHeight);

        // Плавно перемещаем индикатор к целевой позиции
        Vector3 currentPos = indicator.localPosition;
        currentPos.y = Mathf.Lerp(currentPos.y, targetPositionY, Time.deltaTime * smoothSpeed);
        indicator.localPosition = currentPos;
    }
}
