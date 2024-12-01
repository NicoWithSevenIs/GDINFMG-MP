using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OUI_DIalogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trainerName;
    [SerializeField] private TextMeshProUGUI dialogue;

    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Utilities.SetUIActive(canvasGroup, false);

        EventBroadcaster.AddObserver(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_CONTINUED, t => { 
            Utilities.SetUIActive(canvasGroup, true);
            dialogue.text = t["Dialogue"] as string;
            trainerName.text = t["Dialogue Speaker"] as string;
        });
    }
}
