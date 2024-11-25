using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    #region Singleton
    public static BattleManager instance { get; private set; } = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
        Initialize();
    }
    #endregion

    public const string BASE_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/";

    private List<Task> downloadQueue = new();

    public float loadProgress
    {
        get => downloadQueue.Count > 0 ? ((float)downloadQueue.Where(t => t.IsCompleted).Count() / (float)downloadQueue.Count) : 0;
    }

    Battler Player;
    Battler Enemy;

    private bool hasLoaded = false;

    private void Initialize()
    {
        //make queries here

        Player = new Battler(Battler.PLAYER, SampleMons.GetList());
        Enemy  = new Battler(Battler.ENEMY, SampleMons.GetList());


        void DownloadSprites(Pokemon_Battle_Instance[] Party)
        {
            foreach (var battle_instance in Party)
            {
                int id = battle_instance.Pokemon.data.id;

                string frontFile = $"{BASE_URL}{id}.png";
                string backFile = $"{BASE_URL}back/{id}.png";

                downloadQueue.Add(WebAPIManager.Instance.DownloadImage(frontFile));
                downloadQueue.Add(WebAPIManager.Instance.DownloadImage(backFile));

                battle_instance.SetSprite(frontFile, backFile);
            }
        }

        DownloadSprites(Player.Party);
        DownloadSprites(Enemy.Party);

    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.ActivePokemon.TakeDamage(10);
        }

        if(!hasLoaded && loadProgress == 1f)
        {
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED);
            Player.SwitchPokemon(Player.ActivePokemonIndex);
            Enemy.SwitchPokemon(Enemy.ActivePokemonIndex);
            hasLoaded = true;
        }
    }

    #region Wrapping


    public void SwitchPlayerPokemon(int index)
    {
        Player.SwitchPokemon(index);
    }

    public int GetPlayerActivePokemonIndex() => Player.ActivePokemonIndex;

    public Pokemon_Battle_Instance GetPlayerPokemon(int index) => Player.GetPokemon(index);

    #endregion


}

