using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RetrievePokeData : MonoBehaviour
{
    private List<Pokemon_Data> pokeDataHolder = new List<Pokemon_Data>();
   public void callRetrievePokemon(int id, int currIndex)
   {    
        StartCoroutine(RetrieveMon(id, currIndex));
   }

    private IEnumerator RetrieveMon(int pokemonID, int currIndex)
    {
        Pokemon_Data data = new Pokemon_Data();
        //Debug.Log("Retrieving Mon...");
        WWWForm form = new WWWForm();   
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_mon.php", form);
        yield return retrieve_req.SendWebRequest();
        
        if (retrieve_req == null)
        {
            Debug.LogError("Retrieve_Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                data.id = int.Parse(retrieve_result[1]);
                data.name = retrieve_result[2];
                //Debug.Log("New Mon Daa Weight: " + retrieve_result[5].GetType() + " " + retrieve_result[5]);
                data.type1 = this.getDecipheredType1(retrieve_result[3]);
                data.type2 = this.getDecipheredType2(retrieve_result[4]);
                data.weight = float.Parse(retrieve_result[5]);
                data.height = float.Parse(retrieve_result[6]);
                data.spriteID = int.Parse(retrieve_result[7]);

                StartCoroutine(RetrieveStat(pokemonID, currIndex, data));
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");
        }
    }

    private IEnumerator RetrieveStat(int pokemonID, int currIndex, Pokemon_Data refData)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_stat.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req == null)
        {
            Debug.LogError("Retrieve_Req is null.");
        }

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                float hp = float.Parse(retrieve_result[1]);
                float attack = float.Parse(retrieve_result[2]);
                float special_attack = float.Parse(retrieve_result[3]);
                float defense = float.Parse(retrieve_result[4]);
                float special_defense = float.Parse(retrieve_result[5]);
                float speed = float.Parse(retrieve_result[6]); 

                Pokemon_Data newData = 
                new Pokemon_Data(refData.id, refData.spriteID, refData.name, 
                                 refData.type1, refData.type2, 
                                 new Stat(hp, attack, defense, special_attack, special_defense, speed), 
                                 refData.weight, refData.height);
                this.pokeDataHolder.Add(newData);

                this.printPokeData();
                this.printPokeStats();
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");
        }

    }

    private void printPokeData()
    {
        Debug.Log("Pokemon Data Holder Size: " + pokeDataHolder.Count);
        Debug.Log("Pokemon ID: " + pokeDataHolder[0].id);
        Debug.Log("Pokemon Name: " + pokeDataHolder[0].name);
        Debug.Log("Pokemon Type 1: " + pokeDataHolder[0].type1);
        Debug.Log("Pokemon Type 2: " + pokeDataHolder[0].type2);
        Debug.Log("Pokemon Weight: " + pokeDataHolder[0].weight);
        Debug.Log("Pokemon Height: " + pokeDataHolder[0].height);
        Debug.Log("Pokemon Sprite ID: " + pokeDataHolder[0].spriteID);
    }

    private void printPokeStats()
    {
        Debug.Log("Pokemon HP: " + pokeDataHolder[0].baseStats.Health);
        Debug.Log("Pokemon ATK: " + pokeDataHolder[0].baseStats.Attack);
        Debug.Log("Pokemon SP.ATK: " + pokeDataHolder[0].baseStats.Special_Attack);
        Debug.Log("Pokemon DEF: " + pokeDataHolder[0].baseStats.Defense);
        Debug.Log("Pokemon SP.DEF: " + pokeDataHolder[0].baseStats.Special_Defense);
        Debug.Log("Pokemon SPEED: " + pokeDataHolder[0].baseStats.Speed);
    }

    private EType getDecipheredType1(string type)
    {
        if (type == "Normal")
        {
            return EType.NORMAL;
        }
        else if (type == "Fire")
        {
            return EType.FIRE;
        }
        else if (type == "Grass")
        {
            return EType.GRASS;
        }
        else if (type == "Water")
        {
            return EType.WATER;
        }
        else if (type == "Electric")
        {
            return EType.ELECTRIC;
        }
        else if (type == "Ice")
        {
            return EType.ICE;
        }
        else if (type == "Fighting")
        {
            return EType.FIGHTING;
        }
        else if (type == "Poison")
        {
            return EType.POISON;
        }
        else if (type == "Ground")
        {
            return EType.GROUND;
        }
        else if (type == "Flying")
        {
            return EType.FLYING;
        }
        else if (type == "Psychic")
        {
            return EType.PSYCHIC;
        }
        else if (type == "Bug")
        {
            return EType.BUG;
        }
        else if (type == "Rock")
        {
            return EType.ROCK;
        }
        else if (type == "Ghost")
        {
            return EType.GHOST;
        }
        else if (type == "Dragon")
        {
            return EType.DRAGON;
        }
        else if (type == "Dark")
        {
            return EType.DARK;
        }
        else if (type == "Steel")
        {
            return EType.STEEL;
        }
        else if (type == "Fairy")
        {
            return EType.FAIRY;
        }

        return EType.NORMAL;
    }

    private EType? getDecipheredType2(string type)
    {
        if (type == "Normal")
        {
            return EType.NORMAL;
        }
        else if (type == "Fire")
        {
            return EType.FIRE;
        }
        else if (type == "Grass")
        {
            return EType.GRASS;
        }
        else if (type == "Water")
        {
            return EType.WATER;
        }
        else if (type == "Electric")
        {
            return EType.ELECTRIC;
        }
        else if (type == "Ice")
        {
            return EType.ICE;
        }
        else if (type == "Fighting")
        {
            return EType.FIGHTING;
        }
        else if (type == "Poison")
        {
            return EType.POISON;
        }
        else if (type == "Ground")
        {
            return EType.GROUND;
        }
        else if (type == "Flying")
        {
            return EType.FLYING;
        }
        else if (type == "Psychic")
        {
            return EType.PSYCHIC;
        }
        else if (type == "Bug")
        {
            return EType.BUG;
        }
        else if (type == "Rock")
        {
            return EType.ROCK;
        }
        else if (type == "Ghost")
        {
            return EType.GHOST;
        }
        else if (type == "Dragon")
        {
            return EType.DRAGON;
        }
        else if (type == "Dark")
        {
            return EType.DARK;
        }
        else if (type == "Steel")
        {
            return EType.STEEL;
        }
        else if (type == "Fairy")
        {
            return EType.FAIRY;
        }

        return null;
    }
}
