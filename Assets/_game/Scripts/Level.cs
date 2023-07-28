using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Map : ")]
    public Transform leftBorder;
    public Transform rightBorder;
    public Transform targetRun;

    [Header("Tiles : ")]
    public Tile[] tiles;

    [Header("Patrol Characters : ")]
    public Character[] patrolCharacters;
    public Transform[] initialPos;
    public Transform patrolParent;

    [Header("Arena : ")]
    public Arena arena;
    public Boss boss;

    [Header("Traps : ")]
    public LuoiCua[] luoiCuas;

    public int estimatedMaxScore;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach(var character in patrolCharacters)
            {
                character.SwitchState(character.patrolState);
                character.ChangeAnim("run");
            }
        }
    }

    public void CalculateMaxScore()
    {
        // duyet qua tung couple, chon ra phuong an tot nhat
        for (int i = 0; i < tiles.Length; i += 2)
        {
            Tile tile1 = tiles[i];
            Tile tile2 = tiles[i+1];
            int scoreAfter1 = tile1.CalculateWith(estimatedMaxScore);
            int scoreAfter2 = tile2.CalculateWith(estimatedMaxScore);
            estimatedMaxScore = Mathf.Max(scoreAfter1, scoreAfter2);
            if (estimatedMaxScore == 0) return; //die
        }

        for (int i = 0; i < patrolCharacters.Length; i++)
        {
            Character c = patrolCharacters[i];
            int scoreAfter = c.CalculateWith(estimatedMaxScore);
            estimatedMaxScore = Mathf.Max(scoreAfter, estimatedMaxScore);
            if (estimatedMaxScore == 0) return; //die
        }
    }

    public void SetUpMap()
    {
        // calculate max score (for fun)
        estimatedMaxScore = Player.ins.targetScore;

        // tiles
        tiles = GetComponentsInChildren<Tile>(true);
        foreach (Tile tile in tiles)
        {
            tile.gameObject.SetActive(true);
            tile.OnInit();
        }

        // patrol characters
        patrolCharacters = GetComponentsInChildren<Character>(true);
        for (int i = 0; i < patrolCharacters.Length; i++)
        {
            Character patrolCharacter = patrolCharacters[i];
            patrolCharacter.gameObject.SetActive(true);
            patrolCharacter.transform.position = initialPos[i].position;
        }

        CalculateMaxScore(); // tính toán sau khi đã setup value của tiles và patrol characters

        // traps
        luoiCuas = GetComponentsInChildren<LuoiCua>();
        foreach (LuoiCua luoiCua in luoiCuas) {
            luoiCua.gameObject.SetActive(true);
            luoiCua.OnInit();
        }

        // boss
        boss = LevelManager.ins.currentLevel.arena.boss;
        boss.gameObject.SetActive(true);
        //boss.OnInit((estimatedMaxScore - 500) / 100 * 100); // boss it hon player 500hp  
        boss.OnInit(1000); // 1000 hp
    }

    public void SetUpPatrolCharacters()
    {
        for (int i = 0; i < patrolCharacters.Length; i++)
        {
            Character patrolCharacter = patrolCharacters[i];
            patrolCharacter.DisplayValue();
            patrolCharacter.frontCharacter = null;
        }
    }
}
