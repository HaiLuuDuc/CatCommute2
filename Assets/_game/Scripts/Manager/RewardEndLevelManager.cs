using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEndLevelManager : Singleton<RewardEndLevelManager>
{
    public float rewardCoin;

    public void RewardWin()
    {
        rewardCoin = 2.5f * Player.ins.targetScore;
        Debug.Log("rewardCoin : " + rewardCoin);
    }

    public void RewardLose()
    {
        if (LevelManager.ins.currentLevel.arena.isStartFight) // thua khi đánh boss
        {
            rewardCoin = 25;
        }
        else
        {
            rewardCoin = 0;
        }
        Debug.Log("rewardCoin : " + rewardCoin);
    }
}