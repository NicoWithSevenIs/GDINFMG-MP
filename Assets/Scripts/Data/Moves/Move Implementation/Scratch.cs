using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : Move
{
    public override int ID => 4;

    public override void PerformMove(Pokemon attacker, Pokemon target)
    {
        throw new System.NotImplementedException();

        //target.takedamage(attacker.getAttack());
    }
}
