using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCheckDebugger : MonoBehaviour
{

    Stat stat = new();

    StatModHandler handler = new();
    int index = 1;
    int len = Enum.GetNames(typeof(EStatType)).Length;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handler.ApplyMods(this.stat);
            return;
        }

        EStatType stat = (EStatType)index;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            handler.AddMod(stat, -1);
            Debug.Log($"Debuffing {stat.ToString()}");
        }
           

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            handler.AddMod(stat, 1);
            Debug.Log($"Buffing {stat.ToString()}");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = Mathf.Min(index + 1, len - 1);
            stat = (EStatType)index;
            Debug.Log($"Now editing {stat.ToString()}");
        }
            

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = Mathf.Max(index - 1, 1);
            stat = (EStatType)index;
            Debug.Log($"Now editing {stat.ToString()}");
        }
            

    }
}
