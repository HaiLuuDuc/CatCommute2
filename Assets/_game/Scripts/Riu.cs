using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Riu : MonoBehaviour
{
    public float duration;
    public float currentAngleZ;


    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        this.transform.eulerAngles = new Vector3(0, 0, currentAngleZ);
        this.transform
            .DORotate(new Vector3(0, 0, -currentAngleZ), duration, RotateMode.Fast)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

}
