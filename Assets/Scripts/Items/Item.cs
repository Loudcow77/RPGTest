using UnityEngine;

// Adds a new type of object to create in the pull up menu
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; // Name of item

    public Sprite icon = null; // Item Icon

    public bool isDefultItem = false; // Is the Item defult wear?

    public virtual void use()
    {
        // Use the Item
        // Something happens

        Debug.Log("Using " + name);
    }

    public void removeFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
