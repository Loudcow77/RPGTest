using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // makes it so fields show up in inspecter
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public int getValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach (x => finalValue += x) ;
        return finalValue;
    }

    public void addModifier (int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void removeModifier (int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
