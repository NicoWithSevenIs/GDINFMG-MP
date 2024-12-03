using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{

    private Dictionary<EStatType, float> s = new();

    public float GetByEnum(EStatType type) => s[type];
    public float SetStatByEnum(EStatType type, float val) => s[type] = val;

    public float Health { get => s[EStatType.HEALTH]; }
    public float Attack { get => s[EStatType.ATTACK]; }
    public float Defense { get => s[EStatType.DEFENSE]; }
    public float Special_Attack { get => s[EStatType.SPECIAL_ATTACK]; }
    public float Special_Defense { get => s[EStatType.SPECIAL_DEFENSE]; }
    public float Speed { get => s[EStatType.SPEED]; }

    public Stat()
    {
        int size = Enum.GetNames(typeof(EStatType)).Length;
        for (int i = 0; i < size; i++)
            s[(EStatType)i] = 0;
        //Debug.LogError("default stat construct called.");
    }

    public Stat(float HP, float ATK, float DEF, float spATK, float spDEF, float SPE)
    {
        s[EStatType.HEALTH] = HP;
        s[EStatType.ATTACK] = ATK;
        s[EStatType.DEFENSE] = DEF;
        s[EStatType.SPECIAL_ATTACK] = spATK;
        s[EStatType.SPECIAL_DEFENSE] = spDEF;
        s[EStatType.SPEED] = SPE;
    }

    public Stat(Stat stat) => s = new(stat.s);
    public void DoOnAll(Action<Dictionary<EStatType, float>> action) => action(s);
    
}
