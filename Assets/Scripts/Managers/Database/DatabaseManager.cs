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
        // generate pokemon_data
        Debug.Log("About to Get Pokemon Data...");
        for (int i = 0; i < partysize; i++)
        {
            int randomized_int = Random.Range(1, 11);
            //Debug.Log("randomzied int: " + randomized_int);
            StartCoroutine(RetrieveMon(randomized_int, i));        
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
                StartCoroutine(RetrieveStat(pokemonID, currIndex, data));
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
                    //StartCoroutine(RetrievePokeMovePool(pokemonID));

                }
            }
            else
            {
                Debug.LogError("Web Request for RetrievePokeData faled.");
            }
        }


    public IEnumerator RetrievePokeMovePool(int pokemonID)
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
                this.SelectMove(pokemonID);
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

    private void SelectMove(int pokemonID)
    {
        List<int> movePoolCopy = retrieveMoveData.movePoolIDs;
        List<int> selectedIDs = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, movePoolCopy.Count);
            selectedIDs.Add(movePoolCopy[randomIndex]); 
        }

        for (int i = 0; i < selectedIDs.Count; i++)
        {
            StartCoroutine(RetrievePokeMoveData(selectedIDs[i]));
        }

    }

    private IEnumerator RetrievePokeMoveData(int moveID)
    {
        WWWForm form = new WWWForm();
        form.AddField("moveID", moveID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move.php", form);
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
                retrieveMoveData.retrieveMoveData(retrieve_result);
            }
            else
            {
                Debug.Log("Retrieval of move pool failed.");    
            }
        }
        else
        {
            Debug.Log("Response: " + retrieve_req.error);
            Debug.LogError("Web Request for RetrievePokeData faled.");
            
        }

        //Debug.Log("PokeHolder Size: " + retrievePokeData.pokeDataHolder.Count);
    }


}



