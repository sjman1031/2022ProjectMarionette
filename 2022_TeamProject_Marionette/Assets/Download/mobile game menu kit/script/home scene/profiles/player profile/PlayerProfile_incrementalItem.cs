using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {

    [System.Serializable]
    public class MyIncremetalItem
    {
        public IncrementalItem item;
        public int level;
        public int databaseSlot;

    }
    List<MyIncremetalItem> customIncrementalItems;

    public int GetIncrementalItemsCount()
    {
        return customIncrementalItems.Count;
    }

    public MyIncremetalItem GetIncrementalItem(int listSlot)
    {
        if (listSlot > customIncrementalItems.Count)
            return null;

        return customIncrementalItems[listSlot];
    }

    public MyIncremetalItem GetIncrementalItem(IncrementalItem item)
    {
        for (int i = 0; i < customIncrementalItems.Count; i++)
        {
            if (customIncrementalItems[i].item == item)
                return customIncrementalItems[i];
       }

        return null;
    }

    public int GetIncrementalItemLevel(IncrementalItem item)
    {
        for (int i = 0; i < customIncrementalItems.Count; i++)
        {
            if (customIncrementalItems[i].item == item)
                return customIncrementalItems[i].level;
        }
    
        return -1;
    }

    void AddIncrementalItem(IncrementalItem incrementalItem)
    {

        MyIncremetalItem tempItem = new MyIncremetalItem();
        tempItem.item = incrementalItem;
        tempItem.databaseSlot = ItemDatabase.THIS.GetIncrementalItemSlot(incrementalItem);

        customIncrementalItems.Add(tempItem);

    }

    public void LevelUpIncrementalItem(IncrementalItem incrementalItem)
    {
        Debug.Log(" -- LevelUpIncrementalItem -- " + incrementalItem.name);
        MyIncremetalItem targetItem = GetIncrementalItem(incrementalItem);

        if (targetItem == null)
            {
            AddIncrementalItem(incrementalItem);
            }
        else
            targetItem.level++;
    }

    public bool IncrementalItemCapReached(IncrementalItem incrementalItem)
    {
        MyIncremetalItem targetItem = GetIncrementalItem(incrementalItem);

        if (targetItem == null)
            return false;

        if (targetItem.level >= targetItem.item.itemLeves.Length)
            return true;

        return false;
    }



    void SaveIncrementalItems()
    {
        PlayerPrefs.SetInt(profile_slot + "_total_incrementalItems_", customIncrementalItems.Count);
        for (int i = 0; i < customIncrementalItems.Count; i++)
        {
            string saveAdress = profile_slot + "_incrementalItems_" + i;
            PlayerPrefs.SetInt(saveAdress + "Slot", customIncrementalItems[i].databaseSlot);
            PlayerPrefs.SetInt(saveAdress + "Level", customIncrementalItems[i].level);
        }
    }

    void LoadIncrementalItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_incrementalItems_");
        customIncrementalItems.Clear();
        for (int i = 0; i < totalItems; i++)
        {
            string loadAdress = profile_slot + "_incrementalItems_" + i;

            MyIncremetalItem tempItem = new MyIncremetalItem();
            tempItem.databaseSlot = PlayerPrefs.GetInt(loadAdress + "Slot");
            tempItem.level = PlayerPrefs.GetInt(loadAdress + "Level");

            tempItem.item = ItemDatabase.THIS.incrementalItems[tempItem.databaseSlot];

            customIncrementalItems.Add(tempItem);
        }

        /*
        //debug:
        Debug.Log(profile_name + " - incremental items: " + customIncrementalItems.Count);
        for (int i = 0; i < customIncrementalItems.Count; i++)
        {
            Debug.Log(customIncrementalItems[i].item.itemLeves[customIncrementalItems[i].level].myName + " = " + customIncrementalItems[i].level);
        }*/
    }

    void DeleteincrementalItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_incrementalItems_");
        PlayerPrefs.DeleteKey(profile_slot + "_total_incrementalItems_");

        for (int i = 0; i < totalItems; i++)
        {
            string saveAdress = profile_slot + "_incrementalItems_" + i;
            PlayerPrefs.DeleteKey(saveAdress + "Slot");
            PlayerPrefs.DeleteKey(saveAdress + "Level");
        }
    }

}
