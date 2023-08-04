using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Progress : UICanvas
{
    public TextMeshProUGUI goldText;

    public Slider slider;
    public TextMeshProUGUI percentageText;

    public TextMeshProUGUI buttonText;

    public bool isRunningProgress = false;


    public override void Open()
    {
        base.Open();
        RunProgress();
        buttonText.text = "Continue";
    }

    public void Btn_Continue()
    {
        if (isRunningProgress) return;
        //claim
        if (DataManager.ins.playerData.progress == 0)
        {
            DataManager.ins.playerData.maxCharacterLevel += 1;
        }
        LevelManager.ins.LoadNextLevel();
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
        
    }

    public void RunProgress()
    {
        int oldProgress = DataManager.ins.playerData.progress;
        int newProgress = DataManager.ins.playerData.progress + 50;
        isRunningProgress = true;
        StartCoroutine(Utils.LerpIntCoroutine(oldProgress, newProgress, 0.6f, value =>
        {
            DataManager.ins.playerData.progress = value;
            percentageText.text = value.ToString() + "%";
            slider.value = (float)DataManager.ins.playerData.progress * 0.01f;
        },
        () => {
            isRunningProgress = false;
            if(DataManager.ins.playerData.progress >= 100)
            {
                Debug.Log("Show reward after progress 100%");
                //DataManager.ins.playerData.currentCharacterLevel += 1;
                //DataManager.ins.playerData.maxCharacterLevel = Mathf.Max(DataManager.ins.playerData.currentCharacterLevel, DataManager.ins.playerData.maxCharacterLevel);
                buttonText.text = "Claim";
                DataManager.ins.playerData.progress = 0;
            }
        }));
    }
}
