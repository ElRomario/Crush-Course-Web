using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class SkinSelector : MonoBehaviour
{
    public static SkinSelector Instance;
    public string SelectedSkinName;
    [SerializeField] int SkinPrice;
    MoneyAndPlaneDisplay moneyAndPlaneDisplay;
    private List<string> purchasedSkins = new List<string>();

    private void Awake()
    {
        moneyAndPlaneDisplay = FindObjectOfType<MoneyAndPlaneDisplay>();
        LoadPurchasedSkins(); // «агружаем купленные скины сразу при запуске
    }

    void Start()
    {
        LoadSkinOnStart();
    }

    private void LoadPurchasedSkins()
    {
        if (PlayerPrefs.HasKey("PurchasedSkins"))
        {
            string savedSkins = PlayerPrefs.GetString("PurchasedSkins");
            purchasedSkins = new List<string>(savedSkins.Split(','));
        }
    }

    private void SavePurchasedSkins()
    {
        PlayerPrefs.SetString("PurchasedSkins", string.Join(",", purchasedSkins));
        PlayerPrefs.Save();
    }

    private void Purchase()
    {
        LoadPurchasedSkins(); // «агружаем купленные скины перед проверкой

        if (purchasedSkins.Contains(SelectedSkinName))
        {
            Debug.Log("Skin already purchased! Applying skin.");
            SwapSkin();
        }
        else if (SkinPrice > GameManager.Instance.GetMoney())
        {
            Debug.Log("Too Expensive");
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buy);
            GameManager.Instance.SetMoney(GameManager.Instance.GetMoney() - SkinPrice);
            purchasedSkins.Add(SelectedSkinName);
            SavePurchasedSkins();
            SwapSkin();
            GameManager.Instance.SelectSkin(SelectedSkinName);
            moneyAndPlaneDisplay.UpdateUI();
            GameManager.Instance.SaveGameData();
        }
    }

    public void SelectSkin(string skinName)
    {
        SelectedSkinName = skinName;
    }

    public GameObject GetSelectedSkinPrefab()
    {
        if (!string.IsNullOrEmpty(SelectedSkinName))
        {
            return Resources.Load<GameObject>(SelectedSkinName);
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Purchase();
        }
    }

    private void SwapSkin()
    {
        GameObject newSkin = GetSelectedSkinPrefab();
        if (newSkin == null)
        {
            Debug.LogError("Skin prefab not found: " + SelectedSkinName);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Transform skinParent = player.transform.Find("SkinHolder");
        if (skinParent == null)
        {
            Debug.LogError("SkinHolder not found on Player!");
            return;
        }

        foreach (Transform child in skinParent)
        {
            Destroy(child.gameObject);
        }

        GameObject instantiatedSkin = Instantiate(newSkin, skinParent);
        instantiatedSkin.transform.localPosition = Vector3.zero;
        instantiatedSkin.transform.localRotation = Quaternion.identity;
    }

    public void LoadSkinOnStart()
    {
        string selectedSkin = GameManager.Instance.GetSelectedSkin();
        if (string.IsNullOrEmpty(selectedSkin))
        {
            Debug.LogWarning("No selected skin found in GameManager.");
            return;
        }

        LoadPurchasedSkins(); // «агружаем купленные скины перед установкой

        //if (!purchasedSkins.Contains(selectedSkin))
        //{
        //    Debug.LogWarning("Selected skin is not purchased yet.");
        //    return;
        //}

        GameObject skinPrefab = Resources.Load<GameObject>(selectedSkin);
        if (skinPrefab == null)
        {
            Debug.LogError("Skin prefab not found: " + selectedSkin);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        Transform skinParent = player.transform.Find("SkinHolder");
        if (skinParent == null)
        {
            Debug.LogError("SkinHolder not found on Player!");
            return;
        }

        foreach (Transform child in skinParent)
        {
            Destroy(child.gameObject);
        }

        GameObject instantiatedSkin = Instantiate(skinPrefab, skinParent);
        instantiatedSkin.transform.localPosition = Vector3.zero;
        instantiatedSkin.transform.localRotation = Quaternion.identity;
    }
}

