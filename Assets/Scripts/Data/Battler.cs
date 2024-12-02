using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int AvailablePokemon { get => party.Where(t => !t.isFainted).Count(); }

    public Pokemon_Battle_Instance GetPokemon(int index){
        index = Mathf.Clamp(index, 0, PARTY_SIZE - 1);
        return party[index];
    }

    public void SwitchPokemon(int index, bool performAtCall = true)
    {
        index = Mathf.Clamp(index, 0, PARTY_SIZE-1);
        activePokemonIndex = index;

        var p = new Dictionary<string, object>();
        p.Add("Battler Name", battlerName);
        p.Add("Active Pokemon", ActivePokemon);

        EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, p);

        var comp = new ActionSequenceComponent(() => {
            var p = new Dictionary<string, object>();
            p["Message"] = $"{battlerName} sent out {ActivePokemon.Pokemon.data.name}!";
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
        }, true);

        ActionSequencer.AddToSequenceFront(new() { comp }, 0);

        if(performAtCall)
            ActionSequencer.Perform();
       
    }

    public Battler(string battlerName, List<Pokemon> mons)
    {
        party = new Pokemon_Battle_Instance[3];
        for (int i = 0; i < PARTY_SIZE; i++)
            party[i] = new Pokemon_Battle_Instance(mons[i], battlerName);

        this.battlerName = battlerName;
    }

    
}
