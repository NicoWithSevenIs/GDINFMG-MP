using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : Move
{
    public override int ID => 2;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        throw new System.NotImplementedException();

        //target.takedamage(attacker.getAttack());
    }
}
