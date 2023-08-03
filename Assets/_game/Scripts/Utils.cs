using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static IEnumerator LerpIntCoroutine(int start, int end, float duration, System.Action<int> onUpdate, Action OnComplete = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(start, end, t));
            onUpdate(currentValue);
            yield return null;
        }

        onUpdate(end); // Ensure the final value is set correctly
        OnComplete?.Invoke();
    }

    public static IEnumerator LerpFloatCoroutine(float start, float end, float duration, System.Action<float> onUpdate, System.Action OnComplete = null)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float currentValue = (Mathf.Lerp(start, end, t));
            onUpdate(currentValue);
            yield return null;
        }

        onUpdate(end); // Ensure the final value is set correctly

        OnComplete?.Invoke();
    }
}
