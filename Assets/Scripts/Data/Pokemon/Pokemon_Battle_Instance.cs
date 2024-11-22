using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon_Battle_Instance 
{

    private Pokemon pokemon;
    private Stat totalStats;
    private StatModHandler modHandler = new();

    public Stat stat { 
        get => modHandler.ApplyMods(totalStats);
    }

    private float currentHP;

    public float CurrentHealth { get => currentHP; }

    public Pokemon_Battle_Instance(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        totalStats = pokemon.GetTotalStat();
        currentHP = stat.Health;
    }

    public void TakeDamage(float damage)
    {

    }


}
