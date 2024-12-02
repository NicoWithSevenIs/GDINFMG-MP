using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Admin_AddPokemon : MonoBehaviour
{
  private void callAdminAdd()
  {
        StartCoroutine(AdminAddPokemon());
  }

  private IEnumerator AdminAddPokemon()
  {
        WWWForm form = new WWWForm();
        form.AddField("playerID", PlayerManager.playerID);

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

            }
            else
            {

            }
        }
        else
        {
            Debug.LogWarning("Web Request for SendPlayerData faled.");
        }
    }
}
