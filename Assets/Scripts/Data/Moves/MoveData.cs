using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveData 
{
    public string name;
    public string description;
    public int power;
    public int pp;
    public EMoveType moveType;

    public MoveData(string name, string description, int power, int pp, int moveType)
    {
        this.name = name;
        this.description = description;
        this.power = power;
        this.pp = pp;
        this.moveType = (EMoveType) Mathf.Clamp(moveType, 0, Enum.GetNames(typeof(EMoveType)).Length);
    }

}
