using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class RetrieveMoveData : MonoBehaviour
{
    public List<int> movePoolIDs = new List<int>();
    public List<MoveData> chosenMoves = new List<MoveData>();
    public bool hasRetrievedMovePool = false;
    public bool hasRetrievedMoves = false;
    public void callRetrieveMovePool(int pokemonID)
    {
        StartCoroutine(RetrievePokeMovePool(pokemonID));
    }
    
    public IEnumerator RetrievePokeMovePool(int pokemonID)
    {
        WWWForm form = new WWWForm();
        form.AddField("pokemonID", pokemonID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move_pool.php", form);
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
                movePoolIDs.Add(int.Parse(retrieve_result[1]));
                movePoolIDs.Add(int.Parse(retrieve_result[2]));
                movePoolIDs.Add(int.Parse(retrieve_result[3]));
                movePoolIDs.Add(int.Parse(retrieve_result[4]));
                movePoolIDs.Add(int.Parse(retrieve_result[5]));
                movePoolIDs.Add(int.Parse(retrieve_result[6]));
                this.hasRetrievedMovePool = true;
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
    
    public void resetMoveIDs()
    {
       this.movePoolIDs.Clear();    
    }

    public void callRetrieveMove(int pokemonID)
    {
        StartCoroutine(RetrievePokeMoveData(pokemonID));
    }

    private IEnumerator RetrievePokeMoveData(int moveID)
    {
        WWWForm form = new WWWForm();
        form.AddField("moveID", moveID);

        UnityWebRequest retrieve_req = UnityWebRequest.Post("http://localhost/retrieve_move.php", form);
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
                string name = retrieve_result[1];
                string description = retrieve_result[2];
                int power = int.Parse(retrieve_result[5]);
                EType moveType = this.getDecipheredType1(retrieve_result[3]);
                EMoveType moveGroup = this.getDecipheredMoveType(retrieve_result[4]);
                MoveData newMove = new MoveData(name, description, power, 100, moveType, moveGroup);
                this.chosenMoves.Add(newMove);
                Debug.Log("Move Name: " + chosenMoves[0].name);
                Debug.Log("Move Desc: " + chosenMoves[0].description);
                Debug.Log("Move Power: " + chosenMoves[0].power);
                Debug.Log("Move Type: " + chosenMoves[0].type);
                Debug.Log("Move Group: " + chosenMoves[0].moveType);
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

    private EMoveType getDecipheredMoveType(string moveGroup)
    {
        if (moveGroup == "Physical")
        {
            return EMoveType.PHYSICAL;
        }
        else if (moveGroup == "Special")
        {
            return EMoveType.SPECIAL;
        }
        else if (moveGroup == "Status")
        {
            return EMoveType.STATUS;
        }

        return EMoveType.STATUS;
    }
}
