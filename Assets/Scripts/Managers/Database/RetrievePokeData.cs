using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TreeEditor;
using UnityEngine;
using UnityEngine.Networking;
using static Unity.Burst.Intrinsics.X86;

public class RetrievePokeData : MonoBehaviour
{
    public DB_Utility_Helper utilHelper;
    public List<Pokemon_Data> pokeDataHolder = new List<Pokemon_Data>();
    public List<Pokemon> pokemonHolder = new List<Pokemon>();

    public Pokemon_Data retrievePokeData1(string[] retrieve_result)
    {
        Pokemon_Data data = new Pokemon_Data();
        data.id = int.Parse(retrieve_result[1]);
        data.name = retrieve_result[2];
        data.type1 = utilHelper.getDecipheredType1(retrieve_result[3]);
        data.type2 = utilHelper.getDecipheredType2(retrieve_result[4]);
        data.weight = float.Parse(retrieve_result[5]);
        data.height = float.Parse(retrieve_result[6]);
        data.spriteID = int.Parse(retrieve_result[7]);
        return data;
    }

    public void retrievePokeData2(string[] retrieve_result, Pokemon_Data refData)
    {
        float hp = float.Parse(retrieve_result[1]);
        float attack = float.Parse(retrieve_result[2]);
        float special_attack = float.Parse(retrieve_result[3]);
        float defense = float.Parse(retrieve_result[4]);
        float special_defense = float.Parse(retrieve_result[5]);
        float speed = float.Parse(retrieve_result[6]);

        Pokemon_Data newData =
        new Pokemon_Data(refData.id, refData.spriteID, refData.name,
        refData.type1, refData.type2,
                         new Stat(hp, attack, defense, special_attack, special_defense, speed),
                         refData.weight, refData.height);
        this.pokeDataHolder.Add(newData);
        this.printPokeData();
        this.printPokeStats();
    }

    public void generatePokemonWithNoMoves(Pokemon_Data data)
    {
        Pokemon pokemon = new Pokemon();
        pokemon.data = data;
        pokemon.sex = utilHelper.RandomGenerateSex();
        pokemon.IV = utilHelper.RandomGenerateIVs();
        pokemon.EV = utilHelper.RandomGenerateEVs();
        pokemon.nature = utilHelper.RandomGenerateNature(); 
        this.pokemonHolder.Add(pokemon);    
    
    }

    private void printPokeData()
    {
        //Debug.Log("Pokemon Data Holder Size: " + pokeDataHolder.Count
        foreach (Pokemon_Data data in pokeDataHolder)
        {
            Debug.Log("Pokemon ID: " + data.id);
            Debug.Log("Pokemon Name: " + data.name);
            Debug.Log("Pokemon Type 1: " + data.type1);
            Debug.Log("Pokemon Type 2: " + data.type2);
            Debug.Log("Pokemon Weight: " + data.weight);
            Debug.Log("Pokemon Height: " + data.height);
            Debug.Log("Pokemon Sprite ID: " + data.spriteID);
        }
       
    }

    private void printPokeStats()
    {
        foreach (Pokemon_Data data in pokeDataHolder)
        {
            Debug.Log("Pokemon HP: " + data.baseStats.Health);
            Debug.Log("Pokemon ATK: " + data.baseStats.Attack);
            Debug.Log("Pokemon SP.ATK: " + data.baseStats.Special_Attack);
            Debug.Log("Pokemon DEF: " + data.baseStats.Defense);
            Debug.Log("Pokemon SP.DEF: " + data.baseStats.Special_Defense);
            Debug.Log("Pokemon SPEED: " + data.baseStats.Speed);
        }
       
    }
}




