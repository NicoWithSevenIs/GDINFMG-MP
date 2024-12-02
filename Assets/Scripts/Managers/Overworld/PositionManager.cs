using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private List<Transform> checkpoints;

    public void Act()
    {
        PlayerManager.currentFloor++;
        Debug.Log("Current Floor: " + PlayerManager.currentFloor);
    }

    private void Start()
    {
        Debug.Log("Current Floor: " + PlayerManager.currentFloor);
        if (PlayerManager.currentFloor > 0 && PlayerManager.currentFloor <= 3)
            player.position = checkpoints[PlayerManager.currentFloor - 1].position;
        else if (PlayerManager.currentFloor == 4 || PlayerManager.currentFloor == -1)
            SceneManager.LoadScene("EndScene");


    }
}
