using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveBird : Move
{
    public override int ID => 9;

    public override void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target)
    {
        bool isACriticalStrike;
        float damage = Pokemon_Battle_Instance.CalculateDamage(attacker, target, m_Data.Value.power, m_Data.Value.type, m_Data.Value.moveType, out isACriticalStrike, 24);
        target.TakeDamage(damage);
        attacker.TakeDamage(damage / 3);

        var typeComp = new ActionSequenceComponent(() =>
        {
            var p = new Dictionary<string, object>();
            p["Message"] = $"{attacker.Pokemon.data.name} was hurt by recoil!";
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
        }, true);

        ActionSequencer.AddToSequenceBack(new() { typeComp }, 2);
    }
}
