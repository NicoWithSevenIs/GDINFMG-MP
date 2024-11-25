using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Selector : MonoBehaviour
{
    [SerializeField] private GameObject SelectionButton;
    [SerializeField] private Image pokemonImage;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI pokemonName;

    [SerializeField] private int SelectionIndex;

    private void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => SetButtonEnabled());
    }
    private void OnEnable()
    {
        Pokemon_Battle_Instance instance = BattleManager.instance.GetPlayerPokemon(SelectionIndex);

        pokemonImage.sprite = WebAPIManager.Instance.GetSprite(instance.Front_Sprite_URL);
        pokemonName.text = instance.Pokemon.data.name;
        healthBarFill.fillAmount = instance.HealthPercentage;

        SetButtonEnabled();
    }

    private void SetButtonEnabled()
    {

        bool pokemonNotFielded = BattleManager.instance.GetPlayerActivePokemonIndex() != SelectionIndex;
        bool pokemonNotFainted = !BattleManager.instance.GetPlayerPokemon(SelectionIndex).isFainted;
        SelectionButton.SetActive(pokemonNotFielded && pokemonNotFainted);
    }

    public void DeclareSelection()
    {
        BattleManager.instance.SwitchPlayerPokemon(SelectionIndex);
    }
}
