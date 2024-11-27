using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;
    public RetrievePokeData retrievePokeData;
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
        for (int i = 0; i < 1; i++)
        {
            int randomized_int = Random.Range(1, 11);
            retrievePokeData.callRetrievePokemon(randomized_int, i);
            
        }

    }
}
