using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class TutorialStart : MonoBehaviour
{
    [SerializeField] GameObject cutScene;
    [SerializeField] string levelToStart;
    private Rigidbody rigidbody;
    public int requiredLevel;
    public TextMeshProUGUI stopSign;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            // Проверяем, что максимальный уровень выше или равен требуемому уровню
            int maxLevelReached = PlayerPrefs.GetInt("MaxLevelReached", 0);

            if (requiredLevel == 0 || maxLevelReached >= requiredLevel)
            {
                Debug.Log($"Доступ к уровню {levelToStart} открыт!");

                // Отключаем гравитацию у игрока
                Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.useGravity = false;
                }

                // Отключаем коллизии между игроком и окружением
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerLayer"), LayerMask.NameToLayer("EnvironmentLayer"), true);

                // Запускаем кат-сцену
                if (cutScene != null && cutScene.GetComponent<CutScene>() != null)
                {
                    CutScene cutscene = cutScene.GetComponent<CutScene>();
                    cutscene.Initialize(levelToStart);
                    cutScene.SetActive(true);
                }
            }
            else
            {
                StartCoroutine(ShowTextForSeconds(4f));
                Debug.Log("Сначала пройди предыдущий уровень!");
            }
            
        }
    }

    IEnumerator ShowTextForSeconds(float duration)
    {
        stopSign.gameObject.SetActive(true); // Включаем элемент
        yield return new WaitForSeconds(duration); // Ждём заданное время
        stopSign.gameObject.SetActive(false);
    }
}
