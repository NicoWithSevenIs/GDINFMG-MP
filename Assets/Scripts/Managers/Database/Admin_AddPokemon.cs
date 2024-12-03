using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Admin_AddPokemon : MonoBehaviour
{
  public List<TMP_InputField> pokemonInputs = new List<TMP_InputField>();
  public List<TMP_InputField> statInputs = new List<TMP_InputField>();
  public List<TMP_InputField> movepoolInputs = new List<TMP_InputField>();

    public void callAdminAdd()
  {
        
        StartCoroutine(AdminAddPokemon());
  }

  private IEnumerator AdminAddPokemon()
  {
        WWWForm form = new WWWForm();
        form.AddField("pokemonID", pokemonInputs[0].text);
        form.AddField("pokemonName", pokemonInputs[1].text);
        form.AddField("pokemonType1", pokemonInputs[2].text);
        form.AddField("pokemonType2", pokemonInputs[3].text);
        form.AddField("pokemonWeight", pokemonInputs[4].text);
        form.AddField("pokemonHeight", pokemonInputs[5].text);
        form.AddField("spriteID", pokemonInputs[6].text);

        form.AddField("hp", statInputs[0].text);
        form.AddField("attack", statInputs[1].text);
        form.AddField("special_attack", statInputs[2].text);
        form.AddField("defense", statInputs[3].text);
        form.AddField("special_defense", statInputs[4].text);
        form.AddField("speed", statInputs[5].text);

        form.AddField("moveID1", movepoolInputs[0].text);
        form.AddField("moveID2", movepoolInputs[1].text);
        form.AddField("moveID3", movepoolInputs[2].text);
        form.AddField("moveID4", movepoolInputs[3].text);
        form.AddField("moveID5", movepoolInputs[4].text);
        form.AddField("moveID6", movepoolInputs[5].text);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/admin_add_pokemon.php", form);
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
                Debug.Log(retrieve_result[0]);
            }
            else
            {
                Debug.Log(retrieve_result[0]);
            }
        }
        else
        {
            Debug.LogWarning("Web Request for SendPlayerData faled.");
        }
    }
}
