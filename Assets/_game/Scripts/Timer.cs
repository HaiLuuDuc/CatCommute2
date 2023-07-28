using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static void Do(MonoBehaviour monoBehaviour,Action action, float delayTime)
    {
        monoBehaviour.StartCoroutine(DoCoroutine(action, delayTime));
    }

    public static IEnumerator DoCoroutine(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action?.Invoke();
        yield return null;
    }
}
