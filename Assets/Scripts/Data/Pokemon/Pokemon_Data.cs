using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Pokemon_Data 
{

    public int id;
    public int spriteID;
    public string name;
   

    public EType type1;
    public EType? type2;

    public Stat baseStats;

    public float weight;
    public float height;

    public Pokemon_Data
        (   
            int id,
            int spriteID,
            string name,
            EType type1,
            EType? type2,
            Stat baseStats,
            float weight,
            float height
        )
    {
        this.id = id;
        this.spriteID = spriteID;
        this.name = name;
        this.type1 = type1;
        this.type2 = type2;
        this.weight = weight;
        this.height = height;
        if (baseStats == null) Debug.LogError(new Exception("[ERROR]: BASE STAT IN POKEMON_DATA IS NULL."));
        this.baseStats = baseStats;
    }

}
