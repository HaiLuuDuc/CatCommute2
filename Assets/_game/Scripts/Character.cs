using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class Character : MathObject
{
    public bool isRoot;

    [Header("Follow Front Character : ")]
    public Character frontCharacter;
    public float moveSpeedX;
    public float moveSpeedZ;
    public static float distance = 1.76f;

    [Header("Components : ")]
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;

    public static float patrolSpeed = 1.6f;
    public static float jumpForce = 700f;


    [Header("Anim : ")]
    public Animator anim;
    public string currentAnim;

    [Header("Wall Detetion : ")]
    public Vector3 wallContactPoint;
    public bool isCollidingWall = false;
    public bool isInWallZone = false;

    public GameObject model;


    public bool isJump = false;


    #region STATE MACHINE
    public Character_StateBase currentState;
    [HideInInspector] public Character_Idle idleState = new Character_Idle();
    [HideInInspector] public Character_Run runState = new Character_Run();
    [HideInInspector] public Character_Fight fightState = new Character_Fight();
    [HideInInspector] public Character_QueueToFight queueToFightState = new Character_QueueToFight();
    [HideInInspector] public Character_PatrolState patrolState = new Character_PatrolState();
    [HideInInspector] public Character_Die dieState = new Character_Die();
    [HideInInspector] public Character_Dance danceState = new Character_Dance();

    public void SwitchState(Character_StateBase state)
    {
        this.currentState = state;
        this.currentState.EnterState(this);
    }
    #endregion

    /*private void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();
    }*/


    private void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void OnInit(UnityEngine.GameObject model) // sau khi instantiate model
    {
        rb = this.GetComponent<Rigidbody>();
        capsuleCollider = model.GetComponent<CapsuleCollider>();
        anim = model.GetComponent<Animator>();
        anim.enabled = true;
        isCollidingWall = false;
        isInWallZone = false;

        EnablePhysics();

        if (isRoot)
        {
            SwitchState(idleState);
        }
        else
        {
            InitScoreTextUI();
            SwitchState(patrolState);
        }

        this.model = model;

    }

    public void ChangeAnim(string animName)
    {
        
            anim.ResetTrigger(currentAnim);    
            anim.ResetTrigger(animName);
        currentAnim = animName;
            anim.SetTrigger(currentAnim);
    }

    public void InitScoreTextUI()
    {
        string prev = "";
        switch (type)
        {
            case MathType.Add:
                prev = "+";
                break;
            case MathType.Subtract:
                prev = "-";
                break;
            case MathType.Multiply:
                prev = "x";
                break;
            case MathType.Divide:
                prev = "÷";
                break;
        }
        scoreText.text = prev + value.ToString();
        scoreText.outlineWidth = 0.001f;
    }

    public void FollowFrontCharacter()
    {
        if(frontCharacter != null)
        {
            Vector3 followTarget = frontCharacter.transform.position - new Vector3(0, 0, 1) * distance;
            float newX = 0;
            newX = Mathf.Lerp(this.transform.position.x, followTarget.x, moveSpeedX * Time.deltaTime);
            if (isInWallZone)
            {
                if (this.transform.position.x < 0 && followTarget.x > 0)
                {
                    newX = - this.capsuleCollider.radius / 2;
                }
                else if (this.transform.position.x > 0 && followTarget.x < 0)
                {
                    newX = this.capsuleCollider.radius / 2;
                }
            }
            float newZ = Mathf.Lerp(this.transform.position.z, followTarget.z, moveSpeedZ * Time.deltaTime);
            transform.position = new Vector3(newX, this.transform.position.y, newZ);
            LookAt(frontCharacter.transform.position);
        }
    }

    public void GoToFightPos(Vector3 pos, Action OnComplete)
    {
        pos.y = this.transform.position.y;
        this.transform
            .DOMove(pos, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                OnComplete?.Invoke();
            });
    }

    public void LookAt(Vector3 pos)
    {
        Vector3 dir = pos - this.transform.position;
        dir.y = 0;
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        capsuleCollider.isTrigger = false;
    }

    public void DisablePhysics()
    {
        rb.isKinematic = true;
        capsuleCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRoot)
        {
            if (other.CompareTag("tile"))
            {
                if(Player.ins.currentEatCoolDown <= 0)
                {
                    Tile t = other.GetComponent<Tile>();
                    Player.ins.GetValueFromMathObject(t);
                    Player.ins.LerpCurrentScore(0.3f);
                    ScorePopUp.ins.Show(Player.ins.offsetScore, t.meshRenderer.material.color);
                    t.OnHit();
                    Player.ins.currentEatCoolDown = Player.ins.initialEatCoolDown;
                }
            }

            if (other.CompareTag("character"))
            {
                Character c = other.GetComponentInParent<Character>();
                if (!Player.ins.characterList.Contains(c))
                {
                    Player.ins.GetValueFromMathObject(c);
                    Player.ins.LerpCurrentScore(0.3f);
                    ScorePopUp.ins.Show(Player.ins.offsetScore, Color.white);
                    c.SwitchState(c.runState);
                    Player.ins.characterList.Add(c);
                }
            }

            if (other.CompareTag("arena"))
            {
                LevelManager.ins.currentLevel.arena.PutCharactersInFightPos();
            }

            if (other.CompareTag("deathzone") && !MovementController.ins.isBlockControl)
            {
                Debug.Log("deathzone");
                CameraFollow.ins.target = null;
                MovementController.ins.isBlockControl = true;
                foreach (Character character in Player.ins.characterList)
                {
                    character.SwitchState(idleState);
                }
                Player.ins.isAlive = false;
                Timer.Do(UIManager.ins, () => {
                    UIManager.ins.OpenUI<Lose>();
                }, 1f);
            }

            

        }

        if (other.CompareTag("luoicua"))
        {
            if (isRoot)
            {
                Player.ins.targetScore = Player.ins.currentScore = 0;
                UpgradeManager.ins.characterRootModel.gameObject.SetActive(false);
            }
            else
            {
                int idx = Player.ins.characterList.IndexOf(this);
                if (idx < Player.ins.characterList.Count - 1) // k phải thằng cuối
                {
                    Player.ins.characterList[idx + 1].frontCharacter = this.frontCharacter;// bàn giao frontCharacter
                }

                Player.ins.characterList.Remove(this);
                this.transform.SetParent(LevelManager.ins.currentLevel.patrolParent);
                PoolCharacterModel.ins.ReturnToPool(this.model);
                this.gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("jump") && isJump == false && Player.ins.isAlive)
        {
            Debug.Log(this.gameObject.name + " jump");
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isJump = true;
        }

        if (other.CompareTag("riu"))
        {
            if (isRoot)
            {
                Player.ins.targetScore = Player.ins.currentScore = 0;
            }
            else
            {
                int idx = Player.ins.characterList.IndexOf(this);
                if (idx < Player.ins.characterList.Count - 1) // k phải thằng cuối
                {
                    Player.ins.characterList[idx + 1].frontCharacter = this.frontCharacter;// bàn giao frontCharacter
                }

                Player.ins.characterList.Remove(this);
                this.transform.SetParent(LevelManager.ins.currentLevel.patrolParent);
                /*PoolCharacterModel.ins.ReturnToPool(this.model);
                this.gameObject.SetActive(false);*/
                this.SwitchState(dieState);
            }
        }

        if (other.CompareTag("wallzone"))
        {
            isInWallZone = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("jump"))
        {
            isJump = false;
        }

        if (other.CompareTag("wallzone"))
        {
            isInWallZone = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "damagecube")
        {
            DamageCube damageCube = collision.gameObject.GetComponent<DamageCube>();
            if (damageCube.isCollisionWithCharacter) return;
            Player.ins.runSpeed = Player.ins.blockedRunSpeed;
            Player.ins.targetScore -= (int)damageCube.initialValue;
            Player.ins.LerpCurrentScore(damageCube.initialValue/damageCube.decreaseSpeed);
            damageCube.isCollisionWithCharacter = true;
        }

       
            if (collision.gameObject.tag == "wall")
            {
                isCollidingWall = true;
                if (collision.contacts.Length > 0)
                {
                    wallContactPoint = collision.contacts[0].point;
                }
            }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "damagecube")
        {
            DamageCube damageCube = collision.gameObject.GetComponent<DamageCube>();
            damageCube.OnCharacterStay();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            wallContactPoint = Vector3.zero;
            isCollidingWall = false;
        }
    }

}
