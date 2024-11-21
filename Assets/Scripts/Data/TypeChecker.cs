using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TypeChecker 
{
    #region Singleton
    private static TypeChecker instance = null;
    public static TypeChecker Instance { get => instance ?? ( instance = new() ); }
    #endregion  Singleton 

    /*
         Outer Key: Attacker
         Inner Key: Defender
    */

    private Dictionary<EType, Dictionary<EType, float>> ttable = new();

    private const float SUPER_EFFECTIVE = 2f;
    private const float NOT_VERY_EFFECTIVE = 0.5f;
    private const float NO_EFFECT = 0f;


    /*
        //Creates an entry for each element in Enum TYPE
        int size = Enum.GetNames( typeof( Type ) ).Length;
        for(int i = 0; i < size; i++)
            ttable[(Type)i] = new();
    */

    private TypeChecker() => HandleCombinations();
    

    //Type Matrix too confusing to work with hahahhaha
    private void HandleCombinations()
    {

        void AddTypeEffectiveness(EType Attacker, Dictionary<EType, float> typeInteractions) => ttable[Attacker] = typeInteractions;  
        

        AddTypeEffectiveness(EType.NORMAL, new(){
            {EType.ROCK,     NOT_VERY_EFFECTIVE},
            {EType.GHOST,    NO_EFFECT},
            {EType.STEEL,    NOT_VERY_EFFECTIVE}
        });

        AddTypeEffectiveness(EType.FIRE, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.WATER,    NOT_VERY_EFFECTIVE},
            {EType.GRASS,    SUPER_EFFECTIVE},
            {EType.ICE,      SUPER_EFFECTIVE},
            {EType.BUG,      SUPER_EFFECTIVE},
            {EType.ROCK,     NOT_VERY_EFFECTIVE},
            {EType.DRAGON,   NOT_VERY_EFFECTIVE},
            {EType.STEEL,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.WATER, new() {
            {EType.FIRE,     SUPER_EFFECTIVE},
            {EType.WATER,    NOT_VERY_EFFECTIVE},
            {EType.GRASS,    NOT_VERY_EFFECTIVE},
            {EType.GROUND,   SUPER_EFFECTIVE},
            {EType.ROCK,     SUPER_EFFECTIVE},
            {EType.DRAGON,   NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.ELECTRIC, new() {
            {EType.WATER,    SUPER_EFFECTIVE},
            {EType.ELECTRIC, NOT_VERY_EFFECTIVE},
            {EType.GRASS,    NOT_VERY_EFFECTIVE},
            {EType.GROUND,   NO_EFFECT},
            {EType.FLYING,   SUPER_EFFECTIVE},
            {EType.DRAGON,   NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.GRASS, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.WATER,    SUPER_EFFECTIVE},
            {EType.GRASS,    NOT_VERY_EFFECTIVE},
            {EType.POISON,   NOT_VERY_EFFECTIVE},
            {EType.GROUND,   SUPER_EFFECTIVE},
            {EType.FLYING,   NOT_VERY_EFFECTIVE},
            {EType.BUG,      NOT_VERY_EFFECTIVE},
            {EType.ROCK,     SUPER_EFFECTIVE},
            {EType.DRAGON,   NOT_VERY_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });


        AddTypeEffectiveness(EType.ICE, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.WATER,    NOT_VERY_EFFECTIVE},
            {EType.GRASS,    SUPER_EFFECTIVE},
            {EType.ICE,      NOT_VERY_EFFECTIVE},
            {EType.GROUND,   SUPER_EFFECTIVE},
            {EType.FLYING,   SUPER_EFFECTIVE},
            {EType.DRAGON,   SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.FIGHTING, new() {
            {EType.NORMAL,   SUPER_EFFECTIVE},
            {EType.ICE,      SUPER_EFFECTIVE},
            {EType.POISON,   NOT_VERY_EFFECTIVE},
            {EType.FLYING,   NOT_VERY_EFFECTIVE},
            {EType.PSYCHIC,  NOT_VERY_EFFECTIVE},
            {EType.BUG,      NOT_VERY_EFFECTIVE},
            {EType.ROCK,     SUPER_EFFECTIVE},
            {EType.GHOST,    NO_EFFECT},
            {EType.DARK,     SUPER_EFFECTIVE},
            {EType.STEEL,    SUPER_EFFECTIVE},
            {EType.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.POISON, new() {
            {EType.GRASS,    SUPER_EFFECTIVE},
            {EType.POISON,   NOT_VERY_EFFECTIVE},
            {EType.GROUND,   NOT_VERY_EFFECTIVE},
            {EType.ROCK,     NOT_VERY_EFFECTIVE},
            {EType.GHOST,    NOT_VERY_EFFECTIVE},
            {EType.STEEL,    NO_EFFECT},
            {EType.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.GROUND, new() {
            {EType.FIRE,     SUPER_EFFECTIVE},
            {EType.ELECTRIC, SUPER_EFFECTIVE},
            {EType.GRASS,    NOT_VERY_EFFECTIVE},
            {EType.POISON,   SUPER_EFFECTIVE},
            {EType.FLYING,   NO_EFFECT},
            {EType.BUG,      NOT_VERY_EFFECTIVE},
            {EType.ROCK,     SUPER_EFFECTIVE},
            {EType.STEEL,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.FLYING, new() {
            {EType.ELECTRIC, NOT_VERY_EFFECTIVE},
            {EType.GRASS,    SUPER_EFFECTIVE},
            {EType.FIGHTING, SUPER_EFFECTIVE},
            {EType.BUG,      SUPER_EFFECTIVE},
            {EType.ROCK,     NOT_VERY_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.PSYCHIC, new() {
            {EType.FIGHTING, SUPER_EFFECTIVE},
            {EType.POISON,   SUPER_EFFECTIVE},
            {EType.PSYCHIC,  NOT_VERY_EFFECTIVE},
            {EType.DARK,     NO_EFFECT},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.BUG, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.GRASS,    SUPER_EFFECTIVE},
            {EType.FIGHTING, NOT_VERY_EFFECTIVE},
            {EType.POISON,   NOT_VERY_EFFECTIVE},
            {EType.FLYING,   NOT_VERY_EFFECTIVE},
            {EType.PSYCHIC,  SUPER_EFFECTIVE},
            {EType.GHOST,    NOT_VERY_EFFECTIVE},
            {EType.DARK,     SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
            {EType.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.ROCK, new() {
            {EType.FIRE,     SUPER_EFFECTIVE},
            {EType.ICE,      SUPER_EFFECTIVE},
            {EType.FIGHTING, NOT_VERY_EFFECTIVE},
            {EType.GROUND,   NOT_VERY_EFFECTIVE},
            {EType.FLYING,   SUPER_EFFECTIVE},
            {EType.BUG,      SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.GHOST, new() {
            {EType.NORMAL,   NO_EFFECT},
            {EType.PSYCHIC,  SUPER_EFFECTIVE},
            {EType.GHOST,    SUPER_EFFECTIVE},
            {EType.DARK,     NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.DRAGON, new() {
            {EType.DRAGON,   SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
            {EType.FAIRY,    NO_EFFECT},
        });

        AddTypeEffectiveness(EType.DARK, new() {
            {EType.FIGHTING, NOT_VERY_EFFECTIVE},
            {EType.PSYCHIC,  SUPER_EFFECTIVE},
            {EType.GHOST,    SUPER_EFFECTIVE},
            {EType.DARK,     NOT_VERY_EFFECTIVE},
            {EType.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.STEEL, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.WATER,    NOT_VERY_EFFECTIVE},
            {EType.ELECTRIC, NOT_VERY_EFFECTIVE},
            {EType.ICE,      SUPER_EFFECTIVE},
            {EType.BUG,      SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
            {EType.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.STEEL, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.WATER,    NOT_VERY_EFFECTIVE},
            {EType.ELECTRIC, NOT_VERY_EFFECTIVE},
            {EType.ICE,      SUPER_EFFECTIVE},
            {EType.BUG,      SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
            {EType.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(EType.STEEL, new() {
            {EType.FIRE,     NOT_VERY_EFFECTIVE},
            {EType.FIGHTING, SUPER_EFFECTIVE},
            {EType.POISON,   NOT_VERY_EFFECTIVE},
            {EType.DRAGON,   SUPER_EFFECTIVE},
            {EType.DARK,     SUPER_EFFECTIVE},
            {EType.STEEL,    NOT_VERY_EFFECTIVE},
        });
    }



    public static float GetEffectivenessMultiplier(EType attackingType, EType DefendingType1, EType? DefendingType2 = null)
    {
        float multiplier = 1f;

        if (Instance.ttable[attackingType].ContainsKey(DefendingType1))
            multiplier *= Instance.ttable[attackingType][DefendingType1];

        if (DefendingType2 != null && Instance.ttable[attackingType].ContainsKey(DefendingType2.Value))
            multiplier *= Instance.ttable[attackingType][DefendingType2.Value];

        return multiplier;
    }

    public static void CheckEffectivity(EType attackingType, EType defendingType)
    {
        string effectivity = GetEffectivenessMultiplier(attackingType, defendingType)
               switch { 
                    0 => "has no effect",
                    0.5f => "is not very effective", 
                    1 => "is ok", 2 => "is super effective", 
                    _ => "[ERROR]" 
                };

        Debug.Log($"{attackingType.ToString()} {effectivity} against {defendingType.ToString()}");
    }


}
