using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDance : Move
{
    public override int ID => 20;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        attacker.ModHandler.AddMod(EStatType.ATTACK, 1, attacker.Pokemon.data.name);
        attacker.ModHandler.AddMod(EStatType.SPEED, 1, attacker.Pokemon.data.name);
    }
}
