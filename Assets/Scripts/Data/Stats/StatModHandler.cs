using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatModHandler 
{
    private Dictionary<EStatType, int> mods = new();

    public StatModHandler()
    {
        int count = Enum.GetNames(typeof(EStatType)).Length;

        //Excludes Health
        for (int i = 1; i < count; i++)
            mods[(EStatType)i] = 0;
    }

    public void AddMod(EStatType type, int stageCount) => mods[type] = Mathf.Clamp(mods[type] + stageCount, -6, 6);

    public Stat ApplyMods(Stat baseStats)
    {
        var newStat = new Stat(baseStats);

        float getStatModValue(int mod)
        {
            float numerator = 2 + (mod > 0 ? mod : 0);
            float denominator = 2 + (mod < 0 ? -mod : 0);

            return numerator/denominator;
        }

        newStat.DoOnAll( 
            t => {
                List<EStatType> keys = t.Keys.ToList();
                keys.Remove(EStatType.HEALTH);

                foreach (var key in keys)
                    t[key] *=  getStatModValue(mods[key]);           
            }                   
        );

        return newStat;
    }
    

}
 