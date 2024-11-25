using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DataViewPokemon : MonoBehaviour
{

    [SerializeField] private Button statButton;
    [SerializeField] private Button moveButton;

    [SerializeField] private CanvasGroup statPanel;
    [SerializeField] private CanvasGroup movePanel;



    [SerializeField] private List<UI_StatTextGroup> textGroup;


    private void OnEnable()
    {
        statPanel.gameObject.SetActive(true);
        movePanel.gameObject.SetActive(true);
        showStatPanel(true); 
    }

    public void showStatPanel(bool willShow)
    {
        statButton.interactable = !willShow;
        statPanel.alpha = willShow ? 1 : 0;
        statPanel.interactable = willShow;

        moveButton.interactable = willShow;
        movePanel.alpha = willShow ? 0 : 1;
        movePanel.interactable = !willShow;
    }

    public void LoadPokemonData(Pokemon_Battle_Instance mon)
    {

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
