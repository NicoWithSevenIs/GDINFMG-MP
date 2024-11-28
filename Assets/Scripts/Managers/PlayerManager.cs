using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerManager
{
    public static int playerID = 121;
    public static int currentFloor;
    public static int currentMoney;
    public static List<Pokemon> party;

    public static void AddPokemon(Pokemon newMon)
    {
        party.Add(newMon);
    }
}
