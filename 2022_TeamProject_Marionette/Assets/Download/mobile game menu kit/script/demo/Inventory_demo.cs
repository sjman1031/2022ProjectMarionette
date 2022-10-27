using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_demo : MonoBehaviour
{

    public Item.Kind inventoryShowThisKindOfItems;
    public Transform container;
    public GameObject inventoryItemUI;
    game_master my_game_master;

    // Start is called before the first frame update
    void Start()
    {
        my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
        RefreshInventory();


    }

    void RefreshInventory()
    {
        if (my_game_master == null)
            return;

       

        int itemCount = 0;

        if (inventoryShowThisKindOfItems == Item.Kind.Consumable)
            itemCount = my_game_master.GetCurrentProfile().GetConsumableItemsCount();
        else if (inventoryShowThisKindOfItems == Item.Kind.NonConsumable)
            itemCount = my_game_master.GetCurrentProfile().GetNonConsumableItemsCount();
        else if (inventoryShowThisKindOfItems == Item.Kind.Incremental)
            itemCount = my_game_master.GetCurrentProfile().GetIncrementalItemsCount();

        print("RefreshInventory: " + itemCount);

        for (int i = 0; i < itemCount; i++)
            CreateIcon(inventoryShowThisKindOfItems, i);
            

    }

    void CreateIcon(Item.Kind _itemKind, int _itemId)
    {
        GameObject temp = Instantiate(inventoryItemUI);
        temp.transform.SetParent(container);
        temp.GetComponent<item_demo>().StartMe(my_game_master, _itemKind, _itemId);
    }
}
