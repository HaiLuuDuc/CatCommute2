using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour 
{
    public Transform[] fightPositions;
    public Boss boss;

    public float fightDuration = 2f;

    public bool isStartFight = false;

    public void PutCharactersInFightPos()
    {
        for (int i = 0; i < Player.ins.characterList.Count; i++)
        {
            Character c = Player.ins.characterList[i];
            c.SwitchState(c.queueToFightState);
        }
        boss.SwitchState(boss.fightState);
    }

    public void StartFight()
    {
        if (isStartFight) return;
        //calculate who will win
        if (Player.ins.currentScore > boss.value) // player win
        {
            Player.ins.targetScore = Player.ins.targetScore - boss.value;
            boss.targetScore = 0;
            LevelManager.ins.isPlayerWin = 1;
        }
        else if (Player.ins.currentScore < boss.value) // player lose
        {
            Player.ins.targetScore = 0;
            boss.targetScore = boss.targetScore - Player.ins.currentScore;
            LevelManager.ins.isPlayerWin = -1;
        }
        else // hoa`
        {
            Player.ins.targetScore = 0;
            boss.targetScore = 0;
            LevelManager.ins.isPlayerWin = 0;
        }
        // lerp score UI
        Player.ins.LerpCurrentScore(fightDuration);
        boss.LerpCurrentScore(fightDuration);
        /*
        if (LevelManager.ins.isPlayerWin <= 0)
        {
            Timer.Do(UIManager.ins, () =>
            {
                UIManager.ins.OpenUI<Lose>();
            }, fightDuration + 2f);
        }*/
        

        isStartFight = true;
    }

}
