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

    private Dictionary<Type, Dictionary<Type, float>> ttable = new();

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

        void AddTypeEffectiveness(Type Attacker, Dictionary<Type, float> typeInteractions) => ttable[Attacker] = typeInteractions;  
        

        AddTypeEffectiveness(Type.NORMAL, new(){
            {Type.ROCK,     NOT_VERY_EFFECTIVE},
            {Type.GHOST,    NO_EFFECT},
            {Type.STEEL,    NOT_VERY_EFFECTIVE}
        });

        AddTypeEffectiveness(Type.FIRE, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.WATER,    NOT_VERY_EFFECTIVE},
            {Type.GRASS,    SUPER_EFFECTIVE},
            {Type.ICE,      SUPER_EFFECTIVE},
            {Type.BUG,      SUPER_EFFECTIVE},
            {Type.ROCK,     NOT_VERY_EFFECTIVE},
            {Type.DRAGON,   NOT_VERY_EFFECTIVE},
            {Type.STEEL,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.WATER, new() {
            {Type.FIRE,     SUPER_EFFECTIVE},
            {Type.WATER,    NOT_VERY_EFFECTIVE},
            {Type.GRASS,    NOT_VERY_EFFECTIVE},
            {Type.GROUND,   SUPER_EFFECTIVE},
            {Type.ROCK,     SUPER_EFFECTIVE},
            {Type.DRAGON,   NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.ELECTRIC, new() {
            {Type.WATER,    SUPER_EFFECTIVE},
            {Type.ELECTRIC, NOT_VERY_EFFECTIVE},
            {Type.GRASS,    NOT_VERY_EFFECTIVE},
            {Type.GROUND,   NO_EFFECT},
            {Type.FLYING,   SUPER_EFFECTIVE},
            {Type.DRAGON,   NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.GRASS, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.WATER,    SUPER_EFFECTIVE},
            {Type.GRASS,    NOT_VERY_EFFECTIVE},
            {Type.POISON,   NOT_VERY_EFFECTIVE},
            {Type.GROUND,   SUPER_EFFECTIVE},
            {Type.FLYING,   NOT_VERY_EFFECTIVE},
            {Type.BUG,      NOT_VERY_EFFECTIVE},
            {Type.ROCK,     SUPER_EFFECTIVE},
            {Type.DRAGON,   NOT_VERY_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });


        AddTypeEffectiveness(Type.ICE, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.WATER,    NOT_VERY_EFFECTIVE},
            {Type.GRASS,    SUPER_EFFECTIVE},
            {Type.ICE,      NOT_VERY_EFFECTIVE},
            {Type.GROUND,   SUPER_EFFECTIVE},
            {Type.FLYING,   SUPER_EFFECTIVE},
            {Type.DRAGON,   SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.FIGHTING, new() {
            {Type.NORMAL,   SUPER_EFFECTIVE},
            {Type.ICE,      SUPER_EFFECTIVE},
            {Type.POISON,   NOT_VERY_EFFECTIVE},
            {Type.FLYING,   NOT_VERY_EFFECTIVE},
            {Type.PSYCHIC,  NOT_VERY_EFFECTIVE},
            {Type.BUG,      NOT_VERY_EFFECTIVE},
            {Type.ROCK,     SUPER_EFFECTIVE},
            {Type.GHOST,    NO_EFFECT},
            {Type.DARK,     SUPER_EFFECTIVE},
            {Type.STEEL,    SUPER_EFFECTIVE},
            {Type.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.POISON, new() {
            {Type.GRASS,    SUPER_EFFECTIVE},
            {Type.POISON,   NOT_VERY_EFFECTIVE},
            {Type.GROUND,   NOT_VERY_EFFECTIVE},
            {Type.ROCK,     NOT_VERY_EFFECTIVE},
            {Type.GHOST,    NOT_VERY_EFFECTIVE},
            {Type.STEEL,    NO_EFFECT},
            {Type.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.GROUND, new() {
            {Type.FIRE,     SUPER_EFFECTIVE},
            {Type.ELECTRIC, SUPER_EFFECTIVE},
            {Type.GRASS,    NOT_VERY_EFFECTIVE},
            {Type.POISON,   SUPER_EFFECTIVE},
            {Type.FLYING,   NO_EFFECT},
            {Type.BUG,      NOT_VERY_EFFECTIVE},
            {Type.ROCK,     SUPER_EFFECTIVE},
            {Type.STEEL,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.FLYING, new() {
            {Type.ELECTRIC, NOT_VERY_EFFECTIVE},
            {Type.GRASS,    SUPER_EFFECTIVE},
            {Type.FIGHTING, SUPER_EFFECTIVE},
            {Type.BUG,      SUPER_EFFECTIVE},
            {Type.ROCK,     NOT_VERY_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.PSYCHIC, new() {
            {Type.FIGHTING, SUPER_EFFECTIVE},
            {Type.POISON,   SUPER_EFFECTIVE},
            {Type.PSYCHIC,  NOT_VERY_EFFECTIVE},
            {Type.DARK,     NO_EFFECT},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.BUG, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.GRASS,    SUPER_EFFECTIVE},
            {Type.FIGHTING, NOT_VERY_EFFECTIVE},
            {Type.POISON,   NOT_VERY_EFFECTIVE},
            {Type.FLYING,   NOT_VERY_EFFECTIVE},
            {Type.PSYCHIC,  SUPER_EFFECTIVE},
            {Type.GHOST,    NOT_VERY_EFFECTIVE},
            {Type.DARK,     SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
            {Type.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.ROCK, new() {
            {Type.FIRE,     SUPER_EFFECTIVE},
            {Type.ICE,      SUPER_EFFECTIVE},
            {Type.FIGHTING, NOT_VERY_EFFECTIVE},
            {Type.GROUND,   NOT_VERY_EFFECTIVE},
            {Type.FLYING,   SUPER_EFFECTIVE},
            {Type.BUG,      SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.GHOST, new() {
            {Type.NORMAL,   NO_EFFECT},
            {Type.PSYCHIC,  SUPER_EFFECTIVE},
            {Type.GHOST,    SUPER_EFFECTIVE},
            {Type.DARK,     NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.DRAGON, new() {
            {Type.DRAGON,   SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
            {Type.FAIRY,    NO_EFFECT},
        });

        AddTypeEffectiveness(Type.DARK, new() {
            {Type.FIGHTING, NOT_VERY_EFFECTIVE},
            {Type.PSYCHIC,  SUPER_EFFECTIVE},
            {Type.GHOST,    SUPER_EFFECTIVE},
            {Type.DARK,     NOT_VERY_EFFECTIVE},
            {Type.FAIRY,    NOT_VERY_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.STEEL, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.WATER,    NOT_VERY_EFFECTIVE},
            {Type.ELECTRIC, NOT_VERY_EFFECTIVE},
            {Type.ICE,      SUPER_EFFECTIVE},
            {Type.BUG,      SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
            {Type.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.STEEL, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.WATER,    NOT_VERY_EFFECTIVE},
            {Type.ELECTRIC, NOT_VERY_EFFECTIVE},
            {Type.ICE,      SUPER_EFFECTIVE},
            {Type.BUG,      SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
            {Type.FAIRY,    SUPER_EFFECTIVE},
        });

        AddTypeEffectiveness(Type.STEEL, new() {
            {Type.FIRE,     NOT_VERY_EFFECTIVE},
            {Type.FIGHTING, SUPER_EFFECTIVE},
            {Type.POISON,   NOT_VERY_EFFECTIVE},
            {Type.DRAGON,   SUPER_EFFECTIVE},
            {Type.DARK,     SUPER_EFFECTIVE},
            {Type.STEEL,    NOT_VERY_EFFECTIVE},
        });
    }



    public static float GetEffectivenessMultiplier(Type attackingType, Type DefendingType1, Type? DefendingType2 = null)
    {
        float multiplier = 1f;

        if (Instance.ttable[attackingType].ContainsKey(DefendingType1))
            multiplier *= Instance.ttable[attackingType][DefendingType1];

        if (DefendingType2 != null && Instance.ttable[attackingType].ContainsKey(DefendingType2.Value))
            multiplier *= Instance.ttable[attackingType][DefendingType2.Value];

        return multiplier;
    }

    public static void CheckEffectivity(Type attackingType, Type defendingType)
    {

        string effectivity = GetEffectivenessMultiplier(attackingType, defendingType)
               switch { 
                    0 => "no effect",
                    0.5f => "not very effective", 
                    1 => "OK", 2 => "super effective", 
                    _ => "[ERROR]" 
                };

        Debug.Log($"{attackingType.ToString()} { effectivity} against {defendingType.ToString()}");
    }


}
