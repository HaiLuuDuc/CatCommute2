using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.UI;
using JetBrains.Annotations;

public class DataManager : MonoBehaviour
{
    public static DataManager ins;
    private void Awake()
    {
        ins = this;
        this.LoadData();
    }
    /*private void Start()
    {
        this.LoadData();
    }*/
    public bool isLoaded = false;
    public PlayerData playerData;
    public const string PLAYER_DATA = "PLAYER_DATA";


    private void OnApplicationPause(bool pause) { SaveData(); }
    private void OnApplicationQuit() { SaveData(); }


    public void LoadData()
    {
        string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        if (d != "")
        {
            playerData = JsonUtility.FromJson<PlayerData>(d);
        }
        else
        {
            playerData = new PlayerData();
        }

        // sau khi hoàn thành tất cả các bước load data ở trên
        isLoaded = true;
        // FirebaseManager.Ins.OnSetUserProperty();  
    }

    public void SaveData()
    {
        if (!isLoaded) return;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, json);
    }
}


[System.Serializable]
public class PlayerData
{
    /*[Header("--------- Game Setting ---------")]
    public bool isNew = true;
    public bool isMusic = true;
    public bool isSound = true;
    public bool isVibrate = true;
    public bool isNoAds = false;
    public int starRate = -1;*/


    [Header("--------- Game Params ---------")]
    public bool isSetUp = false;
    public int characterLevel;
    public int coin;
    public int remainUpgradeCount;
    public int coinToUpgrade;
    public int currentLevelIndex;

    public PlayerData()
    {
        characterLevel = 1;
        coin = 1000000;
        remainUpgradeCount = 5;
        coinToUpgrade = 100;
        currentLevelIndex = -1;
    }
}

