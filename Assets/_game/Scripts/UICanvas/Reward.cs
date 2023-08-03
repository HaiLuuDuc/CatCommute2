using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : UICanvas
{

    public void Btn_Close()
    {
        UIManager.ins.CloseUI<Reward>();
    }
}
