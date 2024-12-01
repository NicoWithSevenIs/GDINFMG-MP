using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);   
    }

    #endregion

    private Queue<string> dialogueQueue = new();
    private string currentSpeaker;

    public void QueueDialogue(string speaker, List<string> dialogueList)
    {
        currentSpeaker = speaker;
        foreach(var d in dialogueList) 
            dialogueQueue.Enqueue(d);
        Next();
    }

    public void Next()
    {
        var p = new Dictionary<string, object>();
        p["Dialogue"] = dialogueQueue.Dequeue();
        p["Dialogue Speaker"] = currentSpeaker;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_CONTINUED, p);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueQueue.Count > 0)
            Next();
    }


}
