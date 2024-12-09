using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MoveSelector : MonoBehaviour
{
    [SerializeField] protected int index;

    [SerializeField] protected TextMeshProUGUI moveName;
    [SerializeField] protected Image colorHandler;

    protected MoveData heldMoveData;

    protected virtual void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => {
            if (t["Battler Name"] as string != "Player")
                return;
            var mon = t["Active Pokemon"] as Pokemon_Battle_Instance; 
            heldMoveData = MoveManager.GetMove(mon.Pokemon.moveSet[index]).Data;
          

            OnPokemonChanged();
        });
    }

    protected virtual void OnPokemonChanged()
    {
        colorHandler.color = TypeColor.GetColor(heldMoveData.type);
        moveName.text = heldMoveData.name;
    }

    public virtual void SelectMove()
    {
        var p = new Dictionary<string, object>();
        p["Move Index"] = index;
        p["Battler Name"] = "Player";
        EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_MOVE_DECLARED, p);
    }

}
