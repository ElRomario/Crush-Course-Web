using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarningTextController : MonoBehaviour
    {
    public TMP_Text warningText; // Ссылка на текст "ENEMY MISSILE INCOMING".
    public float blinkInterval = 0.5f; // Интервал мигания.

        private bool isBlinking = false;

        // Вызывается при запуске ракеты.
        public void StartBlinking()
        {
            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine(BlinkText());
            }
        }

        // Вызывается, когда StartChaosMovement запускается.
        public void StopBlinking()
        {
            isBlinking = false;
            warningText.enabled = false; // Прячем текст.
        }

        private IEnumerator BlinkText()
        {
            while (isBlinking)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.blip);
                warningText.enabled = !warningText.enabled; // Переключаем видимость.
                yield return new WaitForSeconds(blinkInterval); // Ждём указанный интервал.
            }
            warningText.enabled = true; // Убедимся, что текст виден при остановке.
        }
    }

