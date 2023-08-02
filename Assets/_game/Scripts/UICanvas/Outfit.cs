using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outfit : UICanvas
{
    public Button[] itemButtons;


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
