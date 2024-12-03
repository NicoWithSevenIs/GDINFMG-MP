using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.EventSystems.EventTrigger;

public class Pokemon_Battle_Instance 
{

    public const int LEVEL = 50;

    private Pokemon pokemon;
    public Pokemon Pokemon { get => pokemon; }

    private StatModHandler modHandler = new();
    public StatModHandler ModHandler { get => modHandler; }

    private Stat totalStats;
    public Stat TotalStats { get => totalStats;  }
    public Stat stat {  get => modHandler.ApplyMods(totalStats); }

    /*
    private EAilmentType? ailmentType = null;
    public EAilmentType? AilmentType { get => ailmentType; }
    */

    private float currentHP;
    public float CurrentHealth { get => currentHP; }
    public float HealthPercentage { get => currentHP / stat.Health; }
    public bool isFainted { get => currentHP <= 0; }



    private string ownerType;
    public string OwnerType { get => ownerType; }

    private string front_url;
    public string Front_Sprite_URL { get => front_url; }

    private string back_url;
    public string Back_Sprite_URL { get => back_url; }


    public Pokemon_Battle_Instance(Pokemon pokemon, string ownerType)
    {
        this.pokemon = pokemon;
        GetTotalStat();
        Debug.Log(pokemon.data.name + " Health: " + pokemon.data.baseStats.Health);
        currentHP = stat.Health;
        this.ownerType = ownerType;
    }

    public void SetSprite(string front, string back)
    {
        this.front_url = front;
        this.back_url = back;
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stat.Health);

        var p = new Dictionary<string, object>();
        p["Battler Name"] = ownerType;
        p["Active Pokemon"] = this;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_HEALTH_CHANGED, p);

    }

 
    private void GetTotalStat()
    {
        totalStats = new();


        totalStats.DoOnAll(t => {

            var keys = t.Keys.ToList();

            EStatType increasedByNature;
            EStatType decreasedByNature;

            Nature.GetNatureMultiplier(pokemon.nature, out increasedByNature, out decreasedByNature);

            if (pokemon.data.baseStats == null) Debug.Log("[ERROR]: STATS NULL! " +  pokemon.data.name??"data is null?");
            foreach (var key in keys)
            {

                float Base = pokemon.data.baseStats.GetByEnum(key);
                float IV = pokemon.IV.GetByEnum(key);
                float EV = pokemon.EV.GetByEnum(key);

                float inner = 2 * Base + IV + Mathf.Floor(EV / 4);
                inner *= LEVEL;
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
       out bool isACriticalStrike,
       int critRateMultiplier = 1
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


        List<ActionSequenceComponent> prompts = new();

        //STAB
        if (attacker.Pokemon.data.type1 == type || attacker.Pokemon.data.type2 == type)
            damage *= 1.5f;

        //CRIT

        int random = UnityEngine.Random.Range(Mathf.Min(critRateMultiplier, 24), 25);
    
        isACriticalStrike = random == 24;
        //isACriticalStrike = true;
        if (isACriticalStrike)
        {
            damage *= (2 * Pokemon_Battle_Instance.LEVEL + 5) / Pokemon_Battle_Instance.LEVEL + 5;

            var critComp = new ActionSequenceComponent(() => {
                var p = new Dictionary<string, object>();
                p["Message"] = $"It was a Critical Hit!";
                EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
            }, true);


            prompts.Add(critComp);
           
        }


        //Type Effectiveness

        float TypeMultiplier = TypeChecker.GetEffectivenessMultiplier(type, target.Pokemon.data.type1, target.Pokemon.data.type2);
        damage *= TypeMultiplier;

        if(TypeMultiplier != 1)
        {
            var typeComp = new ActionSequenceComponent(() => {
                var p = new Dictionary<string, object>();

                if(TypeMultiplier == 0)
                    p["Message"] = $"It had No Effect";
                else if (TypeMultiplier < 1)
                    p["Message"] = $"It's Not Very Effective";
                else if (TypeMultiplier > 1)
                    p["Message"] = $"It's Super Effective";

                EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
            }, true);

            prompts.Add(typeComp);
        }

        ActionSequencer.AddToSequenceFront(prompts, 0);


        //Random
        damage *= UnityEngine.Random.Range(85f, 100f) / 100f;

        /*
        //Burn
        if (attacker.AilmentType == EAilmentType.BURN)
            damage *= 0.5f;
        */

        return damage;
    }

}
