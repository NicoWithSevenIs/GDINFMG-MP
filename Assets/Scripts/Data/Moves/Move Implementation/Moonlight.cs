using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonlight : Move
{
    public override int ID => 8;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        float damage = attacker.stat.Health / 2;
        attacker.TakeDamage(-damage);
    }
}
    
