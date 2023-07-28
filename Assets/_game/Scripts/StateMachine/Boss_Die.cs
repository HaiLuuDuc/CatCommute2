using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Die : Boss_StateBase
{
    private float lieTime = 2f;
    private float end;
    public override void EnterState(Boss b)
    {
        b.ChangeAnim("die");
        b.OnDeath();
        end = Time.time + lieTime;

    }
    public override void UpdateState(Boss b)
    {
        if (Time.time >= end)
        {
            b.gameObject.SetActive(false);
        }
    }
}
