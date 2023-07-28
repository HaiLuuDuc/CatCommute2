using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public Transform targetTF;

    private void Start()
    {
        this.transform
            .DOMoveX(targetTF.position.x, 2f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
