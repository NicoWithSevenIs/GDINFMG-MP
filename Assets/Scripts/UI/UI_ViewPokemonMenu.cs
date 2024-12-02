using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ViewPokemonMenu: MonoBehaviour
{
    [SerializeField] private CanvasGroup ViewScreen;
    [SerializeField] private UI_DisplayViewPokemon display;
    [SerializeField] private UI_DataViewPokemon data;



    private void Awake()
    {
        ViewScreen.gameObject.SetActive(true);

        Utilities.SetUIActive(ViewScreen, false);
        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_VIEWER_INVOKED, t => {
            LoadPokemonToViewer((int)t["Party Index"]);
            Utilities.SetUIActive(ViewScreen, true);
        });
    }

    private void LoadPokemonToViewer(int partyIndex)
    {
        Pokemon_Battle_Instance i = BattleManager.instance.GetPlayerPokemon(partyIndex);
        display.LoadPokemonData(i);
        data.LoadPokemonData(i);
    }

    public void ExitViewScreen()
    {
        Utilities.SetUIActive(ViewScreen, false);
    }

}
