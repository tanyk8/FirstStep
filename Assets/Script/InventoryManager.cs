using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private static InventoryManager instance;

    public List<InventoryItem> inventory=new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary=new Dictionary<ItemData, InventoryItem>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Inventory Manager in the scene");
        }
        instance = this;
    }

    public static InventoryManager GetInstance()
    {
        return instance;
    }

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData,out InventoryItem item))
        {
            item.addToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);

        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.removeFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

    //shard of courage
    //mental potion (overuse will give debuff)
}
