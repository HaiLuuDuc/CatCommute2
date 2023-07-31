using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : UICanvas
{
    public override void Open()
    {
        base.Open();
        RewardEndLevelManager.ins.RewardLose();
    }

    public void Btn_Replay()
    {
        LevelManager.ins.LoadLevel(DataManager.ins.playerData.currentLevelIndex, false);
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
    }
}
