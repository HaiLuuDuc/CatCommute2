using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongTacTronXoay : CongTac
{
    public Transform diaXoay;
    public float rotateSpeed;

    public override void OnInit()
    {
        base.OnInit();
        diaXoay.rotation = Quaternion.Euler(new Vector3(0, 0, 69.9f));
    }

    public override void OnHit()
    {
        if (isHit) return;
        base.OnHit();
        StartCoroutine(XoayCoroutine());
    }

    public IEnumerator XoayCoroutine()
    {
        float currentEulerAnglesZ = 29.9f;
        while(currentEulerAnglesZ > -90f)
        {
            currentEulerAnglesZ -= rotateSpeed * Time.deltaTime;
            diaXoay.rotation = Quaternion.Euler(new Vector3(0, 0, currentEulerAnglesZ));
            yield return null;
        }
    }
}
