using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    #region Singleton

    public static PokemonManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    #endregion

   

}
