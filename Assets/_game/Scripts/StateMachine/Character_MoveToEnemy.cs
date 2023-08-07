using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_MoveToEnemy : Character_StateBase
{
    public Transform target;
    public static float moveSpeed = 20f;


    public override void EnterState(Character c)
    {
        c.DisablePhysics();
        c.DisableCollider();
    }

    public override void UpdateState(Character c)
    {
        if(Vector3.Distance(c.transform.position, target.position) > 0.01f)
        {
            c.transform.position = Vector3.MoveTowards(c.transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    public override void ExitState(Character c)
    {

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
