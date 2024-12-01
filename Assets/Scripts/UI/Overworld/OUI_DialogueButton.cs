using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OUI_DialogueButton : MonoBehaviour
{
    [SerializeField] private RectTransform button;

    private string currentSpeaker;
    private List<string> currentDialogue;

    private void Start()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_INVOKABLE, t => {
            currentSpeaker =  t["Dialogue Speaker"] as string;
            currentDialogue = t["Dialogue"] as List<string>;
            button.gameObject.SetActive(true);
        });
        button.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && button.gameObject.activeSelf)
        {
            var p = new Dictionary<string, object>();
            p["Dialogue Speaker"] = currentSpeaker;
            p["Dialogue"] = currentDialogue;

            EventBroadcaster.InvokeEvent(EVENT_NAMES.OVERWORLD_EVENTS.ON_DIALOGUE_INVOKED, p);
            button.gameObject.SetActive(false);
        }
            

    }


}
