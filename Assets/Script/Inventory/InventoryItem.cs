using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;
    public int id;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        id = item.item_ID;
        addToStack();
    }

    public void addToStack()
    {
        stackSize++;
    }

    public void removeFromStack()
    {
        stackSize--;
    }
}
