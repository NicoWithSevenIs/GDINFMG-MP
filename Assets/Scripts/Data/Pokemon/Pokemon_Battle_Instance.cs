using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pokemon_Battle_Instance 
{

    private Pokemon pokemon;
    public Pokemon Pokemon { get => pokemon; }

    private StatModHandler modHandler = new();
    private Stat totalStats;
    public Stat stat { 
        get => modHandler.ApplyMods(totalStats);
    }

    private EAilmentType? ailmentType = null;
    public EAilmentType? AilmentType { get => ailmentType; }


    public const int LEVEL = 50;
    private float currentHP;

    public float CurrentHealth { get => currentHP; }
    public bool isFainted { get => currentHP <= 0; }


    private string front_url;
    public string Front_Sprite_URL { get => front_url; }

    private string back_url;
    public string Back_Sprite_URL { get => back_url; }


    public Pokemon_Battle_Instance(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        GetTotalStat();
        currentHP = stat.Health;

    }

    public void SetSprite(string front, string back)
    {
        this.front_url = front;
        this.back_url = back;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_HEALTH_CHANGED);

        if (currentHP <= 0)
            EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT);
    }

 
    private void GetTotalStat()
    {
        totalStats = new();
        totalStats.DoOnAll(t => {

            var keys = t.Keys.ToList();

            EStatType increasedByNature;
            EStatType decreasedByNature;

            Nature.GetNatureMultiplier(pokemon.nature, out increasedByNature, out decreasedByNature);


            foreach (var key in keys)
            {
                float Base = pokemon.data.baseStats.GetByEnum(key);
                float IV = pokemon.IV.GetByEnum(key);
                float EV = pokemon.EV.GetByEnum(key);

                float inner = 2 * Base + IV + (EV / 4) * LEVEL;
                inner /= 100;

                if (key == EStatType.HEALTH)
                {
                    totalStats.SetStatByEnum(key, inner + LEVEL + 10);
                    continue;
                }

                inner += 5;

                if (increasedByNature != decreasedByNature)
                {
                    if (key == increasedByNature)
                        inner *= 1.1f;

                    if (key == decreasedByNature)
                        inner *= 0.9f;
                }
                
                totalStats.SetStatByEnum(key, inner);
            }

        });

    }

    public static float CalculateDamage(
       Pokemon_Battle_Instance attacker,
       Pokemon_Battle_Instance target,
       float power,
       EType type,
       EMoveType moveClass,
       out bool isACriticalStrike
   )
    {
        if (moveClass == EMoveType.STATUS)
        {
            isACriticalStrike = false;
            return 0f;
        }


        EStatType attackingStat = moveClass == EMoveType.PHYSICAL ? EStatType.ATTACK : EStatType.SPECIAL_ATTACK;
        EStatType defendingStat = moveClass == EMoveType.PHYSICAL ? EStatType.DEFENSE : EStatType.SPECIAL_DEFENSE;

        float A = attacker.stat.GetByEnum(attackingStat);
        float D = target.stat.GetByEnum(defendingStat);

        float levelMultiplier = 2 * (float)Pokemon_Battle_Instance.LEVEL / 5 + 2;
        float damage = levelMultiplier * power * (A / D) / 50 + 2;


        //STAB
        if (attacker.Pokemon.data.type1 == type || attacker.Pokemon.data.type2 == type)
            damage *= 1.5f;

        //CRIT
        isACriticalStrike = Random.Range(1, 25) == 24;
        if (isACriticalStrike)
            damage *= (2 * Pokemon_Battle_Instance.LEVEL + 5) / Pokemon_Battle_Instance.LEVEL + 5;

        //Type Effectiveness
        damage *= TypeChecker.GetEffectivenessMultiplier(type, target.Pokemon.data.type1, target.Pokemon.data.type2);

        //Random
        damage *= Random.Range(85f, 100f) / 100f;

        //Burn
        if (attacker.AilmentType == EAilmentType.BURN)
            damage *= 0.5f;


        return damage;
    }

}
