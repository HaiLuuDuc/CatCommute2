using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEndLevelManager : Singleton<RewardEndLevelManager>
{
    public int rewardCoin;

    public void RewardWin()
    {
        rewardCoin = (int)(25 * (1 + ((float)Player.ins.targetScore * (0.05f + (0.01f * (float)(DataManager.ins.playerData.rwCoinLevel-1))))));
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