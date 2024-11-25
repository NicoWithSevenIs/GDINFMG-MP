using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler
{
    public const int PARTY_SIZE =3 ;
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";


    private string battlerName;

    private Pokemon_Battle_Instance[] party;
    public Pokemon_Battle_Instance[] Party { get => party; }

    private int activePokemonIndex = 0;
    public int ActivePokemonIndex { get => activePokemonIndex; }
    public Pokemon_Battle_Instance ActivePokemon { get =>  party[activePokemonIndex]; }

    public Pokemon_Battle_Instance GetPokemon(int index){
        index = Mathf.Clamp(index, 0, PARTY_SIZE - 1);
        return party[index];
    }

    public void SwitchPokemon(int index)
    {
        index = Mathf.Clamp(index, 0, PARTY_SIZE-1);
        activePokemonIndex = index;

        var p = new Dictionary<string, object>();
        p.Add("Battler Name", battlerName);
        p.Add("Active Pokemon", ActivePokemon);

        EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, p);
    }

    public Battler(string battlerName, List<Pokemon> mons)
    {
        party = new Pokemon_Battle_Instance[3];
        for (int i = 0; i < PARTY_SIZE; i++)
            party[i] = new Pokemon_Battle_Instance(mons[i]);

        this.battlerName = battlerName;
    }

    
}
