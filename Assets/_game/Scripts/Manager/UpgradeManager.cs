using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public CharacterData[] characterDatas;
    public GameObject characterRootModel;
    public List<GameObject> patrolModelList = new List<GameObject>();


    public void DecreaseCoin()
    {
        DataManager.ins.playerData.coin -= DataManager.ins.playerData.coinToUpgrade;
        DataManager.ins.playerData.coinToUpgrade += 100;
    }

    public void DecreaseRemainUpgradeCount()
    {
        DataManager.ins.playerData.remainUpgradeCount--;
    }

    public void SetNewLevelBaseOnRemainUpgradeCount()
    {
        if(DataManager.ins.playerData.remainUpgradeCount <= 0)
        {
            switch (DataManager.ins.playerData.characterLevel)
            {
                /*case 1: // lv1 lên lv2
                    DataManager.ins.playerData.remainUpgradeCount = 10; // lv2 lên lv3 cần 10 upgrade
                    break;
                case 2: // lv2 lên lv3
                    DataManager.ins.playerData.remainUpgradeCount = 20;
                    break;
                case 3: // lv3 lên lv4
                    DataManager.ins.playerData.remainUpgradeCount = 30;
                    break;
                case 4: // lv4 lên lv5
                    DataManager.ins.playerData.remainUpgradeCount = 50;
                    break;
                case 5: // lv5 lên lv6
                    DataManager.ins.playerData.remainUpgradeCount = 70;
                    break;
                case 6: // lv6 lên lv7
                    DataManager.ins.playerData.remainUpgradeCount = 100;
                    break;
                case 7: // lv7 lên lv8
                    DataManager.ins.playerData.remainUpgradeCount = 150;
                    break;
                case 8: // lv8 lên lv9
                    DataManager.ins.playerData.remainUpgradeCount = 200;
                    break;*/
                case 1: // lv1 lên lv2
                    DataManager.ins.playerData.remainUpgradeCount = 2; // lv2 lên lv3 cần 10 upgrade
                    break;
                case 2: // lv2 lên lv3
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 3: // lv3 lên lv4
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 4: // lv4 lên lv5
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 5: // lv5 lên lv6
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 6: // lv6 lên lv7
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 7: // lv7 lên lv8
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;
                case 8: // lv8 lên lv9
                    DataManager.ins.playerData.remainUpgradeCount = 2;
                    break;

            }
            DataManager.ins.playerData.characterLevel++;

            // change model
            PoolCharacterModel.ins.ChangePrefab();
            ChangeCharacterRootModel(DataManager.ins.playerData.characterLevel);
            ChangePatrolCharacterModels(DataManager.ins.playerData.characterLevel);

            // characters OnInit
            Player.ins.characterRoot.OnInit(characterRootModel);
            for (int i = 0; i < LevelManager.ins.currentLevel.patrolCharacters.Length; i++)
            {
                Character patrolCharacter = LevelManager.ins.currentLevel.patrolCharacters[i];
                patrolCharacter.OnInit(patrolModelList[i]);
            }
        }
    }

    public void ChangeCharacterRootModel(int characterLevel) // đang được gọi ở loadlevel mới và upgrade thành công
    {
        characterRootModel = PoolCharacterModel.ins.GetModel();
        characterRootModel.transform.SetParent(Player.ins.characterRoot.transform);
        characterRootModel.transform.localPosition = characterDatas[characterLevel - 1].offsetPos;
    }

    public void ChangePatrolCharacterModels(int characterLevel) // đang được gọi ở loadlevel mới và upgrade thành công
    {
        patrolModelList.Clear();

        // spawn new model
        foreach (Character character in LevelManager.ins.currentLevel.patrolCharacters)
        {
            GameObject model = null;
            model = PoolCharacterModel.ins.GetModel();
            model.transform.SetParent(character.transform);
            model.transform.localPosition = characterDatas[characterLevel - 1].offsetPos;
            model.transform.localScale = Vector3.one;
            model.transform.localRotation = Quaternion.Euler(Vector3.zero);
            patrolModelList.Add(model);
        }
    }
}
