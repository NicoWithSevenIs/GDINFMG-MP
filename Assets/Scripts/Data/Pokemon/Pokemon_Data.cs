using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pokemon_Data 
{

    public int id;
    public string name;
    public ESex sex;

    public EType type1;
    public EType? type2;

    public Stat baseStats;

    public float weight;
    public float height;

    public Pokemon_Data
        (   
            int id,
            string name,
            ESex sex,
            EType type1,
            EType? type2,
            Texture2D front_sprite,
            Texture2D back_sprite,
            Stat baseStats,
            float weight,
            float height
        )
    {
        this.id = id;
        this.name = name;
        this.sex = sex;
        this.type1 = type1;
        this.type2 = type2;
        this.weight = weight;
        this.height = height;
        this.baseStats = baseStats;
    }

}
