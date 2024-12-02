using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Pokemon 
{
    public int ownerID;
    public Pokemon_Data data;

    public ESex sex;
    public string nature;
    public Stat IV;
    public Stat EV;

    public int[] moveSet;

    /*
    public Pokemon(int playerID, int pokemonData, ESex sex, Stat IV, Stat EV, string nature)
    {
        data = PokemonDataManager.RequestPokemon(pokemonData).Value;
        ownerID = playerID;
        this.IV = IV;
        this.EV = EV;
        this.nature = nature;
        this.sex = sex;
    }
    */

    public Pokemon(int playerID, Pokemon_Data data, ESex sex, Stat IV, Stat EV, string nature, int[] moveSet)
    {
        this.data = data;
        ownerID = playerID;
        this.IV = IV;
        this.EV = EV;
        this.nature = nature;
        this.sex = sex;

        this.moveSet = new int[4];
        Array.Copy(moveSet, this.moveSet, 4);
    }


}
