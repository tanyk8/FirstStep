using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject inventorymanager;

    private static InventoryManager instance;

    public List<InventoryItem> inventory=new List<InventoryItem>();
    public Dictionary<ItemData, InventoryItem> itemDictionary=new Dictionary<ItemData, InventoryItem>();

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

    public bool checkInventoryEmpty(string type)
    {
        bool empty=true;

        if (type =="all")
        {
            if (inventory.Count!=0)
            {
                empty = false;
            }
        }
        else if (type == "use")
        {
            for(int x = 0; x < inventory.Count; x++)
            {
                if (inventory.ElementAt(x).itemData.itemType == "use")
                {
                    empty = false;
                    break;
                }
            }
        }
        else if (type == "etc")
        {
            for (int x = 0; x < inventory.Count; x++)
            {
                if (inventory.ElementAt(x).itemData.itemType == "etc")
                {
                    empty = false;
                    break;
                }
            }
        }
        else if (type == "important")
        {
            for (int x = 0; x < inventory.Count; x++)
            {
                if (inventory.ElementAt(x).itemData.itemType == "important")
                {
                    empty = false;
                    break;
                }
            }
        }


        return empty;
    }


    public void updateInventory(List<InventoryItem> inventorylist, Dictionary<ItemData, InventoryItem> itemdictionary)
    {
        inventory = inventorylist;
        itemDictionary = itemdictionary;
    }

    public InventoryManager getInventoryManager()
    {
        return this;
    }
    //shard of courage
    //mental potion (overuse will give debuff)
}
