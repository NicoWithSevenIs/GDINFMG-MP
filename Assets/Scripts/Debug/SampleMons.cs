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
    public MoveData scratch;
    public MoveData growl;
    public MoveData earthQuake;

    public SampleMons()
    {

        tackle = new MoveData("Tackle", "Hits Mon", 40, 35, EType.NORMAL, EMoveType.PHYSICAL);
        scratch = new MoveData("Ember", "Hits Mon", 40, 35, EType.FIRE, EMoveType.SPECIAL);
        growl = new MoveData("Close Combat", "Hits Mon", 40, 35, EType.FIGHTING, EMoveType.PHYSICAL);
        earthQuake = new MoveData("Earthquake", "Hits Mon", 40, 35, EType.GROUND, EMoveType.PHYSICAL);

        Move mTackle = MoveManager.GetMove(1);
        mTackle.Data = tackle;

        Move mScratch = MoveManager.GetMove(2);
        mScratch.Data = scratch;

        Move mGrowl = MoveManager.GetMove(3);
        mGrowl.Data = growl;

        Move mEarthquake = MoveManager.GetMove(4);
        mEarthquake.Data = earthQuake;

        monData1 = new(572, "Minccino", EType.NORMAL, null, new Stat(55,50,40,40,40,75), 5.8f, 0.4f);
        monData2 = new(380, "Latias", EType.DRAGON, EType.PSYCHIC, new Stat(80,80,90,110,130,110), 52f, 1.8f);
        monData3 = new(802, "Marshadow", EType.FIGHTING, EType.GHOST, new Stat(90,125,80,90,90,125), 22.2f, 0.7f);

        mon = new(1, monData1, ESex.MALE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 252, 0, 0, 0, 252), "Jolly", new int[4]{1,2,3,4});
        mon2 = new(1, monData2, ESex.FEMALE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 0, 0, 252, 0, 252), "Timid", new int[4] { 1, 2, 3, 4 });
        mon3 = new(1, monData3, ESex.NONE, new Stat(31, 31, 31, 31, 31, 31), new Stat(6, 252, 0, 0, 0, 252), "Adamant", new int[4] { 1, 2, 3, 4 });
    }
    public static List<Pokemon> GetList()
    {
        return new() { Instance.mon, Instance.mon2, Instance.mon3 };
    }

}
