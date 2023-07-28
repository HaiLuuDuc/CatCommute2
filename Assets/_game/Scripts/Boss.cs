using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MathObject
{
    public int currentScore;
    public int targetScore;

    public Rigidbody rb;
    public CapsuleCollider collider;
    public bool isAlive = true;

    public Animator anim;
    private string currentAnim;

    #region STATE MACHINE
    public Boss_StateBase currentState;
    [HideInInspector] public Boss_Idle idleState = new Boss_Idle();
    [HideInInspector] public Boss_Fight fightState = new Boss_Fight();
    [HideInInspector] public Boss_Die dieState = new Boss_Die();

    public void SwitchState(Boss_StateBase state)
    {
        this.currentState = state;
        this.currentState.EnterState(this);
    }
    #endregion

    private void Update()
    {
        //if (!isAlive) return;
        if(currentState!=null)
        {
            currentState.UpdateState(this);
        }
        UpdateScoreText();
    }

    public void OnInit(int maxHealth)
    {
        currentScore = targetScore = value = maxHealth;
        UpdateScoreText();
        SwitchState(idleState);
        isAlive = true;
    }

    public void ChangeAnim(string animName)
    {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
    }

    public void UpdateScoreText()
    {
        if(currentScore <= 0)
        {
            scoreText.text = "";
        }
        else
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void LerpCurrentScore(float lerpTime)
    {
        StartCoroutine(LerpInt(currentScore, targetScore, lerpTime));
    }

    private IEnumerator LerpInt(int a, int b, float duration)
    {
        float elapsedTime = 0f;

        currentScore = a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            currentScore = Mathf.RoundToInt(Mathf.Lerp(a, b, t));
            yield return null;
        }
        currentScore = b;
    }

    public void OnDeath()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        scoreText.text = "";
        isAlive = false;
    }
}
