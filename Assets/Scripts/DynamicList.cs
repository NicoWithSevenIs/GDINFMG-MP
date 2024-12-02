using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VerticalLayoutDynamicPanels : MonoBehaviour
{
    public GameObject parentPanelPrefab; // Prefab of the parent panel
    public GameObject itemPrefab; // Prefab of the list item
    public Transform panelContainer; // Parent transform for all panels
    public Button addButton; // Button to add items
    public Button nextButton; // Button to navigate to the next panel

    private List<GameObject> panels = new List<GameObject>();
    private int currentPanelIndex = 0;
    private int itemCountInCurrentPanel = 0;

    void Start()
    {
        // Create the first panel at the start
        AddNewPanel();

        // Add listeners for the buttons
        addButton.onClick.AddListener(AddItem);
        nextButton.onClick.AddListener(NextPanel);
    }

    void AddNewPanel()
    {
        // Instantiate a new panel from the prefab
        GameObject newPanel = Instantiate(parentPanelPrefab, panelContainer);

        // Ensure the new panel matches the original layout
        newPanel.transform.localPosition = Vector3.zero; // Align position
        newPanel.transform.localScale = Vector3.one; // Reset scale if needed
        newPanel.transform.SetAsLastSibling(); // Ensure it's added at the end

        // Deactivate all other panels
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Activate the new panel
        newPanel.SetActive(true);
        panels.Add(newPanel);

        // Reset the item count for the new panel
        itemCountInCurrentPanel = 0;
    }

    void AddItem()
    {
        // Check if the current panel is full
        if (itemCountInCurrentPanel >= 10)
        {
            AddNewPanel();
            currentPanelIndex = panels.Count - 1; // Switch to the new panel
        }

        // Get the Content object of the current panel
        GameObject currentPanel = panels[currentPanelIndex];
        Transform contentParent = currentPanel.transform.Find("Scroll View/Viewport/Content");

        // Instantiate a new item inside the Content
        GameObject newItem = Instantiate(itemPrefab, contentParent);

        // Optionally, customize the item's text
        Text itemText = newItem.GetComponentInChildren<Text>();
        if (itemText != null)
        {
            int globalItemIndex = (panels.Count - 1) * 10 + itemCountInCurrentPanel + 1;
            itemText.text = $"Item {globalItemIndex}";
        }

        // Update the item count for the current panel
        itemCountInCurrentPanel++;
    }

    void NextPanel()
    {
        if (panels.Count <= 1) return; // No navigation if there's only one panel

        // Deactivate the current panel
        panels[currentPanelIndex].SetActive(false);

        // Move to the next panel (loop back to the first if at the end)
        currentPanelIndex = (currentPanelIndex + 1) % panels.Count;

        // Activate the new current panel
        panels[currentPanelIndex].SetActive(true);
    }
}
