using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DescMoveSelector : UI_MoveSelector
{

    [SerializeField] private Image type;
    [SerializeField] private TextMeshProUGUI typeName;

    protected override void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_VIEWER_INVOKED, t => {
            Pokemon_Battle_Instance mon = BattleManager.instance.GetPlayerPokemon((int)t["Party Index"]);
            heldMoveData = MoveManager.GetMove(mon.Pokemon.moveSet[index]).Data;

            OnPokemonChanged();
        });
    }

    protected override void OnPokemonChanged()
    {
        base.OnPokemonChanged();
        type.color = TypeColor.GetColor(heldMoveData.type);
        typeName.text = heldMoveData.type.ToString();
    }

    public override void SelectMove()
    {
        var p = new Dictionary<string, object>();
        p["Move Data"] = heldMoveData;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_MOVE_VIEWER_INVOKED, p);
    }

}
