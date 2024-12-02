using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerManager
{
    public static int playerID;
    public static int currentFloor;
    public static List<Pokemon> party = new List<Pokemon>();

    public static void initialize()
    {
        playerID = 121;
        currentFloor = 0;
    }


}
