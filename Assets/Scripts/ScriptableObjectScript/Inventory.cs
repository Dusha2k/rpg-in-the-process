using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> inventory = new List<Item>();
    public int numberOfKeys;

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!inventory.Contains(itemToAdd))
            {
                inventory.Add(itemToAdd);
            }
        }
    }
}
