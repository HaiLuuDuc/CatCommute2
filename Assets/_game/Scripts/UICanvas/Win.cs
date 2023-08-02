using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Win : UICanvas
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI coinEarnedText;

    public override void Open()
    {
        base.Open();
        RewardEndLevelManager.ins.RewardWin();
        coinEarnedText.text = "+" + RewardEndLevelManager.ins.rewardCoin.ToString();
        StartCoroutine(Utils.LerpIntCoroutine(
            DataManager.ins.playerData.coin, 
            DataManager.ins.playerData.coin + (int)RewardEndLevelManager.ins.rewardCoin,
            1f, (value) => {
                goldText.text = ((int)value).ToString();
            }));
        DataManager.ins.playerData.coin += (int)RewardEndLevelManager.ins.rewardCoin;
    }

    public void Btn_Nothanks()
    {
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Progress>();
    }
}
