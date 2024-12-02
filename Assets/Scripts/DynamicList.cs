using UnityEngine;
using UnityEngine.UI;

public class DynamicListController : MonoBehaviour
{
    public GameObject itemPrefab; // Assign the prefab for the list item
    public Transform contentParent; // Assign the content object inside the Scroll View
    public Button addButton; // Assign the button to add new items

    private int itemCount = 0;

    void Start()
    {
        // Add a listener to the button
        addButton.onClick.AddListener(AddItem);
    }

    void AddItem()
    {
        // Instantiate a new item
        GameObject newItem = Instantiate(itemPrefab, contentParent);

        // Manually adjust the position
        RectTransform itemRect = newItem.GetComponent<RectTransform>();
        if (itemRect != null)
        {
            float newY = -itemCount * (itemRect.sizeDelta.y + 10); // Adjust Y by item height + 10px
            itemRect.anchoredPosition = new Vector2(0, newY);
        }

        // Optionally, customize the item (e.g., setting its text)
        Text itemText = newItem.GetComponentInChildren<Text>();
        if (itemText != null)
        {
            itemCount++;
            itemText.text = $"Item {itemCount}";
        }
    }
}
