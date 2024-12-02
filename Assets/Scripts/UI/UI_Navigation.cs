using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Navigation : MonoBehaviour
{
    [SerializeField] private CanvasGroup rootMenu;
    [SerializeField] private CanvasGroup moveMenu;
    [SerializeField] private CanvasGroup switchMenu;

    [SerializeField] private GameObject switchMenuBackButton;

    private void Awake()
    {
        ReturnToRoot();
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => ReturnToRoot());
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_FORCE_SWITCH, t => InvokeSwitchMenu(true) );

        rootMenu.gameObject.SetActive(true);
        moveMenu.gameObject.SetActive(true);
        switchMenu.gameObject.SetActive(false);

        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, t => DisableAll());
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_ENDED, t => ReturnToRoot());
    }



    public void DisableAll()
    {
        Utilities.SetUIActive(rootMenu, false);
        Utilities.SetUIActive(moveMenu, false);
        switchMenu.gameObject.SetActive(false);
    }

    public void ReturnToRoot()
    {
        Utilities.SetUIActive(rootMenu, true);
        Utilities.SetUIActive(moveMenu, false);
        switchMenu.gameObject.SetActive(false);
    }

    public void InvokeMoveMenu()
    {
        Utilities.SetUIActive(rootMenu, false);
        Utilities.SetUIActive(moveMenu, true);
        switchMenu.gameObject.SetActive(false);
    }

    public void InvokeSwitchMenu(bool forceSwitch)
    {
        Utilities.SetUIActive(rootMenu, false);
        Utilities.SetUIActive(moveMenu, false);
        switchMenu.gameObject.SetActive(true);
        switchMenuBackButton.SetActive(!forceSwitch);
    }

}
