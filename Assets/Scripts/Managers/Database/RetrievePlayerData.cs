using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RetrievePlayerData : MonoBehaviour
{
    public void sendToPlayerManager(string[] retrieve_result)
    {
        List<int> player_monIDs = new List<int>();
        foreach (string line in retrieve_result)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Debug.Log("Pokemon ID: " + line);
                player_monIDs.Add(int.Parse(line));
            }
        }

        if (player_monIDs.Count == 3)
        {

        }
    }

    public IEnumerator retrievePlayerMonData(int pokemonID)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_mon.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {

        }
        else
        {
            Debug.LogWarning("Web Request for RetrievePlayerData faled.");
        }

   
    }
}
