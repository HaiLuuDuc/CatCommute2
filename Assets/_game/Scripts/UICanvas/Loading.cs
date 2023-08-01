using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : UICanvas
{
    public Slider slider;


    public override void Open()
    {
        base.Open();
        StartCoroutine(LoadCoroutine());
    }

    public IEnumerator LoadCoroutine()
    {
        float value = 0;
        while(value < 1)
        {
            value += Time.deltaTime/3;
            slider.value = value;
            yield return null;
        }
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<Home>();
        yield return null;
    }
}
