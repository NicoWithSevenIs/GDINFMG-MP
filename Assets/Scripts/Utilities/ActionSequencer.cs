using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionSequenceComponent
{
    public Action action;
    public bool willPauseSequence;

    public ActionSequenceComponent(Action action, bool willPauseSequence = false)
    {
        this.action = action;
        this.willPauseSequence = willPauseSequence;
    }
}

public class ActionSequencer : MonoBehaviour
{

    #region Singleton
    public static ActionSequencer Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => { 
            foreach(var channel in sequencer)  
                channel.Clear();
        });


        for (int i = 0; i < CHANNELS; i++)
            sequencer.Add(new());
    }
    #endregion

    public const int CHANNELS = 3;

    private List<LinkedList<ActionSequenceComponent>> sequencer = new();

    private bool isPromptActive = false;

    private int invokerIndex = 0;


    public static void AddToSequenceBack(List<ActionSequenceComponent> load, int priorityChannel)
    {
        Instance.invokerIndex = 0;
        priorityChannel = Mathf.Clamp(priorityChannel, 0, CHANNELS-1);
        foreach (var sequenceComponent in load)
            Instance.sequencer[priorityChannel].AddLast(sequenceComponent);
    }

    public static void AddToSequenceFront(List<ActionSequenceComponent> load, int priorityChannel)
    {
        Instance.invokerIndex = 0;
        priorityChannel = Mathf.Clamp(priorityChannel, 0, CHANNELS - 1);
        load.Reverse();
        foreach (var sequenceComponent in load)
            Instance.sequencer[priorityChannel].AddFirst(sequenceComponent);
    }


    public static void Perform()
    {

        bool hasEncounteredPause = false;

        while (Instance.invokerIndex < CHANNELS)
        {
            while (Instance.sequencer[Instance.invokerIndex].Count > 0)
            {
                ActionSequenceComponent task = Instance.sequencer[Instance.invokerIndex].First.Value;
                Instance.sequencer[Instance.invokerIndex].RemoveFirst();
                task.action?.Invoke();
             

                if (task.willPauseSequence)
                {
                    Instance.isPromptActive = true;
                    hasEncounteredPause = true;
                    break;
                }
            }

            if (hasEncounteredPause)
                break;

            Instance.invokerIndex++;
        }

        if (Instance.invokerIndex == CHANNELS - 1 && Instance.sequencer[CHANNELS-1].Count == 0)
            Instance.invokerIndex = 0;

    }
           

  



    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (isPromptActive)
        {
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_ENDED);
            isPromptActive = false;
            Perform();
        }
            
    }



}
