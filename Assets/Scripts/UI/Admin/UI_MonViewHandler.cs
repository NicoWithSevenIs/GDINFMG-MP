using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI_Pooler))]
public class UI_MonViewHandler : MonoBehaviour
{
    private UI_Pooler pooler;

    private void Awake()
    {
        pooler = GetComponent<UI_Pooler>();

        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED, t => {



            var monDatas = Admin_Ui.instance.pokemonData_list;
            var poolables = pooler.TryGetBatch(monDatas.Count);

            for (int i = 0; i < poolables.Count; i++)
            {
                var sel = poolables[i].GetComponent<UI_MonView>();
                sel.AssignMon(i + 1, monDatas[i]);
            }
        });


    }
}
