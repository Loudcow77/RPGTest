using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; // slot to store equipment in
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegion;

    public int armourMod; // increase/decrease in armour
    public int damageMod; // increase/decrease in attack

    public override void use()
    {
        base.use();
        EquipmentManager.instance.Equip(this); // equip it
        removeFromInventory(); // remove it from the inventory
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso } // corresponds with blendshapes