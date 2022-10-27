using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoreItem))]
public class StoreItem_Editor : Editor
{
    public override void OnInspectorGUI()
    {

        StoreItem my_target = (StoreItem)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "StoreItem");

        my_target.itemType = (StoreItem.ItemType)EditorGUILayout.EnumPopup("Item Type", my_target.itemType);

        EditorGUI.indentLevel++;

        if (my_target.myItem == null || my_target.myItem.Length == 0)
            my_target.myItem = new StoreItem.ItemInfo[1];

        
        if (my_target.itemType == StoreItem.ItemType.Custom)
            my_target.productType = (StoreItem.ProductType)EditorGUILayout.EnumPopup("productType", my_target.productType);
        else if (my_target.itemType == StoreItem.ItemType.GameCoin
                || my_target.itemType == StoreItem.ItemType.NewLive
                || my_target.itemType == StoreItem.ItemType.ContinueToken)
        {
            my_target.productType = StoreItem.ProductType.Consumable;
        }
        else if (my_target.itemType == StoreItem.ItemType.UnlockWorld
                || my_target.itemType == StoreItem.ItemType.IncrementalItem
                || my_target.itemType == StoreItem.ItemType.NoMoreAds)
        {
            my_target.productType = StoreItem.ProductType.NonConsumable;
        }
        

        if (my_target.itemType == StoreItem.ItemType.IncrementalItem)
        {
            my_target.consumable = null;
            my_target.nonConsumableItem = null;

            my_target.incrementalItem = EditorGUILayout.ObjectField("Incremental item ", my_target.incrementalItem, typeof(IncrementalItem), true) as IncrementalItem;

            if (my_target.incrementalItem != null)
            {
                if (my_target.myItem.Length != my_target.incrementalItem.itemLeves.Length)
                {
                    my_target.myItem = new StoreItem.ItemInfo[my_target.incrementalItem.itemLeves.Length];
                    return;
                }

                ShowPriceCurrency();

                EditorGUI.indentLevel++;
                for (int i = 0; i < my_target.myItem.Length; i++)
                {
                    my_target.myItem[i].myName = my_target.incrementalItem.itemLeves[i].myName;
                    my_target.myItem[i].description = my_target.incrementalItem.itemLeves[i].description;
                    my_target.myItem[i].icon = my_target.incrementalItem.itemLeves[i].icon;

                    EditorGUILayout.LabelField(my_target.myItem[i].myName);
                    GUILayout.Label(AssetPreview.GetAssetPreview(my_target.myItem[i].icon));
                    EditorGUILayout.LabelField(my_target.incrementalItem.itemLeves[i].description);

                    if (my_target.priceCurrency == StoreItem.PriceCurrency.RealMoney)
                        {
                        EditorGUILayout.LabelField("productType = " + my_target.productType);
                        my_target.myItem[i].productID = EditorGUILayout.TextField("productID", my_target.myItem[i].productID);
                        }

                    my_target.myItem[i].price = EditorGUILayout.FloatField("price", my_target.myItem[i].price);

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                }
                EditorGUI.indentLevel--;
            }
        }
        else 
        {
            if (my_target.myItem.Length != 1)
                my_target.myItem = new StoreItem.ItemInfo[1];

            if (my_target.itemType == StoreItem.ItemType.Custom)
                {
                    if (my_target.productType == StoreItem.ProductType.Consumable)
                    {
                        my_target.incrementalItem = null;
                        my_target.nonConsumableItem = null;

                        my_target.consumable = EditorGUILayout.ObjectField("Consumable item ", my_target.consumable, typeof(ConsumableItem), true) as ConsumableItem;

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    if (my_target.consumable != null)
                            {
                            my_target.myItem[0].myName = my_target.consumable.myName;
                            my_target.myItem[0].description = my_target.consumable.description;
                            my_target.myItem[0].icon = my_target.consumable.icon;

      
                                EditorGUILayout.LabelField(my_target.myItem[0].myName);
                                GUILayout.Label(AssetPreview.GetAssetPreview(my_target.myItem[0].icon));
                                EditorGUILayout.LabelField(my_target.myItem[0].description);
                            
                        }
                    }
                    else if (my_target.productType == StoreItem.ProductType.NonConsumable)
                    {
                        my_target.incrementalItem = null;
                        my_target.consumable = null;

                        my_target.nonConsumableItem = EditorGUILayout.ObjectField("Non Consumable item ", my_target.nonConsumableItem, typeof(NonConsumableItem), true) as NonConsumableItem;

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();


                    if (my_target.nonConsumableItem != null)
                            {
                            my_target.myItem[0].myName = my_target.nonConsumableItem.myName;
                            my_target.myItem[0].description = my_target.nonConsumableItem.description;
                            my_target.myItem[0].icon = my_target.nonConsumableItem.icon;

    
                                EditorGUILayout.LabelField(my_target.myItem[0].myName);
                                GUILayout.Label(AssetPreview.GetAssetPreview(my_target.myItem[0].icon));
                                EditorGUILayout.LabelField(my_target.myItem[0].description);
                                
                            }

                    

                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (my_target.consumable != null || my_target.nonConsumableItem != null)
                    ShowShopData();


            }
            else// not custom
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                my_target.myItem[0].myName = EditorGUILayout.TextField("name", my_target.myItem[0].myName);
                my_target.myItem[0].description = EditorGUILayout.TextField("description", my_target.myItem[0].description);

                if (my_target.itemType == StoreItem.ItemType.NoMoreAds)
                    my_target.myItem[0].icon = EditorGUILayout.ObjectField("icon", my_target.myItem[0].icon, typeof(Sprite), true) as Sprite;

                if (my_target.myItem[0] != null)
                    ShowShopData();
            }
        }

