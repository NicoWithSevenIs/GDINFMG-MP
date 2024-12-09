using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AncientPower : Move
{
    public override int ID => 6;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        bool isACriticalStrike;
        float damage = Pokemon_Battle_Instance.CalculateDamage(attacker, target, m_Data.Value.power, m_Data.Value.type, m_Data.Value.moveType, out isACriticalStrike);
        target.TakeDamage(damage);

        int rand = Random.Range(1, 10);
        if (rand == 9)
        {
            attacker.ModHandler.AddMod(EStatType.ATTACK, 1, attacker.Pokemon.data.name);
            attacker.ModHandler.AddMod(EStatType.DEFENSE, 1, attacker.Pokemon.data.name);
            attacker.ModHandler.AddMod(EStatType.SPECIAL_ATTACK, 1, attacker.Pokemon.data.name);
            attacker.ModHandler.AddMod(EStatType.SPECIAL_DEFENSE, 1, attacker.Pokemon.data.name);
            attacker.ModHandler.AddMod(EStatType.SPEED, 1, attacker.Pokemon.data.name);
        }
    }
}
