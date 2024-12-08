using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ViewMovesNavigation : MonoBehaviour
{

    [SerializeField] private Button viewMovesButton;
    [SerializeField] private Button viewPokemonButton;

    [SerializeField] private CanvasGroup viewMovesPanel;
    [SerializeField] private CanvasGroup viewPokemonPanel;

    private void Awake()
    {
        viewMovesPanel.gameObject.SetActive(true);
        viewPokemonPanel.gameObject.SetActive(true);
        SelectPanel(0);
    }

    public void SelectPanel(int index)
    {
        index = Mathf.Clamp(index, 0, 1);

        void SetActive(bool isActive, Button b, CanvasGroup g)
        {
            b.interactable = !isActive;
            Utilities.SetUIActive(g, isActive);
        }

        SetActive(index == 0, viewMovesButton, viewMovesPanel);
        SetActive(index == 1, viewPokemonButton, viewPokemonPanel);

    }


}
