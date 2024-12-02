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
        SetUIActive(dialogueBox, false);
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, t => {
            dialogueText.text = t["Message"] as string;
            SetUIActive(dialogueBox, true);
        });
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => SetUIActive(dialogueBox, false));
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_ENDED, t => SetUIActive(dialogueBox, false));
    }

    public void SetUIActive(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }


}
