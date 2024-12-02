using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static void InvokeAfter(MonoBehaviour script, float delay, Action action){
        IEnumerator waiter()
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
        script.StartCoroutine(waiter());
    }

    public static void SetUIActive(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }
}
