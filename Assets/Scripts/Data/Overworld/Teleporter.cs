using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PositionManager actable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = target.position;
            if (actable != null)
                actable.Act();
        }
            
    }
}
