using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject generateDonePrompt;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.generateDonePrompt.SetActive(false);
    }

    public void clickedgenerateDonePromptOk()
    {
        this.generateDonePrompt.SetActive(false);
    }

    public void openGenerateDonePrompt()
    {
        this.generateDonePrompt.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal()
    {
        Debug.Log("All pokemon healed");
    }

    public void PC()
    {
        Debug.Log("Open PC");
    }
}
