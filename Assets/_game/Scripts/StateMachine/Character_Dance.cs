using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Dance : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.ChangeAnim("dance");
    }

    public override void UpdateState(Character c)
    {

    }
    public override void ExitState(Character c)
    {

    }
}
