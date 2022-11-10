using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject inventorymanager;

    private static InventoryManager instance;

    [SerializeField] public ItemData[] itemDatas;

    public List<InventoryItem> inventory=new List<InventoryItem>();
    //public Dictionary<ItemData, InventoryItem> itemDictionary=new Dictionary<ItemData, InventoryItem>();

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static InventoryManager GetInstance()
    {
        return instance;
    }

    public void Add(ItemData itemData)
    {
        bool found = false;
        int foundindex = 0;
        for(int x = 0; x < inventory.Count; x++)
        {
            if(inventory.ElementAt(x).itemData.item_ID == itemData.item_ID)
            {
                found = true;
                foundindex = x;
            }
            
        }

        if (found)
        {
            inventory.ElementAt(foundindex).addToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
        }
        
        QuestManager qManager = QuestManager.GetInstance();
        if (itemData.item_name == "Guitar pick" &&qManager.checkQuestExist(1)&&qManager.checkQuestInProgress(1)&&qManager.getCurrentProg(1)==2)
        {
            qManager.updateGatherProgressValue(1,getItemStackSize("Guitar pick"));
        }
        if (itemData.item_name == "Shard of Light" && qManager.checkQuestExist(102) && qManager.checkQuestInProgress(102) && qManager.getCurrentProg(102) == 5)
        {
            qManager.updateGatherProgressValue(102, getItemStackSize("Shard of Light"));
        }
        if (itemData.item_name == "Bag of chocolate" && qManager.checkQuestExist(2) && qManager.checkQuestInProgress(2) && qManager.getCurrentProg(2) == 3)
        {
            qManager.updateGatherProgressValue(2, getItemStackSize("Bag of chocolate"));
        }
        if (itemData.item_name == "Shard of Light" && qManager.checkQuestExist(103) && qManager.checkQuestInProgress(103) && qManager.getCurrentProg(103) == 5)
        {
            qManager.updateGatherProgressValue(103, getItemStackSize("Shard of Light"));
        }
    }

    public void Remove(ItemData itemData)
    {
        bool found = false;
        int foundindex = 0;
        for (int x = 0; x < inventory.Count; x++)
        {
            if (inventory.ElementAt(x).itemData.item_ID == itemData.item_ID)
            {
                found = true;
                foundindex = x;
            }

        }

        if (found)
        {
            inventory.ElementAt(foundindex).removeFromStack();
            if (inventory.ElementAt(foundindex).stackSize == 0)
            {
                inventory.RemoveAt(foundindex);
            }
        }



        //if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        //{

        //    item.removeFromStack();
        //    if (item.stackSize == 0)
        //    {
        //        inventory.Remove(item);
        //        itemDictionary.Remove(itemData);
        //    }
        //}
        QuestManager qManager = QuestManager.GetInstance();
        if (itemData.item_name == "Guitar pick" && qManager.checkQuestExist(1) && qManager.checkQuestInProgress(1) && qManager.getCurrentProg(1) == 2)
        {
            qManager.updateGatherProgressValue(1, getItemStackSize("Guitar pick"));
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
                if (inventory.ElementAt(x).itemData.item_type == "use")
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
                if (inventory.ElementAt(x).itemData.item_type == "etc")
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
                if (inventory.ElementAt(x).itemData.item_type == "important")
                {
                    empty = false;
                    break;
                }
            }
        }


        return empty;
    }
    public void updateInventory(List<InventoryItem> inventorylist)
    {
        inventory = inventorylist;
    }

    public InventoryManager getInventoryManager()
    {
        return this;
    }

    public InventoryItem getItemWName(string name) 
    {
        int index=0;

        index = inventory.FindIndex(x => x.itemData.item_name == name);

        return inventory.ElementAt(index);

    } 

    public int getItemStackSize(string name)
    {
        int index = 0;

        index = inventory.FindIndex(x => x.itemData.item_name == name);

        return inventory.ElementAt(index).stackSize;
    }

    public bool checkFulfillReq(string name,int requirement)
    {
        int index = 0;

        index = inventory.FindIndex(x => x.itemData.item_name == name);

        if (inventory.ElementAt(index).stackSize >= requirement)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
