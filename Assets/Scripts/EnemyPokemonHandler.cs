using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPokemonHandler : MonoBehaviour
{   
    void Start()
    {
        DatabaseManager.Instance.GenerateEnemyParty();
    }

}
