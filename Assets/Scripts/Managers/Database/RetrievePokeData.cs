using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RetrievePokeData : MonoBehaviour
{
    public List<Pokemon_Data> pokeDataHolder;

   public void callRetrievePokemon(int id)
   {
        id = 2;
        StartCoroutine(RetrieveMon(id));
   }

    private IEnumerator RetrieveMon(int pokemonID)
    {   
        WWWForm form = new WWWForm();   
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_mon.php", form);
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
                foreach (string s in retrieve_result)
                {
                    Debug.Log(s);
                }
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");
        }


    }
}
