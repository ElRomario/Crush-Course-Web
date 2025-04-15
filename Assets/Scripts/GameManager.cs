using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    SkinSelector skinSelector;
    public int PlayerMoney = 0;
    public GameObject SelectedPlane;
    public string SelectedModelName;
    public string SelectedSkinName;
    public int revenue;

    private const string MoneyKey = "PlayerMoney";
    private const string PlaneKey = "SelectedPlane";
    private const string SkinKey = "SelectedSkin";

    [SerializeField] private int maxLevelReached;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SelectModel("СУ-25 Грач");
        //SelectSkin("starter");
        skinSelector = FindObjectOfType<SkinSelector>();
        skinSelector.LoadSkinOnStart();
        LoadGameData();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    public void SetMaxLevelReached(int level)
    {
        if (level > maxLevelReached) // Только если новый уровень выше текущего
        {
            maxLevelReached = level;
            PlayerPrefs.SetInt("MaxLevelReached", level);
            PlayerPrefs.Save();
        }
    }

    public string GetSelectedSkin()
    {
        return SelectedSkinName;
    }

    public int GetMoney()
    {
        return PlayerMoney;
    }

    public void SetMoney(int money)
    {
        PlayerMoney = money;
        SaveGameData();
    }

    public void SelectSkin(string _skinName)
    {
        SelectedSkinName = _skinName;
    }

    public void SelectModel(string modelName)
    {
        SelectedModelName = modelName;
    }

    // Метод для получения префаба
    public GameObject GetSelectedModelPrefab()
    {
        if (!string.IsNullOrEmpty(SelectedModelName))
        {
            return Resources.Load<GameObject>(SelectedModelName);
        }
        return null;
    }

    private void Start()
    {

        LoadGameData();
        maxLevelReached = PlayerPrefs.GetInt("MaxLevelReached", 1);

    }



    private void OnApplicationQuit()
    {
        SaveGameData(); // Сохраняем данные при закрытии приложения
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GameManager] Загружена сцена: {scene.name}");
        LogGameState();
        // Сохраняем данные при загрузке новой сцены
        SaveGameData();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt(MoneyKey, PlayerMoney);
        PlayerPrefs.SetString(PlaneKey, SelectedModelName != null ? SelectedModelName : "");
        PlayerPrefs.SetString(SkinKey, SelectedSkinName != null ? SelectedSkinName : "");
        PlayerPrefs.Save(); // Обязательно вызываем Save() чтобы данные точно сохранились
    }

    private void LogGameState()
    {
        Debug.Log("===== GameManager State =====");
        Debug.Log($" PlayerMoney: {PlayerMoney}");
        Debug.Log($" SelectedModelName: {SelectedModelName}");
        Debug.Log($" SelectedSkinName: {SelectedSkinName}");
        Debug.Log($" MaxLevelReached: {maxLevelReached}");
        Debug.Log($" Revenue (за сессию): {revenue}");
        Debug.Log("=============================");
    }

    private void LoadGameData()
    {
        // Если данных нет, загружаем стартовые настройки
        if (!PlayerPrefs.HasKey(PlaneKey) || !PlayerPrefs.HasKey(SkinKey))
        {
            LoadDefault();
            return;
        }

        // Загружаем деньги
        PlayerMoney = PlayerPrefs.GetInt(MoneyKey, 0);

        // Загружаем модель самолёта и скин персонажа
        SelectedModelName = PlayerPrefs.GetString(PlaneKey, "");
        SelectedSkinName = PlayerPrefs.GetString(SkinKey, "");

        // Загружаем объект самолёта из ресурсов
        if (!string.IsNullOrEmpty(SelectedModelName))
        {
            GameObject planePrefab = Resources.Load<GameObject>(SelectedModelName);
            if (planePrefab != null)
            {
                //SelectedPlane = Instantiate(planePrefab);
            }
            else
            {
                Debug.LogWarning($"Не удалось загрузить самолёт: {SelectedModelName}");
            }
        }

        // Передаём загруженное имя скина в SkinSelector (если он есть)
        if (skinSelector != null)
        {
            skinSelector.LoadSkinOnStart();
        }
    }
    public void ResetRevenue()
    {
        revenue = 0;
    }

    public void LoadDefault()
    {
        // Устанавливаем стартовые значения
        SelectedModelName = "СУ-25 Грач"; // Замени на фактическое имя стартового самолёта в ресурсах
        SelectedSkinName = "starter"; // Замени на фактическое имя стартового скина персонажа

        // Загружаем стартовый самолёт из ресурсов
        GameObject planePrefab = Resources.Load<GameObject>(SelectedModelName);
        if (planePrefab != null)
        {
            if (SelectedPlane != null)
            {
                //Destroy(SelectedPlane); // Удаляем старый самолёт, если он есть
            }
            SelectedPlane = planePrefab;
        }
        else
        {
            Debug.LogWarning($"Не удалось загрузить стартовый самолёт: {SelectedModelName}");
        }

        // Применяем стартовый скин через SkinSelector
        if (skinSelector != null)
        {
            skinSelector.LoadSkinOnStart();
        }

        // Сохраняем стартовые данные
        SaveGameData();
    }

}