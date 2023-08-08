using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels;
    public Level currentLevel;
    public int currentLevelIndex;
    public bool isStartLevel = false;
    public int isPlayerWin;
    public bool isFirstPlay = true;

    public void OnInit()
    {
        if (DataManager.ins.playerData.currentLevelIndex == -1)
        {
            LoadLevel(0, true);
        }
        else
        {
            LoadLevel(DataManager.ins.playerData.currentLevelIndex, true);
        }
    }

    public void DestroyCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
    }

    public void LoadLevel(int newLevelIndex, bool isNewLevel)
    {
        if(isNewLevel)
        {
            DestroyCurrentLevel();
            currentLevel = Instantiate(levels[newLevelIndex]);
            currentLevel.gameObject.SetActive(true);
        }
        else
        {
            // chuyển character thành child của patrolCharacters (trả lại patrolCharacters nếu chơi lại level
            Player.ins.ResetCharacterList();
        }

        PoolCharacterModel.ins.CollectAll();
        Player.ins.OnStartNewLevel();
        currentLevel.SetUpMap(); // khởi tạo mảng patrolCharacters
        UpgradeManager.ins.ChangeCharacterRootModel(DataManager.ins.playerData.currentCharacterLevel);
        UpgradeManager.ins.ChangePatrolCharacterModels(DataManager.ins.playerData.currentCharacterLevel);
        // characters OnInit
        Player.ins.characterRoot.OnInit(UpgradeManager.ins.characterRootModel);
        for (int i = 0; i < currentLevel.patrolCharacters.Length; i++)
        {
            Character patrolCharacter = currentLevel.patrolCharacters[i];
            patrolCharacter.OnInit(UpgradeManager.ins.patrolModelList[i]);
        }
        //switch state
        Player.ins.characterRoot.SwitchState(Player.ins.characterRoot.idleState);
        currentLevel.SetUpPatrolCharacters();


        CameraFollow.ins.OnStartNewLevel();
        MovementController.ins.isBlockControl = false;
        DataManager.ins.playerData.currentLevelIndex = newLevelIndex;
        isStartLevel = false;
        currentLevel.arena.isStartFight = false;
        currentLevelIndex = newLevelIndex;
    }

    public void LoadNextLevel()
    {
        //NEEDTOFIX
        PoolCharacterModel.ins.CollectAll();
        while (Player.ins.characterList.Count > 1)
        {
            Destroy(Player.ins.characterList[1].gameObject);
            Player.ins.characterList.RemoveAt(1);
        }

        if(DataManager.ins.playerData.currentLevelIndex == levels.Length - 1)
        {
            DataManager.ins.playerData.currentLevelIndex = -1;
        }
        LoadLevel(DataManager.ins.playerData.currentLevelIndex + 1, true);
    }

  

    public void StartLevel()
    {
        if(!isStartLevel)
        {
            Player.ins.characterRoot.SwitchState(Player.ins.characterRoot.runState);
            isStartLevel = true;
        }
    }

}
