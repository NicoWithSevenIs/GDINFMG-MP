using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);

        EventBroadcaster.AddObserver(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_INVOKED, QueueDialogue);
    }

    #endregion

    private Queue<string> dialogueQueue = new();
    private string currentSpeaker;
    private Action OnDialogueEnded;


    public void QueueDialogue(Dictionary<string, object> p )
    {
        currentSpeaker = p["Dialogue Speaker"] as string;

        foreach(var d in p["Dialogue"] as List<string>) 
            dialogueQueue.Enqueue(d);

        Next();
    }

    public void Next()
    {
        var p = new Dictionary<string, object>();
        p["Dialogue"] = dialogueQueue.Dequeue();
        p["Dialogue Speaker"] = currentSpeaker;
        EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_CONTINUED, p);

        if (dialogueQueue.Count == 0)
        {
            EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_LEFT);

            //Assign Stuff here

            SceneManager.LoadScene("Nico Scene");
        }
    }

    private void Update()
    {

        if (!Input.GetMouseButtonDown(0))
            return;

        if (dialogueQueue.Count > 0)
        {
            Next();
        } 
     
    }


}
