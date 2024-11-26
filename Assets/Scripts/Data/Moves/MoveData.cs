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
    public EType type;
    public EMoveType moveType;

    public MoveData(string name, string description, int power, int pp, EType type, EMoveType moveType)
    {
        this.name = name;
        this.description = description;
        this.power = power;
        this.pp = pp;
        this.moveType = moveType;
        this.type = type;
    }

}
