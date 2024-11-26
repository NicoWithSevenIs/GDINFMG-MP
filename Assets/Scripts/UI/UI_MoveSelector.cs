using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MoveSelector : MonoBehaviour
{
    [SerializeField] private int index;

    [SerializeField] private TextMeshProUGUI moveName;
    [SerializeField] private Image colorHandler;

    private void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => {
            if (t["Battler Name"] as string != "Player")
                return;

            var mon = t["Active Pokemon"] as Pokemon_Battle_Instance;

            MoveData m = MoveManager.GetMove(mon.Pokemon.moveSet[index]).Data;

            colorHandler.color = TypeColor.GetColor(m.type);
            moveName.text = m.name;

        });
    }

    public void SelectMove()
    {

    }

}
