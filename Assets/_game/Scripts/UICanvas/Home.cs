using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Home : UICanvas
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinToUpgradeText;
    public TextMeshProUGUI currentCoinText;

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

    private void UpdateLevelText()
    {
        levelText.text = "Lv " + DataManager.ins.playerData.characterLevel.ToString();
    }

    public void Btn_Upgrade()
    {
        UpgradeManager.ins.DecreaseCoin();
        UpgradeManager.ins.DecreaseRemainUpgradeCount();
        UpgradeManager.ins.SetNewLevelBaseOnRemainUpgradeCount();

        UpdateCoinToUpgradeText();
        UpdateCurrentCoinText();
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
