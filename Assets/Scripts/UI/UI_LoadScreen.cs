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
    [SerializeField] private float rotateSpeed;


    private bool canDisable = false;
    public bool CanDisable { get => canDisable; }

    private void Start()
    {
        loadBar.fillAmount = 0;
    }

    private void Update()
    {
        loadBar.fillAmount += (1/transitionTime) * Time.deltaTime;

        if (loadBar.fillAmount == 1)
        {
            if (BattleManager.instance.loadProgress == 1f)
            {
                Utilities.InvokeAfter(this, holdTime, () => canDisable = true);
            }
            else
            {
                loadBar.rectTransform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            }
        }

    }
}
