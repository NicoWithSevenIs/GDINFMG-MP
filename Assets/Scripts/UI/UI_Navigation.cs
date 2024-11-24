using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Navigation : MonoBehaviour
{
    [SerializeField] private GameObject rootMenu;
    [SerializeField] private GameObject moveMenu;
    [SerializeField] private GameObject switchMenu;

    private void Awake()
    {
        ReturnToRoot();
    }

    public void ReturnToRoot()
    {
        rootMenu.SetActive(true);
        moveMenu.SetActive(false);
        switchMenu.SetActive(false);
    }

    public void InvokeMoveMenu()
    {
        rootMenu.SetActive(false);
        moveMenu.SetActive(true);
        switchMenu.SetActive(false);
    }

    public void InvokeSwitchMenu()
    {
        rootMenu.SetActive(false);
        moveMenu.SetActive(false);
        switchMenu.SetActive(true);
    }

}
