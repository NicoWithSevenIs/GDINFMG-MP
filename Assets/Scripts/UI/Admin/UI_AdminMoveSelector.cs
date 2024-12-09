using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AdminMoveSelector : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI moveName;
    [SerializeField] protected Image colorHandler;
    [SerializeField] private Image type;
    [SerializeField] private TextMeshProUGUI typeName;

    private MoveData? heldMoveData;

    public void AssignMoveData(MoveData data)
    {
        heldMoveData = data;

        colorHandler.color = TypeColor.GetColor(heldMoveData.Value.type);
        moveName.text = heldMoveData.Value.name;
        type.color = TypeColor.GetColor(heldMoveData.Value.type);
        typeName.text = heldMoveData.Value.type.ToString();
    }

    public void ShowMove()
    {
        if(heldMoveData == null)
        {
            Debug.LogError(new System.Exception($"No Move Data assigned for {gameObject.GetInstanceID()}"));
            return;
        }
        var p = new Dictionary<string, object>();
        p["Move Data"] = heldMoveData;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_MOVE_VIEWER_INVOKED, p);
    }


}
