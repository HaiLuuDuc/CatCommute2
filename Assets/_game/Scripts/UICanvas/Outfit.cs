using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Outfit : UICanvas
{
    public Button[] itemButtons;
    public GameObject[] buttonBlocks;
    public RectTransform choosing;


    private void Awake()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i;
            itemButtons[index].onClick.AddListener(()=> {
                UpgradeManager.ins.ChangeModel(index);
            });
        }
    }

    public override void Open()
    {
        base.Open();
        for(int i = 0; i < DataManager.ins.playerData.maxCharacterLevel; i++)
        {
            buttonBlocks[i].gameObject.SetActive(false);
        }
        if(DataManager.ins.playerData.maxCharacterLevel < buttonBlocks.Length)
        for (int i = DataManager.ins.playerData.maxCharacterLevel; i < buttonBlocks.Length; i++)
        {
            buttonBlocks[i].gameObject.SetActive(true);
        }
        choosing.transform.position = itemButtons[DataManager.ins.playerData.currentCharacterLevel - 1].transform.position;
    }

    public void Btn_Back()
    {
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
    }

    public void Btn_WatchAd()
    {

    }
}
