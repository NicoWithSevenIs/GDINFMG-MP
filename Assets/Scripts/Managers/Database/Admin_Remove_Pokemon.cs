using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Remove_Pokemon : MonoBehaviour
{
    public TMP_InputField removeInput;
    public void callRemovePokemon()
    {
        StartCoroutine(RemovePokemon());
    }

    private IEnumerator RemovePokemon()
    {
        WWWForm form = new WWWForm();
        form.AddField("pokemonName", removeInput.text);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/remove_pokemon.php", form);
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
            Debug.LogWarning("Web Request for AdminModifyParty faled.");
        }

    }


}
