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
}
