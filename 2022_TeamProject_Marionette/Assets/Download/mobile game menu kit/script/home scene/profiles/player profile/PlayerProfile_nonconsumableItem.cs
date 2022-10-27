using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {

    [System.Serializable]
    public class MyNonConsumableItem
    {
        public NonConsumableItem item;
        public int databaseSlot;

    }
    List<MyNonConsumableItem> customNonConsumableItems;

    public int GetNonConsumableItemsCount()
    {
        return customNonConsumableItems.Count;
    }


    public MyNonConsumableItem GetNonConsumableItem(int listSlot)
    {
        if (listSlot > customNonConsumableItems.Count)
            return null;

        return customNonConsumableItems[listSlot];
    }

    public MyNonConsumableItem GetNonConsumableItem(NonConsumableItem item)
    {
        for (int i = 0; i < customNonConsumableItems.Count; i++)
        {
            if (customNonConsumableItems[i].item == item)
                return customNonConsumableItems[i];
       }

        return null;
    }


    public void AddNonCustomConsumableItem(NonConsumableItem nonConsumableItem)
    {
        MyNonConsumableItem targetItem = GetNonConsumableItem(nonConsumableItem);

        if (targetItem == null)//if you don't have this item yet
        {
            MyNonConsumableItem tempItem = new MyNonConsumableItem();
            tempItem.item = nonConsumableItem;
            tempItem.databaseSlot = ItemDatabase.THIS.GetNonConsumableItemSlot(nonConsumableItem);

            customNonConsumableItems.Add(tempItem);
        }

    }



    void SaveCustomNonConsumableItems()
    {
        PlayerPrefs.SetInt(profile_slot + "_total_customNonConsumableItems_", customNonConsumableItems.Count);
        for (int i = 0; i < customNonConsumableItems.Count; i++)
        {
            string saveAdress = profile_slot + "_customNonConsumableItems_" + i;
            PlayerPrefs.SetInt(saveAdress + "Slot", customNonConsumableItems[i].databaseSlot);
        }
    }

    void LoadCustomNonConsumableItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_customNonConsumableItems_");
        customNonConsumableItems.Clear();
        for (int i = 0; i < totalItems; i++)
        {
            string loadAdress = profile_slot + "_customNonConsumableItems_" + i;

            MyNonConsumableItem tempItem = new MyNonConsumableItem();
            tempItem.databaseSlot = PlayerPrefs.GetInt(loadAdress + "Slot");

            tempItem.item = ItemDatabase.THIS.nonConsumableItems[tempItem.databaseSlot];

            customNonConsumableItems.Add(tempItem);
        }


        //debug:
        /*
        Debug.Log(profile_name + " - non consumable items: " + customNonConsumableItems.Count);
        for (int i = 0; i < customNonConsumableItems.Count; i++)
        {
            Debug.Log(customNonConsumableItems[i].item.myName);
        }*/
    }


    void DeleteCustomNonConsumableItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_customNonConsumableItems_");
        PlayerPrefs.DeleteKey(profile_slot + "_total_customNonConsumableItems_");

        for (int i = 0; i < totalItems; i++)
        {
            string saveAdress = profile_slot + "_customNonConsumableItems_" + i;
            PlayerPrefs.DeleteKey(saveAdress + "Slot");
        }
    }

}
