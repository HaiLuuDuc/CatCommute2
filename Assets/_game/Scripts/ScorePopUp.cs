using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class ScorePopUp : Singleton<ScorePopUp>
{
    public TextMeshProUGUI scoreText;
    Coroutine showCoroutine = null;
    Tween bigger = null;
    Tween smaller = null;

    private void Start()
    {
        Hide();
    }

    public void Show(int value, Color textColor)
    {
        if(showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
            showCoroutine = null;
        }
        Hide();
        DOTween.Kill(transform);
        scoreText.color = textColor;
        if (value>0)
        {
            scoreText.text = "+" + value.ToString();
        }
        else
        {
            scoreText.text = value.ToString();
        }

        //dotween
        showCoroutine = StartCoroutine(ShowCoroutine());
    }

    public IEnumerator ShowCoroutine()
    {
        this.transform.localScale = Vector3.one * 0.3f;
        bigger = this.transform
            .DOScale(Vector3.one, .2f)
            .SetEase(Ease.OutBack)
            .SetLoops(0, LoopType.Yoyo);

        yield return new WaitForSeconds(0.5f);
             
        smaller =  this.transform
            .DOScale(Vector3.one * 0.3f, .2f)
            .SetEase(Ease.InBack)
            .SetLoops(0, LoopType.Yoyo)
            .OnComplete(() =>
            {
                Hide();
            });

        showCoroutine = null;
        yield return null;
    }

    public void Hide()
    {
        scoreText.text = "";
    }
}
