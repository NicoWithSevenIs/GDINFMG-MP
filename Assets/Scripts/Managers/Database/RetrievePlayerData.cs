using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievePlayerData : MonoBehaviour
{
    public void sendToPlayerManager(string[] retrieve_result)
    {
        foreach (string s in retrieve_result)
        {
           Debug.Log(s + " ");
        }
        
    }
}
