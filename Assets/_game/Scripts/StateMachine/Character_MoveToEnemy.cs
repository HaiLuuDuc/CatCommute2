using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_MoveToEnemy : Character_StateBase
{
    public Transform target;
    public static float moveSpeed = 30f;
    public bool isExploded = false;

    public override void EnterState(Character c)
    {
        c.DisablePhysics();
        c.DisableCollider();
        isExploded = false;
    }

    public override void UpdateState(Character c)
    {
        if (isExploded) return;
        if(Vector3.Distance(c.transform.position, target.position) < 0.5f)
        {
            PoolCharacterModel.ins.ReturnToPool(c.model);
            target.gameObject.SetActive(false);
            if (c.isRoot) return;
            Player.ins.characterList.Remove(c);
            c.transform.SetParent(LevelManager.ins.currentLevel.patrolParent);
            isExploded = true;
        }
        else
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
