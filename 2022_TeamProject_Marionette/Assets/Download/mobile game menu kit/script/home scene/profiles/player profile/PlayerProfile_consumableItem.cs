using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {

    [System.Serializable]
    public class MyConsumableItem
    {
        public ConsumableItem item;
        public int quantity;
        public int databaseSlot;

    }
    List<MyConsumableItem> customConsumableItems;

    public int GetConsumableItemsCount()
    {
        return customConsumableItems.Count;
    }

    public MyConsumableItem GetConsumableItem(int listSlot)
    {
        if (listSlot > customConsumableItems.Count)
            return null;

        return customConsumableItems[listSlot];
    }

    public MyConsumableItem GetConsumableItem(ConsumableItem item)
    {
        for (int i = 0; i < customConsumableItems.Count; i++)
        {
            if (customConsumableItems[i].item == item)
                return customConsumableItems[i];
       }

        return null;
    }

    public int GetConsumableItemQuantity(ConsumableItem item)
    {
        for (int i = 0; i < customConsumableItems.Count; i++)
        {
            if (customConsumableItems[i].item == item)
                return customConsumableItems[i].quantity;
        }

        return 0;
    }

    public int GetConsumableItemQuantity(int listSlot)
    {
        return customConsumableItems[listSlot].quantity;
    }

    public void AddCustomConsumableItem(ConsumableItem consumableItem, int quantity)
    {
        MyConsumableItem targetItem = GetConsumableItem(consumableItem);

        if (targetItem == null)//if you don't have this item yet
        {
            MyConsumableItem tempItem = new MyConsumableItem();
            tempItem.item = consumableItem;
            tempItem.databaseSlot = ItemDatabase.THIS.GetConsumableItemSlot(consumableItem);
            tempItem.quantity = quantity;

            //tempItem.storeContainerSlot = storeContainerSlot;
            //tempItem.storeItemSlot = storeItemSlot;

            customConsumableItems.Add(tempItem);
        }
        else //just add the new quantity
        {
            targetItem.quantity += quantity;
            /*
            if (targetItem.currentQuantity > targetItem.storeItem.quantityCap)
                targetItem.currentQuantity = targetItem.storeItem.quantityCap;*/

            if (targetItem.quantity <= 0)
                customConsumableItems.Remove(targetItem);
        }

    }



    void SaveCustomConsumableItems()
    {
        PlayerPrefs.SetInt(profile_slot + "_total_customConsumableItems_", customConsumableItems.Count);
        for (int i = 0; i < customConsumableItems.Count; i++)
        {
            string saveAdress = profile_slot + "_customConsumableItems_" + i;
            PlayerPrefs.SetInt(saveAdress + "Slot", customConsumableItems[i].databaseSlot);
            PlayerPrefs.SetInt(saveAdress + "Quantity", customConsumableItems[i].quantity);
        }
    }

    void LoadCustomConsumableItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_customConsumableItems_");
        customConsumableItems.Clear();
        for (int i = 0; i < totalItems; i++)
        {
            string loadAdress = profile_slot + "_customConsumableItems_" + i;

            MyConsumableItem tempItem = new MyConsumableItem();
            tempItem.databaseSlot = PlayerPrefs.GetInt(loadAdress + "Slot");
            tempItem.quantity = PlayerPrefs.GetInt(loadAdress + "Quantity");

            tempItem.item = ItemDatabase.THIS.consumableItems[tempItem.databaseSlot];

            customConsumableItems.Add(tempItem);
        }


        //debug:
        /*
        Debug.Log(profile_name + " - consumable items: " + customConsumableItems.Count);
        for (int i = 0; i < customConsumableItems.Count; i++)
        {
            Debug.Log(customConsumableItems[i].item.myName + " = " + customConsumableItems[i].quantity);
        }*/
    }

    void DeleteCustomConsumableItems()
    {
        int totalItems = PlayerPrefs.GetInt(profile_slot + "_total_customConsumableItems_");
        PlayerPrefs.DeleteKey(profile_slot + "_total_customConsumableItems_");

        for (int i = 0; i < totalItems; i++)
        {
            string saveAdress = profile_slot + "_customConsumableItems_" + i;
            PlayerPrefs.DeleteKey(saveAdress + "Slot");
            PlayerPrefs.DeleteKey(saveAdress + "Quantity");
        }
    }

}
