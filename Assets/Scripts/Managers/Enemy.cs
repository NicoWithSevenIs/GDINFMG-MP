using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private static Enemy instance = null;
    public static Enemy Instance { get => instance ?? (instance = new Enemy()); }

    public List<Pokemon> enemyMons = new();

}
