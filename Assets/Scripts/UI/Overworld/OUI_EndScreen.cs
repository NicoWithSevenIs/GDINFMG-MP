using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OUI_EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private AudioSource audioSource;


    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;

    private void Awake()
    {
        gameOverText.text = PlayerManager.currentFloor == 4 ? "Victory!" : "Haha Skill Issue";
        audioSource.clip = PlayerManager.currentFloor == 4 ? win : lose;
        audioSource.Play();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Derek Scene");
    }
}
