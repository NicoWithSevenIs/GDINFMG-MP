using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Admin_Modify_Party : MonoBehaviour
{
    public List<TMP_InputField> partyInputs = new List<TMP_InputField>();
 
    public void callAdminModifyParty()
    {

        StartCoroutine(AdminModifyParty());
    }

    private IEnumerator AdminModifyParty()
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", partyInputs[0].text);
        form.AddField("instanceID1", partyInputs[1].text);
        form.AddField("instanceID2", partyInputs[2].text);
        form.AddField("instanceID3", partyInputs[3].text);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/admin_modify_party.php", form);
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
