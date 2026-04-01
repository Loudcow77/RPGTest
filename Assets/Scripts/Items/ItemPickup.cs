using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        pickUp();
    }

    // For picking up items
    void pickUp()
    {
        Debug.Log("You picked up a " + item.name);
        // Adds the item to the inventory
        bool wasPickedUp = Inventory.instance.Add(item);
        // Destroys the item when its picked up
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
