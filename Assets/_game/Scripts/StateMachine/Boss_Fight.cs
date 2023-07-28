using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fight : Boss_StateBase
{
    public override void EnterState(Boss b)
    {
        b.ChangeAnim("fight");
    }

    public override void UpdateState(Boss b)
    {
        if(b.currentScore <= 0)
        {
            b.currentScore = 0;
            b.SwitchState(b.dieState);
        }
    }
}
