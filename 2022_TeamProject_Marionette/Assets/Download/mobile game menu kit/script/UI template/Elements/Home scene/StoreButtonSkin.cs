using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreButtonSkin : UI_Template
{
    [SerializeField]
    protected store_button my_store_button;

    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myNameText;
    [SerializeField]
    protected TextMeshProUGUI myPriceText;
    [SerializeField]
    protected TextMeshProUGUI myCountText;
    [SerializeField]
    protected TextMeshProUGUI myBuyText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myImage)
            ButtonStatus(UI_Skin.ButtonStatus.On);

        my_store_button.can_buy_ico = currentUISkin.storeItemCanBuyIco;
        my_store_button.cant_buy_ico = currentUISkin.storeItemCantBuyIco;
        my_store_button.virtual_money_ico = currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        my_store_button.real_money_ico = currentUISkin.GetIcon(UI_Skin.Icon.RealMoney);

        //my_store_button.my_ico = GetMyIcon();

        //my_store_button.UpdateSkin();


        UpdateText(myNameText, currentUISkin.storeItemName);
        UpdateText(myPriceText, currentUISkin.storeItemPrice);
        UpdateText(myCountText, currentUISkin.storeItemQuantity);
        UpdateText(myBuyText, currentUISkin.storeItemBuyText);

    }

    public Sprite GetMyIcon()
    {
        if (my_store_button.myStoreItem.itemType == StoreItem.ItemType.NewLive)
            return currentUISkin.GetIcon(UI_Skin.Icon.Lives);
        else if (my_store_button.myStoreItem.itemType == StoreItem.ItemType.GameCoin)
            return currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        else if (my_store_button.myStoreItem.itemType == StoreItem.ItemType.ContinueToken)
            return currentUISkin.GetIcon(UI_Skin.Icon.ContinueToken);
        else if (my_store_button.myStoreItem.itemType == StoreItem.ItemType.UnlockWorld)
            return currentUISkin.GetIcon(UI_Skin.Icon.UnlockWorld);


        return null;

    }

    public void ButtonStatus(UI_Skin.ButtonStatus myStatus)
    {
        if (myImage)
        {
            if (myStatus == UI_Skin.ButtonStatus.On)
                UpdateImage(myImage, currentUISkin.storeItemOn);
            else if (myStatus == UI_Skin.ButtonStatus.Off)
                UpdateImage(myImage, currentUISkin.storeItemOff);
            else if (myStatus == UI_Skin.ButtonStatus.Selected)
                UpdateImage(myImage, currentUISkin.storeItemSelected);
        }
    }
}
