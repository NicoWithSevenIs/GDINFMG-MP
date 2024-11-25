using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Navigation : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu;
    [SerializeField] private GameObject moveMenu;
    [SerializeField] private GameObject switchMenu;

    [SerializeField] private GameObject switchMenuBackButton;

    private void Awake()
    {
        ReturnToRoot();
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => ReturnToRoot());
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_FORCE_SWITCH, t => InvokeSwitchMenu(true));
    }

    public void ReturnToRoot()
    {
        rootMenu.SetActive(true);
        moveMenu.SetActive(false);
        switchMenu.SetActive(false);
    }

    public void InvokeMoveMenu()
    {
        rootMenu.SetActive(false);
        moveMenu.SetActive(true);
        switchMenu.SetActive(false);
    }

    public void InvokeSwitchMenu(bool forceSwitch)
    {
        rootMenu.SetActive(false);
        moveMenu.SetActive(false);
        switchMenu.SetActive(true);
        switchMenuBackButton.SetActive(!forceSwitch);
    }

}
