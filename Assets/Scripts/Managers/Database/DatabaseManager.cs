using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    public RetrievePokeData retrievePokeData;
    public RetrieveMoveData retrieveMoveData;
    public int partysize;
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
        List<Pokemon> pokemonHolder = new List<Pokemon>();
        Dictionary<int, int[]> movePools = new Dictionary<int, int[]>();
        // generate pokemon_data
        Debug.Log("About to Get Pokemon Data...");
        for (int i = 0; i < partysize; i++)
        {
            int randomized_int = Random.Range(1, 11);
            retrievePokeData.callRetrievePokemon(randomized_int, i);        
        }

        //fill up pokemon information excluding moveset and player id
        if (retrievePokeData.request_finished)
        {
            for (int i = 0; i < partysize; i++)
            {

                Pokemon newMon = new Pokemon();
                retrievePokeData.RandomGenerateSex(newMon);
                newMon.IV = retrievePokeData.RandomGenerateIVs();
                newMon.EV = retrievePokeData.RandomGenerateEVs();
                newMon.nature = retrievePokeData.RandomGenerateNature();
                newMon.data = retrievePokeData.pokeDataHolder[i];
                pokemonHolder.Add(newMon);
            }

            retrievePokeData.pokeDataHolder.Clear();

            // fill up moveset
            Debug.Log("About to retrieve move pool...");
            for (int i = 0; i < partysize; i++)
            {
                Debug.Log("Pokemon name: " + pokemonHolder[i].data.name);
                int mon_id = pokemonHolder[i].data.id;
                retrieveMoveData.callRetrieveMovePool(mon_id);
                //retrieveMoveData.resetMoveIDs();
            }
        }

        //if (retrieveMoveData.hasRetrievedMovePool)
        //{
        //    //for (int i = 0; i < partysize; i++)
        //    //{
        //    //    int max_size = movePools.Count + 1;
        //    //    for (int j = 0; j < 4; j++)
        //    //    {
        //    //        int randomIndex = Random.Range(0, max_size);
        //    //        retrieveMoveData.callRetrieveMove(movePools[i][randomIndex]);
        //    //    }
        //    //}
        //}

    }


}

