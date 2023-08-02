using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Die : Character_StateBase
{
    public override void EnterState(Character c)
    {
        c.rb.velocity = Vector3.zero;
        c.ChangeAnim("die");
    }

    public override void UpdateState(Character c)
    {
        c.rb.velocity = new Vector3(0, c.rb.velocity.y, 0);
    }

    public override void ExitState(Character c)
    {
        throw new System.NotImplementedException();
    }
}
