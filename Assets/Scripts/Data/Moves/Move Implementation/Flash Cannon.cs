using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCannon : Move
{
    public override int ID => 15;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        bool isACriticalStrike;
        float damage = Pokemon_Battle_Instance.CalculateDamage(attacker, target, m_Data.Value.power, m_Data.Value.type, m_Data.Value.moveType, out isACriticalStrike);
        target.TakeDamage(damage);

        int rand = Random.Range(1, 10);
        if (rand == 10)
        {
            target.ModHandler.AddMod(EStatType.SPECIAL_DEFENSE, -1, target.Pokemon.data.name);
        }
    }
}
