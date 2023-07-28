using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Run : Character_StateBase
{
    public override void EnterState(Character c)
    {
        if (!c.isRoot)
        {
            if(c.frontCharacter == null)
            {
                c.frontCharacter = Player.ins.characterList[Player.ins.characterList.Count - 1];
            }
            c.transform.SetParent(Player.ins.transform);

            c.scoreText.text = "";

            c.DisablePhysics();
            Timer.Do(c, () => c.EnablePhysics(), 1f);
        }
        c.ChangeAnim("run");
    }

    public override void UpdateState(Character c)
    {
        if (c.isRoot)
        {
            if (Vector3.Distance(Player.ins.transform.position, Player.ins.targetRun.position) < 0.01f)
            {
                UIManager.ins.OpenUI<Win>();
                foreach(Character character in LevelManager.ins.currentLevel.patrolCharacters)
                {
                    character.SwitchState(character.idleState);
                }
                c.SwitchState(c.idleState);
            }
            else
            {
                Player.ins.Run();
            }
        }
        else
        {
            c.FollowFrontCharacter();
        }
    }

    public override void ExitState(Character c)
    {

    }

    public IEnumerator DoSomethingInSeconds(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
        yield return null;
    }
}
