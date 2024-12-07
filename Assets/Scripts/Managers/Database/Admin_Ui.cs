using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Admin_Ui : MonoBehaviour
{
    public List<int> spriteID_list;
    public List<int> pokemonID_list;
    public List<string> pokemonName_list;
    public List<string> pokemonType1_list;
    public List<string> pokemonType2_list;
    
    public List<MoveData> moveDatas;

    public void callRetrieveUI()
    {

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


