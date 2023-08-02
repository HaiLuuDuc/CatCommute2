using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Home : UICanvas
{
    public TextMeshProUGUI currentCoinText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinToUpgradeText;

    public static Home ins;
    private void Awake()
    {
        ins = this;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.U))
        {
            Btn_Upgrade();
        }
    }

    public override void Open()
    {
        base.Open();
        UpdateLevelText();
        UpdateCoinToUpgradeText();
        UpdateCurrentCoinText();
    }

    public void Btn_Outfit()
    {
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Outfit>();
    }

    public void Btn_Upgrade()
    {
        UpgradeManager.ins.DecreaseCoin();
        UpgradeManager.ins.DecreaseRemainUpgradeCount();
        //UpgradeManager.ins.SetNewLevelBaseOnRemainUpgradeCount();

        Player.ins.currentScore = Player.ins.targetScore += 100;

        UpdateCoinToUpgradeText();
        UpdateCurrentCoinText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + (LevelManager.ins.currentLevelIndex + 1).ToString();
    }

    public void UpdateCoinToUpgradeText()
    {
        coinToUpgradeText.text = DataManager.ins.playerData.coinToUpgrade.ToString();
    }

    public void UpdateCurrentCoinText()
    {
        currentCoinText.text = DataManager.ins.playerData.coin.ToString();
    }
}
