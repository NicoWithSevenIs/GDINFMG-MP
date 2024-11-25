using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{

    [SerializeField] private string owner;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI pokemonName;

    [SerializeField] private GameObject maleIcon;
    [SerializeField] private GameObject femaleIcon;


    private void Awake()
    {
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, OnPokemonSwitched);
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_HEALTH_CHANGED, OnHealthUpdate);
    }

    public void OnPokemonSwitched(Dictionary<string, object> p)
    {

        if (p["Battler Name"] as string != owner)
            return;
           
        var mon = p["Active Pokemon"] as Pokemon_Battle_Instance;

        pokemonName.text = mon.Pokemon.data.name;

        maleIcon.SetActive(mon.Pokemon.sex == ESex.MALE);
        femaleIcon.SetActive(mon.Pokemon.sex == ESex.FEMALE);

        OnHealthUpdate(p);
    }

    public void OnHealthUpdate(Dictionary<string, object> p)
    {
        if (p["Battler Name"] as string != owner)
            return;

        var mon = p["Active Pokemon"] as Pokemon_Battle_Instance;
        healthBarFill.fillAmount = mon.CurrentHealth / mon.Pokemon.data.baseStats.Health;
    }


}
