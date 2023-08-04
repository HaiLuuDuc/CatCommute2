using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;
    // Start is called before the first frame update

    public Level[] levels;
    public int levelToPlay = -1;
    public bool isCheat = false;
    public override void Awake()
    {
        base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 100;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);
        StartCoroutine(I_InitGame());
    }


    IEnumerator I_InitGame()
    {
        /*yield return new WaitUntil(
            () => (
            AppOpenAdsManager.ins != null
            && Ins != null
            && FirebaseManager.ins != null
            && UIManager.Ins != null
            && MaxManager.ins != null
            && LeaderboardManager.Ins != null
            )
        );

        StartCoroutine(FirebaseManager.ins.I_InitFirebase());
        yield return new WaitUntil(() => FirebaseManager.ins.isGetRemoteConfigOK);
        DataManager.ins.LoadData();//show AOA trong nay

        //UIManager.Ins.OpenUI<Leaderboard>();
        //UIManager.Ins.CloseUI<Leaderboard>();*/
        yield return new WaitUntil(
            () => (
                UIManager.ins != null
                && DataManager.ins != null
                && UpgradeManager.ins != null
                && PoolCharacterModel.ins != null
                && LevelManager.ins != null
            )
        );
        PoolCharacterModel.ins.InitAllPools();
        PoolCharacterModel.ins.ChangeMainPool();
        LevelManager.ins.OnInit();

        UIManager.ins.OpenUI<Loading>();
        yield return null;
    }


    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}

}
