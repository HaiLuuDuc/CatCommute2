using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss_StateBase
{
    public abstract void EnterState(Boss b);
    public abstract void UpdateState(Boss b);
   
}