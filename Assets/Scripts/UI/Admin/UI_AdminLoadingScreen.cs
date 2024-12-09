using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AdminLoadingScreen : MonoBehaviour
{

    [SerializeField] private GameObject loadScreen;

    [SerializeField] private RectTransform rotator;
    [SerializeField] private float minRotationDuration;
    [SerializeField] private float rotationSpeed;

    [SerializeReference] private Admin_Ui loadable;

    private float rotateDuration = 0;

    private void Awake()
    {
        loadScreen.SetActive(true);
    }

    private void Update()
    {
        rotateDuration += Time.deltaTime;
        if (!loadable.hasLoaded || rotateDuration < minRotationDuration)
            rotator.Rotate(Vector3.forward * rotationSpeed);
        else loadScreen.SetActive(false);

    }


}
