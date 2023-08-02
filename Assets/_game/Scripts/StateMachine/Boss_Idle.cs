using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : Boss_StateBase
{
    public override void EnterState(Boss b)
    {
        b.ChangeAnim("idle");
    }

    public override void UpdateState(Boss b)
    {

    }
}
