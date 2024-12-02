using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DataViewPokemon : MonoBehaviour
{

    [SerializeField] private Button statButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button modifierButton;

    [SerializeField] private CanvasGroup statPanel;
    [SerializeField] private CanvasGroup movePanel;
    [SerializeField] private CanvasGroup modifierPanel;

    [SerializeField] private UI_ModifierHandler modifierHandler;

    [SerializeField] private List<UI_StatTextGroup> textGroup;


    private void OnEnable()
    {
        statPanel.gameObject.SetActive(true);
        movePanel.gameObject.SetActive(true);
        showStatPanel(1); 
    }

    public void showStatPanel(int index)
    {

        void setActive(Button button, CanvasGroup panel, bool willShow)
        {
            button.interactable = !willShow;
            panel.alpha = willShow ? 1 : 0;
            panel.interactable = willShow;
        }

        setActive(statButton, statPanel, index == 1);
        setActive(moveButton, movePanel, index == 2);   
        setActive(modifierButton, modifierPanel, index == 3);
        
    }

    public void LoadPokemonData(Pokemon_Battle_Instance mon)
    {

        modifierHandler.LoadMods(mon.ModHandler);

        for (int i = 0; i < Enum.GetNames(typeof(EStatType)).Length; i++) {
            var t = (EStatType)i;
            int baseStat = (int)mon.Pokemon.data.baseStats.GetByEnum(t);
            int IV = (int)mon.Pokemon.IV.GetByEnum(t);
            int EV = (int)mon.Pokemon.EV.GetByEnum(t);
            int total = (int)mon.TotalStats.GetByEnum(t);
            textGroup[i].SetStats(baseStat, IV, EV, total);
        }

    }
}
