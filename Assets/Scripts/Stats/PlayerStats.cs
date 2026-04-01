using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // when equipment is changed this is called and puts in the right modifiers
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armour.addModifier(newItem.armourMod);
            damage.addModifier(newItem.damageMod);
        }

        if (oldItem != null)
        {
            armour.removeModifier(oldItem.armourMod);
            damage.removeModifier(oldItem.damageMod);
        }
    }
}
