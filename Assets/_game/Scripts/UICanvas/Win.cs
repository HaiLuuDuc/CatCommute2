using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public override void Open()
    {
        base.Open();
        RewardEndLevelManager.ins.RewardWin();
    }

    public void Btn_NextLevel()
    {
        LevelManager.ins.LoadNextLevel();
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Ready>();
    }
}
