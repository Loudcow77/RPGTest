using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track of equipment. Also has functions for adding and removing items.
public class EquipmentManager : MonoBehaviour
{
    #region Singlton
    public static EquipmentManager instance;
    
    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] defultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment; // items we currently have equiped
    SkinnedMeshRenderer[] currentMeshs;

    Inventory inventory; // reference to our inventory

    // Callback for when an item is equiped/unequiped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    void Start()
    {
        inventory = Inventory.instance; // get a reference to our inventory

        // initialize current equipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshs = new SkinnedMeshRenderer[numSlots];
    }

    // equip a new item
    public void Equip(Equipment newItem)
    {
        // find out what item slot it fits in
        int slotIndex = (int)newItem.equipSlot;
        // unequips the previous item that was in that slot
        Equipment oldItem = Unequip(slotIndex);

        // an item was equiped so trigger callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        setEquipmentBlendShapes(newItem, 100);

        // put the item into the slot
        currentEquipment[slotIndex] = newItem;
        // puts the mesh into the gameworld attached to its item
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        // making the mesh move
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        // put in current mesh array
        currentMeshs[slotIndex] = newMesh;
    }

    // unequips items of a particular index
    public Equipment Unequip(int slotIndex)
    {
        // only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // if the equipment is unequiped remove it from model
            if (currentMeshs[slotIndex] != null)
            {
                Destroy(currentMeshs[slotIndex].gameObject);
            }
            // add the item to the inventory
            Equipment oldItem = currentEquipment[slotIndex];
            setEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            // remove item from the equipment array
            currentEquipment[slotIndex] = null;

            // equipment has been changed so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    // unequips all items
    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void setEquipmentBlendShapes (Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegion)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void Update()
    {
        // unequips all items if we press U
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
