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
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_FORCE_SWITCH, t => InvokeSwitchMenu(true));

        rootMenu.gameObject.SetActive(true);
        moveMenu.gameObject.SetActive(true);
        switchMenu.gameObject.SetActive(true);
    }

    public void SetUIActive(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }

    public void ReturnToRoot()
    {
        SetUIActive(rootMenu, true);
        SetUIActive(moveMenu, false);
        SetUIActive(switchMenu, false);
    }

    public void InvokeMoveMenu()
    {
        SetUIActive(rootMenu, false);
        SetUIActive(moveMenu, true);
        SetUIActive(switchMenu, false);
    }

    public void InvokeSwitchMenu(bool forceSwitch)
    {
        SetUIActive(rootMenu, false);
        SetUIActive(moveMenu, false);
        SetUIActive(switchMenu, true);
        switchMenuBackButton.SetActive(!forceSwitch);
    }

}
