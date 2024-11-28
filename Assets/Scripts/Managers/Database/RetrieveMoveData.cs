using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

public class RetrieveMoveData : MonoBehaviour
{
    public DB_Utility_Helper utilHelper;
    public List<int> movePoolIDs = new List<int>();
    public List<MoveData> chosenMoves = new List<MoveData>();
    
    public void retrieveMovePool(string[] retrieve_result)
    {
        movePoolIDs.Add(int.Parse(retrieve_result[1]));
        movePoolIDs.Add(int.Parse(retrieve_result[2]));
        movePoolIDs.Add(int.Parse(retrieve_result[3]));
        movePoolIDs.Add(int.Parse(retrieve_result[4]));
        movePoolIDs.Add(int.Parse(retrieve_result[5]));
        movePoolIDs.Add(int.Parse(retrieve_result[6]));

        //this.printMovePoolIDs();
    }

    public void retrieveMoveData(string[] retrieve_result, int moveID)
    {
        string name = retrieve_result[1];
        string description = retrieve_result[2];
        int power = int.Parse(retrieve_result[5]);
        EType moveType = utilHelper.getDecipheredType1(retrieve_result[3]);
        EMoveType moveGroup = utilHelper.getDecipheredMoveType(retrieve_result[4]);
        MoveData newMove = new MoveData(name, description, power, 100, moveType, moveGroup);
        this.chosenMoves.Add(newMove);

        //Move move = MoveManager.GetMove(moveID);
        //move.Data = newMove;
        //this.printMoveData();
    }


    private void printMovePoolIDs()
    {
        foreach (int id in movePoolIDs)
        {
            Debug.Log("id: " + id);
        }
    }

    private void printMoveData()
    {
        foreach (MoveData move in chosenMoves) {
            Debug.Log("Move Name: " + move.name);
            Debug.Log("Move Desc: " + move.description);
            Debug.Log("Move Power: " + move.power);
            Debug.Log("Move Type: " + move.type);
            Debug.Log("Move Group: " + move.moveType);
        }
       
    }
}
