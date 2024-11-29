using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMons
{

    private static SampleMons instance = null;
    public static SampleMons Instance { get => instance ??  (instance = new SampleMons()); }


    public Pokemon_Data monData1;
    public Pokemon_Data monData2;
    public Pokemon_Data monData3;

    public Pokemon mon;
    public Pokemon mon2;
    public Pokemon mon3;

    public MoveData tackle;
    public MoveData hydroPump;
    public MoveData growl;
    public MoveData earthQuake;

    public SampleMons()
    {
        monData1 = new(572, "Minccino", EType.NORMAL, null, new Stat(55,50,40,40,40,75), 5.8f, 0.4f);
        monData2 = new(380, "Latias", EType.DRAGON, EType.PSYCHIC, new Stat(80,80,90,110,130,110), 52f, 1.8f);
        monData3 = new(802, "Marshadow", EType.FIGHTING, EType.GHOST, new Stat(90,125,80,90,90,125), 22.2f, 0.7f);

        mon = new(1, monData1, ESex.MALE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 252, 0, 0, 0, 252), "Jolly", new int[4]{20,2,18,9});
        mon2 = new(1, monData2, ESex.FEMALE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 0, 0, 252, 0, 252), "Timid", new int[4] { 20, 2, 18, 9 });
        mon3 = new(1, monData3, ESex.NONE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 252, 0, 0, 0, 252), "Adamant", new int[4] { 20, 2, 18, 9 });

        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED, t => MakeMoves());
    }
    public static List<Pokemon> GetList()
    {
        return new() { Instance.mon, Instance.mon2, Instance.mon3 };
    }

    public void MakeMoves()
    {
        tackle = new MoveData("Close Combat", "Hits Mon", 110, 35, EType.FIGHTING, EMoveType.PHYSICAL);
        hydroPump = new MoveData("Hydro Pump", "The target is blasted by a huge volume of water launched under great pressure.", 110, 35, EType.WATER, EMoveType.SPECIAL);
        growl = new MoveData("Dragon Dance", "Hits Mon", 40, 35, EType.DRAGON, EMoveType.STATUS);
        earthQuake = new MoveData("Charge Beam", "Hits Mon", 60, 35, EType.ELECTRIC, EMoveType.SPECIAL);

        Move mTackle = MoveManager.GetMove(18);
        mTackle.Data = tackle;

        Move mHydroPump = MoveManager.GetMove(2);
        mHydroPump.Data = hydroPump;

        Move mGrowl = MoveManager.GetMove(20);
        mGrowl.Data = growl;

        Move mEarthquake = MoveManager.GetMove(9);
        mEarthquake.Data = earthQuake;
    }
}
