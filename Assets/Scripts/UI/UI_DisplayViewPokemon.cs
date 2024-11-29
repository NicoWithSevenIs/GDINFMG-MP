using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DisplayViewPokemon : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI pokemonName;
    [SerializeField] private Image pokemonImage;
    [SerializeField] private Image type1;
    [SerializeField] private TextMeshProUGUI type1Text;
    [SerializeField] private Image type2;
    [SerializeField] private TextMeshProUGUI type2Text;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI nature;

    public void LoadPokemonData(Pokemon_Battle_Instance mon)
    {
        pokemonName.text = mon.Pokemon.data.name;
        pokemonImage.sprite = WebAPIManager.Instance.GetSprite(mon.Front_Sprite_URL);
        type1.color = TypeColor.GetColor(mon.Pokemon.data.type1);
        type1Text.text = mon.Pokemon.data.type1.ToString();

        type2.gameObject.SetActive(mon.Pokemon.data.type2 != null);

        if (type2.gameObject.activeSelf)
        {
            type2.color = TypeColor.GetColor(mon.Pokemon.data.type2.Value);
            type2Text.text = mon.Pokemon.data.type2.Value.ToString();
        }

        health.text = $"{(int)mon.CurrentHealth} / {(int)mon.stat.Health}";
        nature.text = mon.Pokemon.nature;
    }


}
