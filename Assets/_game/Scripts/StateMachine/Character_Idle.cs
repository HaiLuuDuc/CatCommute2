using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Idle : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.ChangeAnim("idle");
    }

    public override void UpdateState(Character c)
    {

    }

    public override void ExitState(Character c)
    {

    }
}
