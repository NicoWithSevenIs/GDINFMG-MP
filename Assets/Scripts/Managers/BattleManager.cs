using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

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

        Player = new Battler("George", Battler.PLAYER, SampleMons.GetList());
        Enemy  = new Battler("Tina", Battler.ENEMY, SampleMons.GetList());


        void DownloadSprites(Pokemon_Battle_Instance[] Party)
        {
            foreach (var battle_instance in Party)
            {
                int id = battle_instance.Pokemon.data.spriteID;

                string frontFile = $"{BASE_URL}{id}.png";
                string backFile = $"{BASE_URL}back/{id}.png";

                downloadQueue.Add(WebAPIManager.Instance.DownloadImage(frontFile));
                downloadQueue.Add(WebAPIManager.Instance.DownloadImage(backFile));

                battle_instance.SetSprite(frontFile, backFile);
            }
        }

        DownloadSprites(Player.Party);
        DownloadSprites(Enemy.Party);

        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => {
            if (t["Battler Name"] as string != "Enemy")
                return;

            if (Enemy.AvailablePokemon > 0 )
                Enemy.SwitchPokemon(Enemy.ActivePokemonIndex + 1);
            else
            {
                EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_PLAYER_WIN);
                Debug.Log("Player Won");

                SceneManager.LoadScene("Derek Scene");
            }
            
        });

        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, t => {
            if (t["Battler Name"] as string != "Player")
                return;

            if (Player.AvailablePokemon > 0)
                EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_FORCE_SWITCH);
            else
            {
                EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_ENEMY_WIN);
                Debug.Log("Enemy Won");
                SceneManager.LoadScene("Derek Scene");
            }
        });

        EventBroadcaster.AddObserver(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_MOVE_DECLARED, PerformMove);

    }

    
    private void PerformMove(Dictionary<string, object> t)
    {


        var actions = new List<ActionSequenceComponent>();

        void HandleFainting(Pokemon_Battle_Instance mon, string name)
        {
            var faintPrompt = new List<ActionSequenceComponent>() {
                        new ActionSequenceComponent(
                            () => {
                                var p = new Dictionary<string, object>();
                                string affix = mon == Enemy.ActivePokemon ? "Foe " : "";
                                p["Message"] =  $"{affix}{mon.Pokemon.data.name} fainted!" ;
                                EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
                            }, true, false
                        ),
                        new ActionSequenceComponent(
                            () => {
                                var p = new Dictionary<string, object>();
                                p["Battler Name"] = name;
                                p["Active Pokemon"] = mon;
                                EventBroadcaster.InvokeEvent(EVENT_NAMES.BATTLE_EVENTS.ON_POKEMON_FAINT, p);
                            }, false, false
                        )
                    };

            ActionSequencer.AddToSequenceFront(faintPrompt, 1);
        }

        void AddToSequence(int moveID, Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target, string namePrefix = "")
        {

            Move m = MoveManager.GetMove(moveID);

            Action promptAction = () => {
                var p = new Dictionary<string, object>();
                p["Message"] =  $"{namePrefix}{attacker.Pokemon.data.name} used {m.Data.name}!" ;
                EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_DIALOGUE_INVOKED, p);
            };

            Action moveAction = () => {

                m.PerformMove(attacker, target);

                //bandaid fix
                if (Player.ActivePokemon.CurrentHealth == 0)
                    HandleFainting(Player.ActivePokemon, Player.ActivePokemon.OwnerType);

           
                if (Enemy.ActivePokemon.CurrentHealth == 0)
                    HandleFainting(Enemy.ActivePokemon, Enemy.ActivePokemon.OwnerType);
                
            };

            var prompt = new ActionSequenceComponent(promptAction, true);
            var move  = new ActionSequenceComponent(moveAction, false);

            actions.Add(prompt);
            actions.Add(move);
        }

        int playerMoveID = Player.ActivePokemon.Pokemon.moveSet[(int)t["Move Index"]];
        int enemyMoveID = Enemy.ActivePokemon.Pokemon.moveSet[UnityEngine.Random.Range(0, 4)];

        float playerMonSPD = Player.ActivePokemon.stat.Speed;
        float enemyMonSPD = Enemy.ActivePokemon.stat.Speed;

      
        if (playerMonSPD >= enemyMonSPD)
        {
            AddToSequence(playerMoveID, Player.ActivePokemon, Enemy.ActivePokemon);
            AddToSequence(enemyMoveID, Enemy.ActivePokemon, Player.ActivePokemon, "Foe ");
        }
        else
        {
            AddToSequence(enemyMoveID, Enemy.ActivePokemon, Player.ActivePokemon, "Foe ");
            AddToSequence(playerMoveID, Player.ActivePokemon, Enemy.ActivePokemon);
        }
        
        ActionSequencer.AddToSequenceFront(actions, 2);
        ActionSequencer.Perform();

    }

    private void Update()
    {

        if (!hasLoaded && loadProgress == 1f)
        {
            EventBroadcaster.InvokeEvent(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED);
            Enemy.SwitchPokemon(Enemy.ActivePokemonIndex, false);
            Player.SwitchPokemon(Player.ActivePokemonIndex, true);
   
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

    public int GetEnemyPokemonIndex(Pokemon_Battle_Instance battle_instance)
    {
        for(int i =0; i< Enemy.Party.Count(); i++)
            if (Enemy.Party[i] == battle_instance) 
                return i;

        return -1;
    }

    public int AvailablePlayerPokemon { get =>  Player.AvailablePokemon; }

    #endregion


}


