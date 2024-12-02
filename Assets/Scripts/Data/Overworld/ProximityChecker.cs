using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]   
public class ProximityChecker : MonoBehaviour
{
    public bool isPlayerWithinProximity { get; private set; } = false;
    public Action OnPlayerEnter;
    public Action OnPlayerLeave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerWithinProximity = true;
            OnPlayerEnter?.Invoke();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){ 
            isPlayerWithinProximity = false; 
            OnPlayerLeave?.Invoke();
        }
    }

}
