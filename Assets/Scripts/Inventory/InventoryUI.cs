using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParents;

    public Inventory inventory;

    public GameObject inventoryUI;

    InventorySlot[] slots;

    void Start()
    {
        // Sets inventory to Inventory class
        // Makes it so we dont have to type as much
        // Instance just calls the entire Inventory class
        inventory = Inventory.instance;
        inventory.onItemChagnedcallBack += UpdateUI;

        slots = itemsParents.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        // Created an Input in project settings called Inventory
        // Makes it so the Inventory opens on B and I
        if (Input.GetButtonDown("Inventory"))
        {
            // This just opens the inventory UI we made
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Checks if we have slots avalible
            if (i < inventory.items.Count)
            {
                // We do so add the item to that slot number i
                slots[i].addItem(inventory.items[i]);
            }
            else
            {
                // If we dont clear that slot
                slots[i].clearedSlot();
            }
        }
    }
}
