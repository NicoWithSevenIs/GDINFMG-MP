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

    private EAilmentType? ailemntType = null;
    public EAilmentType? AilmentType { get => ailemntType; }

    public const int LEVEL = 50;
    private float currentHP;

    public float CurrentHealth { get => currentHP; }

    public Pokemon_Battle_Instance(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        currentHP = stat.Health;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        //if(currentHP <= 0) Invoke On Death Event
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
    

}
