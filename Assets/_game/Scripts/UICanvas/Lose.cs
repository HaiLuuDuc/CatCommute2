using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lose : UICanvas
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI coinEarnedText;


    public override void Open()
    {
        base.Open();
        RewardEndLevelManager.ins.RewardLose();
        coinEarnedText.text = "+" + RewardEndLevelManager.ins.rewardCoin.ToString();
        StartCoroutine(Utils.LerpIntCoroutine(
            DataManager.ins.playerData.coin,
            DataManager.ins.playerData.coin + (int)RewardEndLevelManager.ins.rewardCoin,
            1f, (value) => {
                goldText.text = ((int)value).ToString();
            }));
        DataManager.ins.playerData.coin += (int)RewardEndLevelManager.ins.rewardCoin;
    }

    public void Btn_Replay()
    {
        LevelManager.ins.LoadLevel(DataManager.ins.playerData.currentLevelIndex, false);
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
    }
}
