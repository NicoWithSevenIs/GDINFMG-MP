using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Derek Scene");
    }

    public void Admin()
    {
        SceneManager.LoadScene("Admin");
    }

    public void AdminPlayer()
    {
        SceneManager.LoadScene("Admin Player");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");

    }

}
