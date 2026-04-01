using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    // Putting Item item lets us access the Item class in a variable called item
    Item item;

    // This puts an item into an inventory slot
    public void addItem(Item newItem)
    {
        // Changes the base item into a newItem thats in the inventory
        item = newItem;

        // All stuff within the bag like making the X button work
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // This is for when there is nothing in a slot
    public void clearedSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // This makes the X button in the inventory delete the item from the bag
    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    // Calls our use fuction that was in a different class
    public void useItem()
    {
        item.use();
    }
}
