using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_QueueToFight : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.ChangeAnim("idle");
        c.DisablePhysics();

        int characterIndex = Player.ins.characterList.IndexOf(c);
        c.GoToFightPos(LevelManager.ins.currentLevel.arena.fightPositions[characterIndex].position, () =>
        {
            Timer.Do(c, () => { c.SwitchState(c.fightState); }, 0.5f);
        });

        MovementController.ins.isBlockControl = true;
        if (c.isRoot)
        {
            CameraFollow.ins.target = c.transform;
        }
    }

    public override void UpdateState(Character c)
    {
        c.LookAt(LevelManager.ins.currentLevel.boss.transform.position);
    }

    public override void ExitState(Character c)
    {

    }
}
