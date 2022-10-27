using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MenuKit/Store Item")]

public class StoreItem : ScriptableObject
{
    public bool productIDWarning;//to show the warning text in the editor

    public enum ItemType
    {
        Custom,

        GameCoin,//special consumable

        NewLive,//special consumable
        ContinueToken,//special consumable

        UnlockWorld,//special nonconsumable

        IncrementalItem,//special nonconsumable

        NoMoreAds,//special nonconsumable

    }
    public ItemType itemType;
    public enum ProductType
    {
        Consumable = 0,
        NonConsumable = 1,
    }
    public ProductType productType;
    public enum PriceCurrency
    {
        GameCoin,
        RealMoney
    }
    [Space()]
    public PriceCurrency priceCurrency;

    public ConsumableItem consumable;
    public NonConsumableItem nonConsumableItem;
    public IncrementalItem incrementalItem;

    [System.Serializable]
    public class ItemInfo
    {
        public string myName;
        public Sprite icon;
        [TextArea()]
        public string description;

        public float price;
        public string productID;//for IAP store
   
    }
    public ItemInfo[] myItem;

    public int quantity;
    public int quantityCap;//for custom consumables

    public int avaibleFromWorld;
    public int avaibleFromStage;

    private void OnEnable()
    {
        
    }

    public bool AvaibleFromThisWorldStage(int maxWorldReached, int maxStageReached)
    {
        if (avaibleFromWorld <= maxWorldReached && avaibleFromStage <= maxStageReached)
            return true;

        return false;
    }

    public float GetPrice(int level = 0)
    {
        if (level >= myItem.Length)
            level--;

       return myItem[level].price;
    }

    public Sprite GetIcon(int level = 0)
    {
        if (itemType == ItemType.Custom)
        {
            if (productType == ProductType.Consumable)
                return consumable.icon;

            if (productType == ProductType.NonConsumable)
                return nonConsumableItem.icon;
        }
        else if (itemType == ItemType.IncrementalItem)
        {
            if (level >= incrementalItem.itemLeves.Length)
                level--;

            return incrementalItem.itemLeves[level].icon;
        }

        return myItem[0].icon;
    }

    public Sprite GetMaxIncrementalItemIco()
    {
        return GetIcon(incrementalItem.itemLeves.Length-1);
    }

    public string GetProductID(int level = 0)
    {
        return myItem[level].productID;
    }

    public string GetName(int level = 0)
    {
        if (itemType == ItemType.Custom)
        {
            if (productType == ProductType.Consumable)
                return consumable.myName;

            if (productType == ProductType.NonConsumable)
                return nonConsumableItem.myName;
        }
        else if (itemType == ItemType.IncrementalItem)
        {
            if (level >= incrementalItem.itemLeves.Length)
                level--;

            return incrementalItem.itemLeves[level].myName;
        }


        return myItem[0].myName;
    }

    public string GetDescription(int level = 0)
    {
        if (itemType == ItemType.Custom)
        {
            if (productType == ProductType.Consumable)
                return consumable.description;

            if (productType == ProductType.NonConsumable)
                return nonConsumableItem.description;
        }
        else if (itemType == ItemType.IncrementalItem)
        {
            if (level >= incrementalItem.itemLeves.Length)
                level--;

            return incrementalItem.itemLeves[level].description;
        }

        return myItem[0].description;
    }

    public void ExecuteThisCodeWhenPurchased(game_master my_game_master)
    {
        if (my_game_master.show_debug_messages)
		    Debug.Log("---ExecuteThisCodeWhenPurchased: " + itemType + " = " + name);

		switch(itemType)
			{
			case StoreItem.ItemType.GameCoin:
                my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(quantity);
			break;

            case StoreItem.ItemType.NoMoreAds:
                my_game_master.GetCurrentProfile().SetNoMoreAds(true);
                break;

            case StoreItem.ItemType.NewLive:
                my_game_master.GetCurrentProfile().UpdateLives(quantity);

			break;

			case StoreItem.ItemType.UnlockWorld:
                my_game_master.GetCurrentProfile().PurchaseWorld(quantity);
			break;

			case StoreItem.ItemType.ContinueToken:
                my_game_master.GetCurrentProfile().UpdateContinueTokens(quantity);
			break;

			case StoreItem.ItemType.IncrementalItem:
                my_game_master.GetCurrentProfile().LevelUpIncrementalItem(incrementalItem);
			break;

			case StoreItem.ItemType.Custom:
                if (productType == ProductType.Consumable)
                    my_game_master.GetCurrentProfile().AddCustomConsumableItem(consumable, quantity);
                else if (productType == ProductType.NonConsumable)
                    my_game_master.GetCurrentProfile().AddNonCustomConsumableItem(nonConsumableItem);
                break;
		}

		my_game_master.Save(my_game_master.current_profile_selected);

    }


}
