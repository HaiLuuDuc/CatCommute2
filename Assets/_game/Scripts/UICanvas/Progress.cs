using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Progress : UICanvas
{
    public TextMeshProUGUI goldText;


    public override void Open()
    {
        base.Open();
    }

    public void Btn_Continue()
    {
        LevelManager.ins.LoadNextLevel();
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
    }
}
