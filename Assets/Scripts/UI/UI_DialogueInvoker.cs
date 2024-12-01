using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DialogueInvoker : MonoBehaviour
{
    [SerializeField] private CanvasGroup dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        dialogueBox.gameObject.SetActive(true);
        Utilities.SetUIActive(dialogueBox, false);
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, t => {
            dialogueText.text = t["Message"] as string;
            Utilities.SetUIActive(dialogueBox, true);
        });
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => Utilities.SetUIActive(dialogueBox, false));
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_ENDED, t => Utilities.SetUIActive(dialogueBox, false));
    }

 


}
