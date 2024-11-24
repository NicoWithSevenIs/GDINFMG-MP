using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadScreen : MonoBehaviour
{

    [SerializeField] private Image loadBar;
    [SerializeField] private float transitionTime;
    [SerializeField] private float holdTime;
    private void Update()
    {
        loadBar.fillAmount += (1/transitionTime) * Time.deltaTime;



        if (loadBar.fillAmount == 1 && BattleManager.instance.loadProgress == 1f)
            Utilities.InvokeAfter(this, holdTime, () => gameObject.SetActive(false));

    }
}
