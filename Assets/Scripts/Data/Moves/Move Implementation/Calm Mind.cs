using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmMind : Move
{
    public override int ID => 12;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        attacker.ModHandler.AddMod(EStatType.SPECIAL_ATTACK, 1, attacker.Pokemon.data.name);
        attacker.ModHandler.AddMod(EStatType.SPECIAL_DEFENSE, 1, attacker.Pokemon.data.name);
    }
}

