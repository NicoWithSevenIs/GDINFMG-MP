using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatModHandler 
{
    private Dictionary<EStatType, int> mods = new();

    public Dictionary<EStatType, int> Mods { get => mods; }
    public StatModHandler()
    {
        int count = Enum.GetNames(typeof(EStatType)).Length;

        //Excludes Health
        for (int i = 1; i < count; i++)
            mods[(EStatType)i] = 0;
    }

    public void AddMod(EStatType type, int stageCount, string ReceiverName ="")
    {

        if (stageCount == 0)
            return;

        void MakePrompt(string message)
        {
            ActionSequencer.AddToSequenceBack(new() {
                new ActionSequenceComponent(
                    () => {
                        var p = new Dictionary<string, object>();
                        p["Message"] = message;
                        EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
                    }, true
                )
            }, 0);
        }

        if (mods[type] == -6 && stageCount < 0)
        {
            MakePrompt($"{ReceiverName}'s {type.ToString()} won't go any lower");
            return;
        }else if (mods[type] == 6 && stageCount > 0)
        {
            MakePrompt($"{ReceiverName}'s {type.ToString()} won't go any higher");
            return;
        }

        string message = $"{ReceiverName}'s {type.ToString()} ";

        //ermm
        switch (stageCount)
        {
            case 1: message +=  "rose!"; break;
            case 2: message += "rose sharply!"; break;
            case 3:
            case 4:
            case 5:
            case 6: message += "rose drastically!"; break;
            case -1: message += "fell!"; break;
            case -2: message += "harshly fell!"; break;
            case -3:
            case -4:
            case -5:
            case -6: message += "severely fell!"; break;
        }

        MakePrompt(message);
        mods[type] = Mathf.Clamp(mods[type] + stageCount, -6, 6);
    }

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
 