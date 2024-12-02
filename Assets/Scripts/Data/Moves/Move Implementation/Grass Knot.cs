using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassKnot : Move
{
    public override int ID => 3;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        bool isACriticalStrike;
        int power = 0;
        float weight = target.Pokemon.data.weight;
        if (weight <= 9.9)
        {
            power = 20;

        }
        else if (10 <= weight && weight <= 24.9)
        {
            power = 40;
        }
        else if (25 <= weight && weight <= 49.9)
        {
            power = 60;
        }
        else if (50 <= weight && weight <= 99.9)
        {
            power = 80;
        }
        else if (100 <= weight && weight <= 199.9)
        {
            power = 100;
        }
        else if (200 <= weight)
        {
            power = 120;

        }
        float damage = Pokemon_Battle_Instance.CalculateDamage(attacker, target, power, m_Data.Value.type, m_Data.Value.moveType, out isACriticalStrike);
        target.TakeDamage(damage);
    }
}
