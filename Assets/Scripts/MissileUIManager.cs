using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileUIManager : MonoBehaviour
{

   
        public static MissileUIManager Instance { get; private set; }
        public Text missileText;

        private int missileCount;
        public int MaxMissiles;
        public float ReloadTime = 2f; // Время между добавлением одной ракеты

        private Coroutine reloadCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {  
            MaxMissiles = CheckForMissilesCount();
            MissileCount = MaxMissiles;
            UpdateMissileUI();
        }

    private int CheckForMissilesCount()
    {
        int count;
        if (GameManager.Instance.SelectedModelName == "СУ-25 Грач")
            count = 2;
        else
        {
            count = 4;
        }

        return count;
    }

    public int MissileCount
        {
            get { return missileCount; }
            set
            {
                missileCount = Mathf.Clamp(value, 0, MaxMissiles);
                UpdateMissileUI();

                // Если ракеты уменьшились, запускаем перезарядку (если она ещё не идёт)
                if (reloadCoroutine == null && missileCount < MaxMissiles)
                {
                    reloadCoroutine = StartCoroutine(ReloadMissiles());
                }
            }
        }

        private IEnumerator ReloadMissiles()
        {
            while (missileCount < MaxMissiles)
            {
                yield return new WaitForSeconds(ReloadTime);
                missileCount++;
                UpdateMissileUI();
            }
            reloadCoroutine = null; // Завершаем перезарядку
        }

        private void UpdateMissileUI()
        {
            if (missileText != null)
            {
                missileText.text = missileCount.ToString();
            }
        }
    }
