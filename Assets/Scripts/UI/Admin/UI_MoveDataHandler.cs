using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI_Pooler))]
public class UI_MoveDataHandler : MonoBehaviour
{
    private UI_Pooler pooler;

    private void Awake()
    {
        pooler = GetComponent<UI_Pooler>();

        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED, t => {

       

            var moveDatas = Admin_Ui.instance.moveData_list;
            Debug.Log("m count:" + moveDatas.Count);

            var poolables = pooler.TryGetBatch(moveDatas.Count);

            Debug.Log("p count:"+ poolables.Count);

            for (int i =0; i <poolables.Count; i++)
            {
                var sel = poolables[i].GetComponent<UI_AdminMoveSelector>();
                sel.AssignMoveData(moveDatas[i]);
            }
        });


    }

}
