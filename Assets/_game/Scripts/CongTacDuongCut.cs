using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CongTacDuongCut : CongTac
{
    public GameObject ground;
    public Transform btn;
    public float pressSpeed;

    public override void OnInit()
    {
        base.OnInit();
        HideGround();
        btn.localPosition = new Vector3(btn.localPosition.x, -1f, btn.localPosition.z);
    }

    public override void OnHit()
    {
        base.OnHit();
        ShowGround();
        StartCoroutine(PressCoroutine());
    }

    public void ShowGround()
    {
        ground.SetActive(true);
    }

    public void HideGround()
    {
        ground.SetActive(false);
    }

    IEnumerator PressCoroutine()
    {
        while (btn.localPosition.y > -1.3f)
        {
            btn.localPosition = Vector3.MoveTowards(btn.localPosition, new Vector3(btn.localPosition.x, -1.3f, btn.localPosition.z), Time.deltaTime * pressSpeed);
            yield return null;
        }
    }
}
