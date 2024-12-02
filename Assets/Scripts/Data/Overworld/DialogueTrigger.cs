using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProximityChecker))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string speakerName;
    [SerializeField] private List<string> dialogue;

    private ProximityChecker checker;

    private void Start()
    {
        checker = GetComponent<ProximityChecker>();
        checker.OnPlayerEnter += () => {

            var p = new Dictionary<string, object>(); 
            p["Dialogue Speaker"] = speakerName;
            p["Dialogue"] = dialogue;

            EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_INVOKABLE, p);

        };

        checker.OnPlayerLeave += () => EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_LEFT);


    }

 

}
