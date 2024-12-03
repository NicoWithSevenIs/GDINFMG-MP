using UnityEngine;
using UnityEngine.UI;

public class DynamicListController : MonoBehaviour
{
    public GameObject itemPrefab; // Prefab for the list item
    public RectTransform contentParent; // RectTransform of the content object inside the Scroll View
    public Button addButton; // Button to add new items

    private int itemCount = 0; // Counter for the number of items

    void Start()
    {
        // Add a listener to the button
        addButton.onClick.AddListener(AddItem);
    }

    void AddItem()
    {
        // Instantiate a new item as a child of the content parent
        GameObject newItem = Instantiate(itemPrefab, contentParent);

        // Customize the item (e.g., setting its text)
        Text itemText = newItem.GetComponentInChildren<Text>();
        if (itemText != null)
        {
            itemCount++;
            itemText.text = $"Item {itemCount}";
        }

        // Optionally: You can adjust properties of the Vertical Layout Group here if needed
    }
}