        EditorGUI.indentLevel--;

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(my_target);
    }

    void ShowPriceCurrency()
    {
        StoreItem my_target = (StoreItem)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "ShowPriceCurrency");

        my_target.priceCurrency = (StoreItem.PriceCurrency)EditorGUILayout.EnumPopup("currency", my_target.priceCurrency);
        if (my_target.priceCurrency == StoreItem.PriceCurrency.RealMoney)
        {
            my_target.productIDWarning = EditorGUILayout.Foldout(my_target.productIDWarning, "WARNING");
            if (my_target.productIDWarning)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("1- Name, productID, productType and price MUST match the ones in your google/ios... game in app store profile");

                EditorGUILayout.LabelField("2- Because ManuKit support multiple player profiles on the same device");
                EditorGUILayout.LabelField("if you want to have more that a single player profile,");
                EditorGUILayout.LabelField("you MUST create multiple versions of the same item in your google/ios... game in app store profile");
                EditorGUILayout.LabelField("So, you you have here a productID like 'fiveLives' and 3 slots for player profiles,");
                EditorGUILayout.LabelField(" in your google/ios... game in app store profile you MUST create 3 items with these productIDs:");
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("p0_fiveLives");
                EditorGUILayout.LabelField("p1_fiveLives");
                EditorGUILayout.LabelField("p2_fiveLives");
                EditorGUI.indentLevel--;
                EditorGUILayout.LabelField("3- Instead if you want to have just a single player profile");
                EditorGUILayout.LabelField("Home scene > manage_audio > game_master > 'profiles save slots = 1'");
                EditorGUILayout.LabelField("if your productID here is 'fiveLives'");
                EditorGUILayout.LabelField("then one in your google/ios... game in app store profile");
                EditorGUILayout.LabelField("MUST also be 'fiveLives'");
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space();

        }

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(my_target);
    }

    void ShowShopData()
    {
        StoreItem my_target = (StoreItem)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "ShowShopData");

        ShowPriceCurrency();


        if (my_target.itemType != StoreItem.ItemType.Custom && my_target.priceCurrency == StoreItem.PriceCurrency.RealMoney)
            EditorGUILayout.LabelField("productType = " + my_target.productType);

        if (my_target.priceCurrency == StoreItem.PriceCurrency.RealMoney)
            my_target.myItem[0].productID = EditorGUILayout.TextField("productID", my_target.myItem[0].productID);

        my_target.myItem[0].price = EditorGUILayout.FloatField("price", my_target.myItem[0].price);

        if (my_target.itemType != StoreItem.ItemType.NoMoreAds && my_target.itemType != StoreItem.ItemType.UnlockWorld)
            my_target.quantity = EditorGUILayout.IntField("quantity", my_target.quantity);

        if (my_target.itemType == StoreItem.ItemType.UnlockWorld)
            my_target.quantity = EditorGUILayout.IntField("world id", my_target.quantity);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Avaible from:");
        EditorGUILayout.BeginHorizontal();
        my_target.avaibleFromWorld = EditorGUILayout.IntField("world", my_target.avaibleFromWorld);
        my_target.avaibleFromStage = EditorGUILayout.IntField("stage", my_target.avaibleFromStage);
        EditorGUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(my_target);
    }
}
