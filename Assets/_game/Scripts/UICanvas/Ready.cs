using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ready : UICanvas
{
    public TextMeshProUGUI currentCoinText;
    public TextMeshProUGUI coinToUpgradeText;

    public static Ready ins;
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
        UpdateCoinToUpgradeText();
        UpdateCurrentCoinText();
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
