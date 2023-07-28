using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_PatrolState : Character_StateBase
{
    private bool isMovingRight;
    public override void EnterState(Character c)
    {
        c.ChangeAnim("run");
        isMovingRight = c.transform.eulerAngles.y == 90?true:false;
        Debug.Log("enter patrol state");
    }

    public override void UpdateState(Character c)
    {
        if(c.transform.position.x >= LevelManager.ins.currentLevel.rightBorder.position.x && isMovingRight)
        {
            isMovingRight = false;
        }
        else if (c.transform.position.x <= LevelManager.ins.currentLevel.leftBorder.position.x && !isMovingRight)
        {
            isMovingRight = true;
        }

        else
        {
            Vector3 patrolTarget = c.transform.position;
            if (isMovingRight)
            {
                patrolTarget.x = LevelManager.ins.currentLevel.rightBorder.position.x;
                c.transform.position = Vector3.MoveTowards(c.transform.position, patrolTarget, Time.deltaTime * Character.patrolSpeed);
                if(Vector3.Distance(c.transform.position, patrolTarget) > 0.01f)
                {
                    c.LookAt(patrolTarget);
                }
            }
            else
            {
                patrolTarget.x = LevelManager.ins.currentLevel.leftBorder.position.x;
                c.transform.position = Vector3.MoveTowards(c.transform.position, patrolTarget, Time.deltaTime * Character.patrolSpeed);
                if (Vector3.Distance(c.transform.position, patrolTarget) > 0.01f)
                {
                    c.LookAt(patrolTarget);
                }
            }
        }

        c.scoreText.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }
    public override void ExitState(Character c)
    {
        throw new System.NotImplementedException();
    }
}
