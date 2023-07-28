using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Die : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.ChangeAnim("die");
    }

    public override void UpdateState(Character c)
    {

    }

    public override void ExitState(Character c)
    {
        throw new System.NotImplementedException();
    }
}
