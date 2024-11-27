using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    public RetrievePokeData retrievePokeData;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public void GeneratePlayerParty()
    {
        List<Pokemon> pokemonHolder = new List<Pokemon> ();   
        for (int i = 0; i < 3; i++)
        {
            int randomized_int = Random.Range(1, 11);
            retrievePokeData.callRetrievePokemon(randomized_int, i);        
        }

        if (retrievePokeData.request_finished)
        {
            for (int i = 0; i < 3; i++)
            {

                Pokemon newMon = new Pokemon();
                retrievePokeData.RandomGenerateSex(newMon);
                newMon.IV = retrievePokeData.RandomGenerateIVs();
                newMon.EV = retrievePokeData.RandomGenerateEVs();
                newMon.nature = retrievePokeData.RandomGenerateNature();
                newMon.data = retrievePokeData.pokeDataHolder[i];
                pokemonHolder.Add(newMon);
            }

            retrievePokeData.request_finished = false;
            retrievePokeData.pokeDataHolder.Clear();
        }        
    }



}
