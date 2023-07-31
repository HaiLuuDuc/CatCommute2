using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Fight : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.ChangeAnim("fight");
        LevelManager.ins.currentLevel.arena.StartFight();
    }

    public override void UpdateState(Character c)
    {
        if(LevelManager.ins.isPlayerWin == 1 && LevelManager.ins.currentLevel.boss.currentScore <= 0)
        {
            LevelManager.ins.currentLevel.boss.SwitchState(LevelManager.ins.currentLevel.boss.dieState);
            c.SwitchState(c.danceState);
            Timer.Do(UIManager.ins, () =>
            {
                UIManager.ins.OpenUI<Win>();
            }, 2f);
            MovementController.ins.isBlockControl = true;
        }
        else if(LevelManager.ins.isPlayerWin == -1 && Player.ins.currentScore <= 0)
        {
            if(LevelManager.ins.currentLevel.boss.isAlive)
            {   
                LevelManager.ins.currentLevel.boss.SwitchState(LevelManager.ins.currentLevel.boss.idleState);
            }
            c.SwitchState(c.dieState);
            MovementController.ins.isBlockControl = false;
        }
        else if (LevelManager.ins.isPlayerWin == 0 && Player.ins.currentScore <= 0)
        {
            if (LevelManager.ins.currentLevel.boss.isAlive)
            {
                LevelManager.ins.currentLevel.boss.SwitchState(LevelManager.ins.currentLevel.boss.dieState);
            }
            c.SwitchState(c.dieState);
            MovementController.ins.isBlockControl = false;
        }
    }

    public override void ExitState(Character c)
    {

    }

}
