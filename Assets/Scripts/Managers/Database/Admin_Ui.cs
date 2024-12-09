using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Admin_Ui : MonoBehaviour
{
    public DB_Utility_Helper db_UtilityHelper;

    public List<int> spriteID_list = new List<int>();
    public List<int> pokemonID_list = new List<int>();
    public List<string> pokemonName_list = new List<string>();
    public List<string> pokemonType1_list = new List<string>();
    public List<string> pokemonType2_list = new List<string>();
    
    public List<Pokemon_Data> pokemonData_list = new List<Pokemon_Data>();
    public List<MoveData> moveData_list = new List<MoveData>();

    public List<int> moveID_list = new List<int>();
    public List<int> movePower_list = new List<int>();
    public List<string> moveName_list = new List<string>();
    public List<string> moveDesc_list = new List<string>();
    public List<string> moveGroup_list = new List<string>(); // move group refers to whether a move is physical, special, or status
    public List<string> moveType_list = new List<string>();


    public Task loadingTask;

    private void Awake()
    {
        loadingTask = RetrieveDataUIAsync();
    }

    public void callRetrieveUI()
    {
        this.clearLists();
        StartCoroutine(RetrieveDataUI());
    }

    public async Task RetrieveDataUIAsync()
    {
        this.clearLists();
        UnityWebRequest retrieve_req = UnityWebRequest.Get("http://localhost/retrieve_data_ui.php");
        await retrieve_req.SendWebRequest();

        if (retrieve_req == null)
            Debug.LogError("Send Req is null.");

        if (retrieve_req.result != UnityWebRequest.Result.Success)
            Debug.LogWarning("Web Request for AdminModifyParty faled.");

        string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
        if (retrieve_result[0].Contains("Success"))
        {
            foreach (string s in retrieve_result)
            {
                //Debug.Log(s);
                this.DecipherPokemonData(s);
                this.DecipherMoveData(s);
            }

            this.putInMoveData();
        }
        else
        {
            Debug.Log(retrieve_result[0]);
        }
    }

    public IEnumerator RetrieveDataUI()
    {
        UnityWebRequest retrieve_req = UnityWebRequest.Get("http://localhost/retrieve_data_ui.php");
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Send Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                foreach (string s in retrieve_result)
                {
                    //Debug.Log(s);
                    this.DecipherPokemonData(s);
                    this.DecipherMoveData(s);
                }

                this.putInPokemonData();
                this.putInMoveData();
            }
            else
            {
                Debug.Log(retrieve_result[0]);
            }
        }
        else
        {
            Debug.LogWarning("Web Request for AdminModifyParty faled.");
        }
    }

    private void DecipherPokemonData(string value)
    {
        if (value.Contains("PokemonID: "))
        {
            string pokemonID = value.Substring(11);
            this.pokemonID_list.Add(int.Parse(pokemonID));
        }
        else if (value.Contains("PokemonName: "))
        {
            string pokemonName = value.Substring(13);
            this.pokemonName_list.Add(pokemonName);
        }
        else if (value.Contains("PokemonType1: "))
        {
            string pokemonType1 = value.Substring(14);
            this.pokemonType1_list.Add(pokemonType1);
        }
        else if (value.Contains("PokemonType2: "))
        {
            string pokemonType2 = value.Substring(14);
            this.pokemonType2_list.Add(pokemonType2);  
        }
        else if (value.Contains("SpriteID: "))
        {
            string spriteid = value.Substring(10);
            this.spriteID_list.Add(int.Parse(spriteid));
        }
    }

    private void DecipherMoveData(string value)
    {
        if (value.Contains("MoveID: "))
        {
            string moveID = value.Substring(8);
            this.moveID_list.Add(int.Parse(moveID));    
        }
        else if (value.Contains("MoveName: "))
        {
            string moveName = value.Substring(10);
            this.moveName_list.Add(moveName);
        }
        else if (value.Contains("MoveDescription: "))
        {
            string moveDesc = value.Substring(17);
            this.moveDesc_list.Add(moveDesc);
        }
        else if (value.Contains("MoveType: "))
        {
            string moveType = value.Substring(10);
            this.moveType_list.Add(moveType);
        }
        else if (value.Contains("MoveGroup: "))
        {
            string moveGroup = value.Substring(11);
            this.moveGroup_list.Add(moveGroup);
        }
        else if (value.Contains("MovePower: "))
        {
            string movePower = value.Substring(11);
            this.movePower_list.Add(int.Parse(movePower));
        }
    }

    private void putInMoveData()
    {
        int size = this.moveID_list.Count;

        for (int i = 0; i < size; i++)
        {
            string name = this.moveName_list[i];
            string description = this.moveDesc_list[i];
            int power = this.movePower_list[i];
            int pp = 100;
            EType moveType = db_UtilityHelper.getDecipheredType1(this.moveType_list[i]);
            EMoveType moveGroup = db_UtilityHelper.getDecipheredMoveType(this.moveGroup_list[i]);  

            MoveData data = new MoveData(name, description, power, pp, moveType, moveGroup);
            this.moveData_list.Add(data);
        }
    }

    private void putInPokemonData()
    {
        int size = this.pokemonID_list.Count;

        for (int i = 0; i < size; i++)
        {
            int id = this.pokemonID_list[i];
            int spriteid = this.spriteID_list[i];
            string name = this.pokemonName_list[i];
            EType type1 = db_UtilityHelper.getDecipheredType1(this.pokemonType1_list[i]);
            EType? type2 = db_UtilityHelper.getDecipheredType2(this.pokemonType2_list[i]);

            Pokemon_Data data = new Pokemon_Data(id, spriteid, name, type1, type2, new Stat(), 10, 10);
            this.pokemonData_list.Add(data);
        }
    }

    private void clearLists()
    {
        this.spriteID_list.Clear();
        this.pokemonID_list.Clear();    
        this.pokemonName_list.Clear();
        this.pokemonType1_list.Clear();
        this.pokemonType2_list.Clear();

        this.pokemonData_list.Clear();
        this.moveData_list.Clear();

        this.moveID_list.Clear();
        this.moveName_list.Clear();
        this.moveDesc_list.Clear();
        this.movePower_list.Clear();
        this.moveType_list.Clear();
        this.moveGroup_list.Clear();
    }
}


