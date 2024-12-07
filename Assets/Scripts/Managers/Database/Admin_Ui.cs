using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Admin_Ui : MonoBehaviour
{
    public List<int> spriteID_list = new List<int>();
    public List<int> pokemonID_list = new List<int>();
    public List<string> pokemonName_list = new List<string>();
    public List<string> pokemonType1_list = new List<string>();
    public List<string> pokemonType2_list = new List<string>();
    
    public List<MoveData> moveDatas = new List<MoveData>();

    public void callRetrieveUI()
    {
        StartCoroutine(RetrieveDataUI());
    }

    public IEnumerator RetrieveDataUI()
    {
        UnityWebRequest retrieve_req = UnityWebRequest.Get("http://localhost/retrieve_data_ui.php");
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                foreach (string s in retrieve_result)
                {
                    //Debug.Log(s);
                    this.DecipherPokemonData(s);
                }
            }
            else
            {
                Debug.Log(retrieve_result[0]);
            }
        }
        else
        {
            Debug.LogWarning("Web Request for AdminModifyParty faled.");
        }
    }

    private void DecipherPokemonData(string value)
    {
        if (value.Contains("PokemonID: "))
        {
            string pokemonID = value.Substring(11);
            this.pokemonID_list.Add(int.Parse(pokemonID));
        }
        else if (value.Contains("PokemonName: "))
        {
            string pokemonName = value.Substring(13);
            this.pokemonName_list.Add(pokemonName);
        }
        else if (value.Contains("PokemonType1: "))
        {
            string pokemonType1 = value.Substring(14);
            this.pokemonType1_list.Add(pokemonType1);
        }
        else if (value.Contains("PokemonType2: "))
        {
            string pokemonType2 = value.Substring(14);
            this.pokemonType2_list.Add(pokemonType2);  
        }
        else if (value.Contains("SpriteID: "))
        {
            string spriteid = value.Substring(10);
            this.spriteID_list.Add(int.Parse(spriteid));
        }
    }
}


