using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Makes it so only one class is initalized
    #region Singlton
    public static Inventory instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void onItemChanged();
    public onItemChanged onItemChagnedcallBack;

    // The amount of items our inventory can hold
    public int space = 20;
    // Creates a list called items for all our new items being added
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        // Check if its not a defult item if its not proceed
        if (!item.isDefultItem)
        {
            // Check if there is space in the bag
            // If not then dont run if there is proceed
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            // Add the item to the items list
            items.Add(item);

            if (onItemChagnedcallBack != null)
                onItemChagnedcallBack.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        // Removes the item from the items list
        items.Remove(item);

        if (onItemChagnedcallBack != null)
            onItemChagnedcallBack.Invoke();
    }
}
