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
                randomized_int = Random.Range(1, 11);
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
                    retrievePokeData.generatePokemonWithNoMoves(refData);
                    yield return StartCoroutine(RetrievePokeMovePool(pokemonID, currIndex));

                }
            }
            else
            {
                Debug.LogError("Web Request for RetrievePokeData faled.");
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

        Debug.Log("Pokemon Name: " + retrievePokeData.pokemonHolder[currentIndex].data.name);
        Debug.Log("Move Pool Copy size: " + movePoolCopy.Count);

        for (int i = 0; i < selectedIDs.Count; i++)
        {
            Debug.Log("selected ids: " + selectedIDs[i]);
        }


        
  
        for(int j = 0; j < retrievePokeData.pokemonHolder[currentIndex].moveSet.Length; j++)
        {
            retrievePokeData.pokemonHolder[currentIndex].moveSet[j] = selectedIDs[j];
            Debug.Log("check pokemon holder move id data: index " + j + ": " + retrievePokeData.pokemonHolder[currentIndex].moveSet[j]);
        }

        for (int i = 0; i < selectedIDs.Count; i++)
        {
            yield return StartCoroutine(RetrievePokeMoveData(selectedIDs[i]));
        }
    }

    private IEnumerator RetrievePokeMoveData(int moveID)
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
        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[0], 0));

        Debug.Log("Finished 0 Index");

        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[1], 1));

        Debug.Log("Finished 1 Index");

        yield return StartCoroutine(SendToPokeDetails(retrievePokeData.pokemonHolder[2], 2));

        Debug.Log("Finished 2 Index");

        Debug.Log("clear all the holders");

        retrievePokeData.pokemonHolder.Clear();
        retrievePokeData.pokeDataHolder.Clear();
        retrieveMoveData.clearLists();
    }

    private IEnumerator SendToPokeDetails(Pokemon pokemon, int partyMemberNum)
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
        form.AddField("partyMemberNum", partyMemberNum);


        UnityWebRequest send_req = UnityWebRequest.Post("http://localhost/send_to_pokedetails.php", form);
        yield return send_req.SendWebRequest();


        if (send_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (send_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = send_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                Debug.Log("Send Req back!");
            }
            else
            {
                Debug.Log("Response: " + send_req.downloadHandler.text);
                Debug.LogWarning("Send data failed.");
            }
        }
        else
        {
            Debug.Log("Error: " + send_req.error + " Party Num: " + partyMemberNum);
            Debug.Log("Response: " + send_req.downloadHandler.text);
            Debug.LogWarning("Web Request for SendToPokeDetails faled.");

        }
    }

}


