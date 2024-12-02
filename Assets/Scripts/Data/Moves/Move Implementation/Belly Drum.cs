using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellyDrum : Move
{
    public override int ID => 17;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        float damage = attacker.stat.Health / 2;
        attacker.TakeDamage(damage);
        attacker.ModHandler.AddMod(EStatType.ATTACK, 6, attacker.Pokemon.data.name);
    }
}
