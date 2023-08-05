using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Score : ")]
    public int currentScore = 0;
    public int targetScore = 0;
    public int offsetScore;
    public TextMeshProUGUI scoreText;

    [Header("Run : ")]
    public Transform targetRun;
    public float runSpeed;
    public float initialRunSpeed;
    public float blockedRunSpeed;

    [Header("Manage : ")]
    public List<Character> characterList = new List<Character>();
    public Character characterRoot => characterList[0];
    public CharacterData characterData;

    [Header("Eat Tile : ")]
    public float initialEatCoolDown = 0.1f;
    public float currentEatCoolDown = 0.1f;

    public bool isAlive = true;

    private void Start()
    {
        initialRunSpeed = runSpeed;
        blockedRunSpeed = runSpeed/3;
    }


    public void OnStartNewLevel()
    {
        //set target run (ở cuối đường)
        targetRun = LevelManager.ins.currentLevel.targetRun;
        runSpeed = initialRunSpeed;
        //set vị trí ban đầu
        this.transform.position = new Vector3(0, 0, 170f);
        characterRoot.transform.localPosition = Vector3.zero;
        characterRoot.rb.velocity = Vector3.zero;
        
        currentScore = targetScore = DataManager.ins.playerData.playerStartHealth;
        isAlive = true;
    }

    public void ResetCharacterList()
    {
        //xóa hết các character nhỏ, chỉ giữ lại character root //TODO : character pool
        while (characterList.Count > 1)
        {
            //Destroy(characterList[1].gameObject);
            Character patrolCharacter = characterList[1];
            patrolCharacter.transform.SetParent(LevelManager.ins.currentLevel.patrolParent);
            characterList.RemoveAt(1);
        }
    }

    private void Update()
    {
        if (!isAlive) return;

        if(Input.GetKeyDown(KeyCode.Escape)) {
            targetScore = 1000;
        }


        if(currentEatCoolDown > 0)
        {
            currentEatCoolDown -= Time.deltaTime;
        }

        if (currentScore <= 0)
        {
            currentScore = 0;

            if (!LevelManager.ins.currentLevel.arena.isStartFight)
            {
                for (int i = 0; i < characterList.Count; i++)
                {
                    int index = i;
                    Timer.Do(characterList[index], () =>
                    {
                        if(index<characterList.Count)
                        characterList[index].SwitchState(characterList[index].dieState);
                    }, index * Time.deltaTime * 8);
                }
            }

            MovementController.ins.isBlockControl = true;

            // lose UI
            Timer.Do(UIManager.ins, () =>
            {
                UIManager.ins.OpenUI<Lose>();
            }, 1f);

            isAlive = false;
        }

        UpdateScoreText(); // score UI được update liên tục từ currentScore
    }

    public void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRun.position, runSpeed * Time.deltaTime);
        //characterRoot.rb.velocity = Vector3.forward * runSpeed;
    }

    public void GetValueFromMathObject(MathObject mathObj)
    {
        int oldTargetScore;
        switch (mathObj.type)
        {
            case MathType.Add:
                targetScore += mathObj.value;
                offsetScore = mathObj.value;
                //TurnBigger();
                break;
            case MathType.Subtract:
                targetScore -= mathObj.value;
                offsetScore = -mathObj.value;
                //TurnSmaller();
                break;
            case MathType.Multiply:
                oldTargetScore = targetScore;
                targetScore *= mathObj.value;
                offsetScore = targetScore - oldTargetScore;
                //TurnBigger();
                break;
            case MathType.Divide:
                oldTargetScore = targetScore;
                targetScore /= mathObj.value;
                offsetScore = targetScore - oldTargetScore;
                //TurnSmaller();
                break;
        }
    }

  /*  public void TurnBigger()
    {
        transform.localScale += Vector3.one * scaleOffset;
    }

    public void TurnSmaller()
    {
        transform.localScale -= Vector3.one * scaleOffset;
    }*/

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

    //NEEDTOFIX
    public bool IsPointInOABB(Vector3 point, BoxCollider box)
    {
        point = box.transform.InverseTransformPoint(point) - box.center;

        float halfX = (box.size.x * 0.5f);
        float halfY = (box.size.y * 0.5f);
        float halfZ = (box.size.z * 0.5f);

        if (point.x < halfX && point.x > -halfX &&
           point.y < halfY && point.y > -halfY &&
           point.z < halfZ && point.z > -halfZ)
            return true;
        else
            return false;
    }
}
