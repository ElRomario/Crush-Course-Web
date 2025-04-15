using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WarningTextController : MonoBehaviour
    {
    public TMP_Text warningText; // ������ �� ����� "ENEMY MISSILE INCOMING".
    public float blinkInterval = 0.5f; // �������� �������.

        private bool isBlinking = false;

        // ���������� ��� ������� ������.
        public void StartBlinking()
        {
            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine(BlinkText());
            }
        }

        // ����������, ����� StartChaosMovement �����������.
        public void StopBlinking()
        {
            isBlinking = false;
            warningText.enabled = false; // ������ �����.
        }

        private IEnumerator BlinkText()
        {
            while (isBlinking)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.blip);
                warningText.enabled = !warningText.enabled; // ����������� ���������.
                yield return new WaitForSeconds(blinkInterval); // ��� ��������� ��������.
            }
            warningText.enabled = true; // ��������, ��� ����� ����� ��� ���������.
        }
    }

