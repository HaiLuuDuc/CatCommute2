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
        c.rb.velocity = new Vector3(0, c.rb.velocity.y, 0);
    }

    public override void ExitState(Character c)
    {

    }
}
