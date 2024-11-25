using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageScript : MonoBehaviour
{

    [SerializeField] private GameObject gameScene;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private UI_LoadScreen loadScreen;

    private bool hasAssetsLoaded = false;
    private bool loadFinished = false;


    private void Awake()
    {
        loadScreen.gameObject.SetActive(true);
        SetEnabled(false);

        EventBroadcaster.AddObserver(EVENT_NAMES.UI_EVENTS.ON_LOADING_FINISHED, t => {
            hasAssetsLoaded = true;
            SetEnabled(true);
        });
    }
    private void SetEnabled(bool enabled)
    {
        gameScene.SetActive(enabled);
        gameUI.SetActive(enabled);
    }

    private void Update()
    {
        if (!loadFinished && hasAssetsLoaded && loadScreen.CanDisable)
        {
            loadScreen.gameObject.SetActive(false);
            loadFinished = true;
        }
              
    }

}
