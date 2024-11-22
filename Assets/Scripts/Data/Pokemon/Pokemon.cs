using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pokemon 
{

    private int ownerID;
    public int OwnerID { get => ownerID; }

    public const int Level = 50;

    private Pokemon_Data data;

    public Pokemon_Data Data { get => data; }

    private Stat IV = new();
    private Stat EV = new();

    public Pokemon(int playerID, int pokemonData)
    {
        data = PokemonDataManager.RequestPokemon(pokemonData).Value;
        ownerID = playerID;
        GenerateIVS();
    }

    private void GenerateIVS()
    {
        IV.DoOnAll(t => {
            var keys = t.Keys.ToList();
            foreach (var key in keys)
                t[key] = Random.Range(0, 31);           
        });
    }

    public void PushToDatabase()
    {

    }

    public Stat GetTotalStat()
    {
        Stat total = new();

        total.DoOnAll(t => {

            var keys = t.Keys.ToList();

            foreach (var key in keys)
            {
                float Base = Data.baseStats.GetByEnum(key);
                float IV = this.IV.GetByEnum(key);
                float EV = this.EV.GetByEnum(key);

                float inner = 2 * Base + IV + (EV / 4) * Level;
                inner /= 100;

                if (key == EStatType.HEALTH)
                    total.SetStatByEnum(key, inner + Level + 10);
                else
                {
                    inner += 5;
                    //calculate nature
                }
            }

        });


        return null;
    }
}
