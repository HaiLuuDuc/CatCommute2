using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_StateBase
{
    public abstract void EnterState(Character c);
    public abstract void UpdateState(Character c);
    public abstract void ExitState(Character c);
    /*public abstract void OnTriggerEnterState(Character c, Collider other);
    public abstract void OnTriggerStayState(Character c, Collider other);
    public abstract void OnTriggerExitState(Character c, Collider other);*/

}
