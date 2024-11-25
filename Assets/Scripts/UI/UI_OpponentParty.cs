using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpponentParty : MonoBehaviour
{
    [SerializeField] private Image[] icons;

    private void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => {
            if (t["Battler Name"] as string != "Enemy")
                return;

            int index = BattleManager.instance.GetEnemyPokemonIndex(t["Active Pokemon"] as Pokemon_Battle_Instance);
            icons[index].enabled = false;
        });
    }

}
