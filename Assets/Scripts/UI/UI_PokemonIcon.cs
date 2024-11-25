using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PokemonIcon : MonoBehaviour
{
    [SerializeField] private string ownerName;
    private Image pokemon;
    private void Awake()
    {
      
        pokemon = GetComponent<Image>();
        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_CHANGED, t => {

            if (t["Battler Name"] as string != ownerName)
                return;

            var battle_instance = t["Active Pokemon"] as Pokemon_Battle_Instance;

            if(ownerName == "Player")
                pokemon.sprite = WebAPIManager.Instance.GetSprite(battle_instance.Back_Sprite_URL);
            else if (ownerName == "Enemy")
                pokemon.sprite = WebAPIManager.Instance.GetSprite(battle_instance.Front_Sprite_URL);

        });
    }
}
