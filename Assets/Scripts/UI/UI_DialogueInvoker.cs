using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DialogueInvoker : MonoBehaviour
{
    [SerializeField] private CanvasGroup dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;


    private Queue<string> dialogueQueue = new();

    private bool isDialogueActive { get => dialogueBox.alpha == 1f;  }

    private void Awake()
    {
        dialogueBox.gameObject.SetActive(true);
        SetUIActive(dialogueBox, false);
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, t => {
            var dialogueList = t["Messages"] as List<string>;
            SetUIActive(dialogueBox, true);
            foreach (var dialogue in dialogueList) 
                dialogueQueue.Enqueue(dialogue);
            Next();
        });
    }

    public void SetUIActive(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }

    private void Next()
    {
        string s = dialogueQueue.Dequeue();
        dialogueText.text = s;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !isDialogueActive)
            return;
        
        if(dialogueQueue.Count == 0)
        {
            SetUIActive(dialogueBox, false);
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_ENDED);
        }
        else
        {
            Next();
        }   
    }

}
