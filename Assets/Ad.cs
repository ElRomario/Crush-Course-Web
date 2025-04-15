using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Ad : MonoBehaviour
{
    public string rewardID = "money";
    public MoneyAndPlaneDisplay moneyAndPlaneDisplay;

    private void OnTriggerEnter(Collider other)
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            GameManager.Instance.SetMoney(GameManager.Instance.GetMoney() + 100);
            GameManager.Instance.SaveGameData();
            moneyAndPlaneDisplay.UpdateUI();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buy);

        });
    }
}
