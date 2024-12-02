using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RetrievePlayerData : MonoBehaviour
{   
    public DB_Utility_Helper utilHelper;
    public List<float> chosenInstances = new List<float>();
    public List<Pokemon> PartyMons = new List<Pokemon>();

    public void sendToPlayerManager(string[] retrieve_result)
    {
        int playerID = int.Parse(retrieve_result[1]);
        int pokemonID = int.Parse(retrieve_result[2]);

        ESex eSex = utilHelper.getDeicpheredSex(retrieve_result[3]);

        string nature = retrieve_result[4];

        int[] moveSet = new int[4];
        moveSet[0] = int.Parse(retrieve_result[5]);
        moveSet[1] = int.Parse(retrieve_result[6]);
        moveSet[2] = int.Parse(retrieve_result[7]);
        moveSet[3] = int.Parse(retrieve_result[8]);


        float hpIV = float.Parse(retrieve_result[9]);
        float atkIV = float.Parse(retrieve_result[10]);
        float spatkIV = float.Parse(retrieve_result[11]);
        float defIV = float.Parse(retrieve_result[12]);
        float spdefIV = float.Parse(retrieve_result[13]);
        float speedIV = float.Parse(retrieve_result[14]);

        Stat IV = new Stat(hpIV, atkIV, defIV, spatkIV, spdefIV, speedIV);

        Stat EV = new Stat(85, 85, 85, 85, 85, 85);

        float baseHP = float.Parse(retrieve_result[15]);
        float baseAtk = float.Parse(retrieve_result[16]);
        float baseSpAtk = float.Parse(retrieve_result[17]);
        float baseDef = float.Parse(retrieve_result[18]);
        float baseSpDef = float.Parse(retrieve_result[19]);
        float baseSpeed = float.Parse(retrieve_result[20]);


        Stat baseStat = new Stat(baseHP, baseAtk, baseDef, baseSpAtk, baseSpDef, baseSpeed);

        string pokemonName = retrieve_result[21];

        EType type1 = utilHelper.getDecipheredType1(retrieve_result[22]);
        EType? type2 = utilHelper.getDecipheredType2(retrieve_result[23]);

        float weight = float.Parse(retrieve_result[24]);
        float height = float.Parse(retrieve_result[25]);

        int spriteID = int.Parse(retrieve_result[26]);  

        Pokemon_Data data = new Pokemon_Data(pokemonID, spriteID, pokemonName, type1, type2, baseStat, weight, height);
        Pokemon pokemon = new Pokemon(playerID, data, eSex, IV, EV, nature, moveSet);

        float instanceID = float.Parse(retrieve_result[27]);

        //Debug.Log("instanceid: " + retrieve_result[27]);
        
        chosenInstances.Add(instanceID);

        PlayerManager.party.Add(pokemon);
        PartyMons.Add(pokemon);

        StartCoroutine(registerMoves(moveSet));
        
    }

    public IEnumerator registerMoves(int[] moveset)
    {
        yield return StartCoroutine(RetrievePokeMoveData(moveset[0]));
        yield return StartCoroutine(RetrievePokeMoveData(moveset[1]));
        yield return StartCoroutine(RetrievePokeMoveData(moveset[2]));
        yield return StartCoroutine(RetrievePokeMoveData(moveset[3]));
    }

    public IEnumerator RetrievePokeMoveData(int moveID)
    {
        WWWForm form = new WWWForm();
        form.AddField("moveID", moveID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move.php", form);
        yield return retrieve_req.SendWebRequest();

        if (retrieve_req.result == UnityWebRequest.Result.Success)
        {
            string[] retrieve_result = retrieve_req.downloadHandler.text.Split('\t');
            if (retrieve_result[0].Contains("Success"))
            {
                string name = retrieve_result[1];
                string description = retrieve_result[2];
                int power = int.Parse(retrieve_result[5]);
                EType moveType = utilHelper.getDecipheredType1(retrieve_result[3]);
                EMoveType moveGroup = utilHelper.getDecipheredMoveType(retrieve_result[4]);
                MoveData newMove = new MoveData(name, description, power, 100, moveType, moveGroup);
                
                MoveManager.assignMoveData(moveID, newMove);
            }
            else
            {
                Debug.Log("Retrieval of move pool failed.");
            }
        }
        else
        {
            Debug.LogError("Web Request for RetrievePokeData faled.");

        }

    }
}
