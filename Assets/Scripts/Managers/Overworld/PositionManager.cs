using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private List<Transform> checkpoints;

    public void Act()
    {
        PlayerManager.currentFloor++;
    }

    private void Start()
    {
        if(PlayerManager.currentFloor > 0 && PlayerManager.currentFloor <= 3)
            player.position = checkpoints[PlayerManager.currentFloor-1].position;
    }
}
