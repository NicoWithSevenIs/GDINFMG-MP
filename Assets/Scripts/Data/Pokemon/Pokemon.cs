using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Pokemon 
{
    public int ownerID;
    public Pokemon_Data data;

    public string nature;
    public Stat IV;
    public Stat EV;

    public Pokemon(int playerID, int pokemonData, Stat IV, Stat EV, string nature)
    {
        data = PokemonDataManager.RequestPokemon(pokemonData).Value;
        ownerID = playerID;
        this.IV = IV;
        this.EV = EV;
        this.nature = nature;
    }


  
}
