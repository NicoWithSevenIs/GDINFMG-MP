using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonDataManager: MonoBehaviour
{

    #region Singleton

    public static PokemonDataManager Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    #endregion

    private Dictionary<int, Pokemon_Data> localStorage;


    public static Pokemon_Data? RequestPokemon(int id)
    {

        if (Instance.localStorage.ContainsKey(id)) 
            return Instance.localStorage[id];
        else 
        { 
            //make query here
        }

        return null;
    }


}
