using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    public RetrievePokeData retrievePokeData;
    public RetrieveMoveData retrieveMoveData;
    public RetrievePlayerData retrievePlayerData;

    public UIHandler uiHandler;

    
    public List<Pokemon> EnemyMons = new List<Pokemon>();

    public int partysize;
    public int debug_num;
    public int pokemon_db_list_size;

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

        PlayerManager.initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            foreach (var pokemon in Enemy.Instance.enemyMons)
            {
                if (pokemon.data.baseStats == null) Debug.Log("enemy: [ERROR]: NULL: " + pokemon.data.name);
                else
                    pokemon.data.baseStats.DoOnAll(t =>
                    {
                        string s = pokemon.data.name;
                        foreach (var entry in t)
                        {
                            s += $"{entry.Key.ToString()}: {entry.Value} | ";

                        }

                        Debug.Log(s);


                    });
            }

        if (Input.GetKeyDown(KeyCode.N))
            foreach (var pokemon in PlayerManager.party)
            {
                if (pokemon.data.baseStats == null) Debug.Log("enemy: [ERROR]: NULL: " + pokemon.data.name);
                else
                    pokemon.data.baseStats.DoOnAll(t =>
                    {
                        string s = pokemon.data.name;
                        foreach (var entry in t)
                        {
                            s += $"{entry.Key.ToString()}: {entry.Value} | ";

                        }

                        Debug.Log(s);


                    });
            }
    }


    public void StartPlayerPartyGenerator()
    {
        StartCoroutine(GetMaxPokemonCountForPlayer());
    }

    public IEnumerator GetMaxPokemonCountForPlayer()
    {
        UnityWebRequest retrieve_req = UnityWebRequest.Get("http://localhost/get_pokemon_count.php");
        yield return retrieve_req.SendWebRequest();
        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                this.pokemon_db_list_size = int.Parse(retrieve_result[1]);
                //Debug.Log("pokemon db list size: " + this.pokemon_db_list_size);
                this.GeneratePlayerParty();
            }
            else
            {
                Debug.Log("Success wasn't the first text thrown.");
            }
        }
        else
        {
            Debug.LogError("GetMaxPokemonCount Failed.");
        }

    }

    public void GeneratePlayerParty()
    {   
        if (retrievePokeData.pokemonHolder.Count != 0)
        {
            retrievePokeData.pokemonHolder.Clear();
            retrievePokeData.pokeDataHolder.Clear();
            retrieveMoveData.clearLists();
        }

        List<int> usedIndices = new List<int>();
        List<int> selectedIndices = new List<int>();
        for (int i = 0; i < partysize; i++)
        {
            int randomized_int;
            do
            {
                randomized_int = Random.Range(1, this.pokemon_db_list_size);
                //Debug.Log("Gen Party i value: " + i);
                //Debug.Log("Gen Party Random Int: " + randomized_int);
            } while (usedIndices.Contains(randomized_int));
            usedIndices.Add(randomized_int);
            selectedIndices.Add(randomized_int);
        }

        StartCoroutine(GenerateMon(selectedIndices, 0));
    }

    private IEnumerator GenerateMon(List<int> selectedIndices, int index)
    {
        if (index < selectedIndices.Count)
        {   
            int pokemonID = selectedIndices[index];
            Debug.Log("Starting RetrieveMon for pokemonID: " + pokemonID);

            yield return StartCoroutine(RetrieveMon(pokemonID, index));

            yield return StartCoroutine(GenerateMon(selectedIndices, index + 1));
        }
        else
        {
            Debug.Log("Generated Pokemons: " + retrievePokeData.pokemonHolder.Count);
            Debug.Log("All Pokemon have been processed.");
            uiHandler.openGenerateDonePrompt();
        }
    }

    public void GenerateEnemyParty()
    {
        if (retrievePokeData.pokemonHolder.Count != 0)
        {
            Debug.Log("GenerateEnemyParty enemyMons count: " + Enemy.Instance.enemyMons);
            retrievePokeData.pokemonHolder.Clear();
            retrievePokeData.pokeDataHolder.Clear();
            retrieveMoveData.clearLists();
        }

        List<int> usedIndices = new List<int>();
        List<int> selectedIndices = new List<int>();
        for (int i = 0; i < partysize; i++)
        {
            int randomized_int;
            do
            {
                randomized_int = Random.Range(1, 11);
                //Debug.Log("Gen Party i value: " + i);
                //Debug.Log("Gen Party Random Int: " + randomized_int);
            } while (usedIndices.Contains(randomized_int));
            usedIndices.Add(randomized_int);
            selectedIndices.Add(randomized_int);
        }

        StartCoroutine(GenerateEnemyMon(selectedIndices, 0));
    }

    private IEnumerator GenerateEnemyMon(List<int> selectedIndices, int index)
    {
        if (index < selectedIndices.Count)
        {
            int pokemonID = selectedIndices[index];
            Debug.Log("Starting RetrieveMon for pokemonID: " + pokemonID);

            yield return RetrieveMon(pokemonID, index);

            yield return GenerateEnemyMon(selectedIndices, index + 1);
        }
        else
        {
            Debug.Log("Generated Pokemons: " + retrievePokeData.pokemonHolder.Count);
            Debug.Log("All Pokemon have been processed.");
            Debug.Log("RetrievePokeData PokemonHolder at GenEnemyMon: " + retrievePokeData.pokemonHolder.Count);

            foreach (Pokemon refMon in retrievePokeData.pokemonHolder)
            {
                Pokemon newMon = new Pokemon();
                newMon = refMon;
                Enemy.Instance.enemyMons.Add(newMon);   
                EnemyMons.Add(newMon);
            }
            //foreach (var pokemon in retrievePokeData.pokemonHolder)
            //{
            //    if (pokemon.data.baseStats == null) Debug.Log("enemy: [ERROR]: NULL: " + pokemon.data.name);
            //    else
            //        pokemon.data.baseStats.DoOnAll(t =>
            //        {
            //            string s = pokemon.data.name;
            //            foreach (var entry in t)
            //            {
            //                s += $"{entry.Key.ToString()}: {entry.Value} | ";

            //            }

            //            Debug.Log(s);


            //        });
            //}
            Debug.Log("Enemy.Instance.enemyMons: " + Enemy.Instance.enemyMons.Count);

        }
    }

    private IEnumerator RetrieveMon(int pokemonID, int currIndex)
    {
        WWWForm form = new WWWForm();
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_mon.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                Pokemon_Data data = retrievePokeData.retrievePokeData1(retrieve_result);
                yield return StartCoroutine(RetrieveStat(pokemonID, currIndex, data));
            }
        }       
        else
        {
            Debug.LogError("Retrieve Mon Failed.");
        }
    }

    private IEnumerator RetrieveStat(int pokemonID, int currIndex, Pokemon_Data refData)
    {
            WWWForm form = new WWWForm();
            form.AddField("pokemonID", pokemonID);

            UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_stat.php", form);
            yield return retrieve_req.SendWebRequest();

            if (retrieve_req == null)
            {
                Debug.LogError("Retrieve_Req is null.");
            }

            if (retrieve_req.result == UnityWebRequest.Result.Success)
            {
                string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
                if (retrieve_result[0].Contains("Success"))
                {
                retrievePokeData.retrievePokeData2(retrieve_result, refData);
                retrievePokeData.generatePokemonWithNoMoves(retrievePokeData.pokeDataHolder[currIndex]);
                //foreach(var pokemon in retrievePokeData.pokemonHolder)
                //{
                //    pokemon.data.baseStats.DoOnAll(t => {
                //        string s = "AFTER GENERATE MON WITH NO MOVES: " + pokemon.data.name;
                //        foreach (var entry in t)
                //        {
                //            s += $"{entry.Key.ToString()}: {entry.Value} | ";

                //        }

                //        Debug.Log(s);


                //    });
                //}

                yield return StartCoroutine(RetrievePokeMovePool(pokemonID, currIndex));

                }
            }
            else
            {
                Debug.LogError("Web Request for RetrieveStat faled.");
            }
        }


    public IEnumerator RetrievePokeMovePool(int pokemonID, int currentIndex)
    {
        WWWForm form = new WWWForm();
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move_pool.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Retrieve_Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                retrieveMoveData.retrieveMovePool(retrieve_result);
                yield return StartCoroutine(SelectMove(pokemonID, currentIndex));
            }
            else
            {
                Debug.Log("Retrieval of move pool failed.");
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");
        }
    }

    private IEnumerator SelectMove(int pokemonID, int currentIndex)
    {
        List<int> movePoolCopy = retrieveMoveData.movePoolIDs;
        List<int> usedIndices = new List<int>();
        List<int> selectedIDs = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, movePoolCopy.Count);
            } while (usedIndices.Contains(randomIndex));    
            usedIndices.Add(randomIndex);
            selectedIDs.Add(movePoolCopy[randomIndex]);
        }

        //Debug.Log("Pokemon Name: " + retrievePokeData.pokemonHolder[currentIndex].data.name);
        //Debug.Log("Move Pool Copy size: " + movePoolCopy.Count);

        //for (int i = 0; i < selectedIDs.Count; i++)
        //{
        //    Debug.Log("selected ids: " + selectedIDs[i]);
        //}

        for(int j = 0; j < retrievePokeData.pokemonHolder[currentIndex].moveSet.Length; j++)
        {
            retrievePokeData.pokemonHolder[currentIndex].moveSet[j] = selectedIDs[j];
            //Debug.Log("check pokemon holder move id data: index " + j + ": " + retrievePokeData.pokemonHolder[currentIndex].moveSet[j]);
        }

        for (int i = 0; i < selectedIDs.Count; i++)
        {
            yield return StartCoroutine(RetrievePokeMoveData(selectedIDs[i]));
        }
    }

    public IEnumerator RetrievePokeMoveData(int moveID)
    {
        WWWForm form = new WWWForm();
        form.AddField("moveID", moveID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                retrieveMoveData.retrieveMoveData(retrieve_result, moveID);
                retrieveMoveData.clearLists();               
            }
            else
            {
                Debug.Log("Retrieval of move pool failed.");    
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");
            
        }
        
    }

    public void callSendBackData ()
    {
        if (retrievePokeData.pokemonHolder.Count != 0)
        {
            StartCoroutine(SendBackData());
        }
        else
        {
            Debug.Log("Not enough party members!");
        }
       
    }

    public IEnumerator SendBackData()
    {
        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[0]));
        Debug.Log("Finished 0 Index");

        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[1]));
        Debug.Log("Finished 1 Index");

        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[2]));
        Debug.Log("Finished 2 Index");

        yield return StartCoroutine(SendToPokemonIVDetails(retrievePokeData.pokemonHolder[0]));
        Debug.Log("Finished 0 IV");

        yield return StartCoroutine(SendToPokemonIVDetails(retrievePokeData.pokemonHolder[1]));
        Debug.Log("Finished 1 IV");

        yield return StartCoroutine(SendToPokemonIVDetails(retrievePokeData.pokemonHolder[2]));
        Debug.Log("Finished 2 IV");

        Debug.Log("clear all the holders");

        retrievePokeData.pokemonHolder.Clear();
        retrievePokeData.pokeDataHolder.Clear();
        retrieveMoveData.clearLists();

    }

    private IEnumerator SendToPokeDetails(Pokemon pokemon)
    {   
        WWWForm form = new WWWForm();
        form.AddField("playerID", pokemon.ownerID);
        form.AddField("pokemonID", pokemon.data.id);
        form.AddField("pokemonGender", pokemon.sex.ToString());
        form.AddField("pokemonNature", pokemon.nature);
        form.AddField("moveID1", pokemon.moveSet[0]);
        form.AddField("moveID2", pokemon.moveSet[1]);
        form.AddField("moveID3", pokemon.moveSet[2]);
        form.AddField("moveID4", pokemon.moveSet[3]);


        UnityWebRequest send_req = UnityWebRequest.Post("http://localhost/send_to_pokedetails.php", form);
        yield return send_req.SendWebRequest();

    }

    private IEnumerator SendToPokemonIVDetails(Pokemon pokemon)
    {
        WWWForm form = new WWWForm();
        form.AddField("hpIV", pokemon.IV.Health.ToString());
        form.AddField("atkIV", pokemon.IV.Attack.ToString());
        form.AddField("sp_atkIV", pokemon.IV.Special_Attack.ToString());
        form.AddField("defIV", pokemon.IV.Defense.ToString());
        form.AddField("sp_defIV", pokemon.IV.Special_Defense.ToString());
        form.AddField("speedIV", pokemon.IV.Speed.ToString());
        

        UnityWebRequest send_req = UnityWebRequest.Post("http://localhost/send_to_pokemon_ivs.php", form);
        yield return send_req.SendWebRequest();

        if (send_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = send_req.downloadHandler.text.Split('\t');
            Debug.Log(retrieve_result[0]);
        }
        else
        {
            Debug.LogError("Web Request for SendToPokemonIVDetails faled.");

        }
    }

    public void callGetPlayerData()
    {
        StartCoroutine(GetPlayerData());
    }

    private IEnumerator GetPlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", PlayerManager.playerID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_player.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\n');
            
        }
        else
        {
            Debug.LogWarning("Web Request for GetPlayerData faled.");
        }

    }

    private IEnumerator SendPlayerData(float instanceID1, float instanceID2, float instanceID3)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", PlayerManager.playerID);
        form.AddField("currentFloor", PlayerManager.currentFloor);

        Debug.Log("player id: " + PlayerManager.playerID);
        Debug.Log("currentFloor: " + PlayerManager.currentFloor);

        form.AddField("instanceID1", instanceID1.ToString());
        form.AddField("instanceID2", instanceID2.ToString());
        form.AddField("instanceID3", instanceID3.ToString());

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/send_to_player.php", form);
        yield return retrieve_req.SendWebRequest();

      
        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            //retrievePlayerData.chosenInstances.Clear();
            //string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\n');

        }
        else
        {
            Debug.LogWarning("Web Request for SendPlayerData faled.");
        }
    }

    public void callReshuffle()
    {
        StartCoroutine(ReshuffleParty());
    }

    private IEnumerator ReshuffleParty()
    {
        yield return StartCoroutine(retrievePlayerData.GetDBInstances());

        debug_num = 0;
        Debug.Log("debug num: " + debug_num);
        yield return StartCoroutine(ReshuffleMons(retrievePlayerData.chosenInstances[0]));

        debug_num++;
        Debug.Log("debug num: " + debug_num);
        yield return StartCoroutine(ReshuffleMons(retrievePlayerData.chosenInstances[1]));

        debug_num++;
        Debug.Log("debug num: " + debug_num);
        yield return StartCoroutine(ReshuffleMons(retrievePlayerData.chosenInstances[2]));

        //Debug.Log("size: " + retrievePlayerData.chosenInstances.Count);
        yield return StartCoroutine(SendPlayerData(retrievePlayerData.chosenInstances[0], retrievePlayerData.chosenInstances[1], retrievePlayerData.chosenInstances[2]));

        retrievePlayerData.chosenInstances.Clear();
        retrievePlayerData.dbInstanceID_list.Clear();
    }

    private IEnumerator ReshuffleMons(float randomInstanceID)
    {
        Debug.Log("Random instance: " + randomInstanceID);
        WWWForm form = new WWWForm();
        form.AddField("randomInstanceID", randomInstanceID.ToString());

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/reshuffle_player_mons.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            Debug.Log("result length: " + retrieve_result.Length);
            //foreach (string s in retrieve_result)
            //{
            //    Debug.Log(s);
            //}
            retrievePlayerData.sendToPlayerManager(retrieve_result);

        }
        else
        {
            Debug.LogWarning("Web Request for RetrievePlayerData faled.");
        }
    }

}



