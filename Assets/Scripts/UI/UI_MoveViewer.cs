using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MoveViewer : MonoBehaviour
{
    [SerializeField] private CanvasGroup parentPanel;

    [SerializeField] private TextMeshProUGUI moveName;
    [SerializeField] private TextMeshProUGUI moveDescription;
    [SerializeField] private TextMeshProUGUI powerValue;

    [SerializeField] private GameObject physicalIcon;
    [SerializeField] private GameObject specialIcon;
    [SerializeField] private GameObject statusIcon;

    private void OnEnable()
    {
        parentPanel.alpha =0;
    }

    private void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_MOVE_VIEWER_INVOKED, t => {

            parentPanel.alpha = 1;
            var m = (MoveData)t["Move Data"];

            moveName.text = m.name;
            moveDescription.text = m.description;
            powerValue.text = m.moveType != EMoveType.STATUS ? $"Power: {m.power}" : "";

            physicalIcon.SetActive(m.moveType == EMoveType.PHYSICAL);
            specialIcon.SetActive(m.moveType == EMoveType.SPECIAL);
            statusIcon.SetActive(m.moveType == EMoveType.STATUS);

        });
    }


}
