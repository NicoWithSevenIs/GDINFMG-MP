using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Nature {

    private static Dictionary<EStatType, Dictionary<EStatType, string>> natures = null;

    private static void TryInit()
    {
        if (natures != null)
            return;

        natures = new();

        void AddNatureGroup(EStatType increasedStat, Dictionary<EStatType, string> decreasedStats) => natures.Add(increasedStat, decreasedStats);


        AddNatureGroup(EStatType.ATTACK, new() {
            {EStatType.ATTACK,  "Hardy" },
            {EStatType.DEFENSE, "Lonely"},
            {EStatType.SPEED,   "Brave" },
            {EStatType.SPECIAL_ATTACK, "Adamant"},
            {EStatType.SPECIAL_DEFENSE, "Naughty"}
        });

        AddNatureGroup(EStatType.DEFENSE, new() {
            {EStatType.ATTACK,  "Bold" },
            {EStatType.DEFENSE, "Docile"},
            {EStatType.SPEED,   "Relaxed" },
            {EStatType.SPECIAL_ATTACK, "Impish"},
            {EStatType.SPECIAL_DEFENSE, "Lax"}
        });

        AddNatureGroup(EStatType.SPEED, new() {
            {EStatType.ATTACK,  "Timid" },
            {EStatType.DEFENSE, "Hasty"},
            {EStatType.SPEED,   "Serious" },
            {EStatType.SPECIAL_ATTACK, "Jolly"},
            {EStatType.SPECIAL_DEFENSE, "Naive"}
        });

        AddNatureGroup(EStatType.SPECIAL_ATTACK, new() {
            {EStatType.ATTACK,  "Modest" },
            {EStatType.DEFENSE, "Mild"},
            {EStatType.SPEED,   "Quiet" },
            {EStatType.SPECIAL_ATTACK, "Bashful"},
            {EStatType.SPECIAL_DEFENSE, "Rash"}
        });

        AddNatureGroup(EStatType.SPECIAL_DEFENSE, new() {
            {EStatType.ATTACK,  "Calm" },
            {EStatType.DEFENSE, "Gentle"},
            {EStatType.SPEED,   "Sassy" },
            {EStatType.SPECIAL_ATTACK, "Careful"},
            {EStatType.SPECIAL_DEFENSE, "Quirky"}
        });

    }


    public static void GetNatureMultiplier(string nature, out EStatType incremented, out EStatType decremented)
    {

        TryInit();

        incremented = EStatType.HEALTH;
        decremented = EStatType.HEALTH;

        foreach (var increasedStat in natures)
        {
            incremented = increasedStat.Key;

            foreach (var decreasedStat in increasedStat.Value)
            {
                decremented = decreasedStat.Key;

                if (decreasedStat.Value == nature)
                    break;
            }
        }

        if (incremented == EStatType.HEALTH || decremented == EStatType.HEALTH)
            Debug.LogError("Invalid Nature");


    }
   
    
}
