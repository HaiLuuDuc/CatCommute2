using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;

public class TruDiDong : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public float rotateSpeed;
    public float moveSpeed;
    public float moveTime;

    public bool isMoveRight = true;

    public bool isRotate = true;

    Coroutine rotateCoroutine = null;
    Coroutine moveCoroutine = null;

    public void OnInit()
    {
        if(rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        if(isRotate) rotateCoroutine = StartCoroutine(RotateCoroutine());
        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    public IEnumerator RotateCoroutine()
    {
        while (true)
        {
            this.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator MoveCoroutine( )
    {
        Vector3 targetMove;
        if (isMoveRight) targetMove = right.transform.position;
        else targetMove = left.transform.position;
        targetMove.y = this.transform.position.y;
        while (Vector3.Distance(this.transform.position, targetMove) > 0.01f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetMove, moveSpeed * Time.deltaTime);
            yield return null;
        }

        //test dotween
        MoveAround();
        yield return null;
        
    }

    public void MoveAround()
    {
        if(isMoveRight)
        {
            this.transform
            .DOMoveX(left.transform.position.x, moveTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                this.transform
                    .DOMoveX(right.transform.position.x, moveTime)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        MoveAround();
                    });
            });
        }
        else
        {
            this.transform
            .DOMoveX(right.transform.position.x, moveTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                this.transform
                    .DOMoveX(left.transform.position.x, moveTime)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        MoveAround();
                    });
            });
        }
        
    }


}
