using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class store_button : MonoBehaviour {

    [HideInInspector] public StoreItem myStoreItem;
    int currentItemLevel;

    [HideInInspector]public Sprite my_ico;

	bool disable_me_after_purchased;
	bool purchased;

    bool you_have_enough_money;
	bool this_buy_hit_the_cap;//so disable it

    [HideInInspector] public Sprite can_buy_ico;
    [HideInInspector] public Sprite cant_buy_ico;
    [HideInInspector] public Sprite virtual_money_ico;
    [HideInInspector] public Sprite real_money_ico;

    public TextMeshProUGUI my_name_tx;
    public TextMeshProUGUI my_price_tx;
    public TextMeshProUGUI my_quantity_tx;
    public TextMeshProUGUI my_buy_tx;
	public Image my_ico_img;
	public Image my_buy_ico_img;
	public Image my_money_ico_img;
    public StoreButtonSkin mySkin;


    [HideInInspector] public game_master my_game_master;
    [HideInInspector] public manage_menu_uGUI my_manage_menu_uGUI;
    [HideInInspector] public store_tabs my_store_tabs;
    [HideInInspector] public feedback_window my_feedback_window;



	public void My_start(StoreItem _myStoreItem, int _currentItemLevel)
	{

		if (game_master.game_master_obj && my_game_master == null)
			my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");


        myStoreItem = _myStoreItem;

        currentItemLevel = _currentItemLevel;

        if (Check_if_show_this_button())
			{
            if (my_game_master.show_debug_messages)
			    Debug.Log(myStoreItem.itemType + " " + myStoreItem.productType/* + " in " + storeContainerSlot + "," + storeItemSlot*/);
			my_name_tx.text = myStoreItem.GetName(currentItemLevel);

            my_ico = GetComponent<StoreButtonSkin>().GetMyIcon();
            if (my_ico == null)
                my_ico = myStoreItem.GetIcon(currentItemLevel);

            my_ico_img.sprite = my_ico;

			if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.RealMoney)
				{
				my_money_ico_img.sprite = real_money_ico;
				you_have_enough_money = true;
				}
			else if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.GameCoin)
				{
				my_money_ico_img.sprite = virtual_money_ico;
                Check_if_you_have_enough_virtual_money();
				}

            //my_price_tx.text = myStoreItem.myItem[currentItemLevel].price.ToString();
            my_price_tx.text = myStoreItem.GetPrice(currentItemLevel).ToString();


            Check_if_this_purchase_dont_hit_the_cap();

			Show_quantity();
			Show_buy_ico();

            UpdateSkin();

            }
		else
			this.gameObject.SetActive(false);

	}

    public void UpdateSkin()
    {

        if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.RealMoney)
            my_money_ico_img.sprite = real_money_ico;
        else if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.GameCoin)
            my_money_ico_img.sprite = virtual_money_ico;

        my_name_tx.text = myStoreItem.GetName(currentItemLevel);
        my_price_tx.text = myStoreItem.GetPrice(currentItemLevel).ToString();

        Show_quantity();
        Show_buy_ico();

        if (my_ico)
            my_ico_img.sprite = my_ico;

    }


    void Show_buy_ico()
	{
		if (!this_buy_hit_the_cap && you_have_enough_money)
			my_buy_ico_img.sprite = can_buy_ico;
		else
			my_buy_ico_img.sprite = cant_buy_ico;
	}

	public void Incremental_item_MAX()
	{
		Debug.Log("Incremental_item_MAX(): " + myStoreItem.name);
		my_quantity_tx.gameObject.SetActive(true);
		my_quantity_tx.text = "MAX";
		my_buy_tx.text = "MAX";
        //my_ico = my_game_master.my_store_item_manager.incrementalItems[my_item_ID].itemlevels[my_game_master.my_store_item_manager.incrementalItems[my_item_ID].itemlevels.Count - 1].icon;
        my_ico = myStoreItem.GetMaxIncrementalItemIco();
        my_price_tx.gameObject.SetActive(false);
		this_buy_hit_the_cap = true;

	}

	void Show_quantity()
	{

		if (myStoreItem.quantity > 1)
		{
			my_quantity_tx.gameObject.SetActive(true);
			my_quantity_tx.text = myStoreItem.quantity.ToString();
		}
		else
			my_quantity_tx.gameObject.SetActive(false);
		
		if (this_buy_hit_the_cap)
			{
			my_buy_tx.text = "MAX";
			if (myStoreItem.itemType == StoreItem.ItemType.IncrementalItem)
				my_quantity_tx.gameObject.SetActive(true);
			}
		else
			my_buy_tx.text = "Buy";
	}

	void Check_if_this_purchase_dont_hit_the_cap()
	{

		switch(myStoreItem.itemType)
		{
		case StoreItem.ItemType.GameCoin:
			if ((my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyInt() + myStoreItem.quantity) > my_game_master.my_store_item_manager.virtual_money_cap)
				this_buy_hit_the_cap = true;
			else
				this_buy_hit_the_cap = false;
			break;

		case StoreItem.ItemType.NewLive:
			if ((my_game_master.GetCurrentProfile().GetCurrentLivesInt() + myStoreItem.quantity) > my_game_master.live_cap)
				this_buy_hit_the_cap = true;
			else
				this_buy_hit_the_cap = false;
			break;

		case StoreItem.ItemType.ContinueToken:
			if ((my_game_master.GetCurrentProfile().GetCurrentContinueTokesInt() + myStoreItem.quantity) > my_game_master.continue_tokens_cap)
				this_buy_hit_the_cap = true;
			else
				this_buy_hit_the_cap = false;
			break;

		case StoreItem.ItemType.Custom:
            if (myStoreItem.productType == StoreItem.ProductType.Consumable)
                {
                    //print(myStoreItem.name + " custom consumable: " + my_game_master.GetCurrentProfile().GetConsumableItemQuantity(myStoreItem.consumable) + " + " +  myStoreItem.quantity + " > " + myStoreItem.quantityCap);
                if ((my_game_master.GetCurrentProfile().GetConsumableItemQuantity(myStoreItem.consumable) + myStoreItem.quantity) > myStoreItem.quantityCap)
                    this_buy_hit_the_cap = true;
			    else
				    this_buy_hit_the_cap = false;
                }
            else if (myStoreItem.productType == StoreItem.ProductType.NonConsumable)
                {
                    //print(myStoreItem.name + " custom nonconsumable");
                }

            break;

            case StoreItem.ItemType.IncrementalItem:
                //print(myStoreItem.name + " incremental level: " + my_game_master.GetCurrentProfile().GetIncrementalItemLevel(myStoreItem.incrementalItem));
                this_buy_hit_the_cap = my_game_master.GetCurrentProfile().IncrementalItemCapReached(myStoreItem.incrementalItem);

            break;

        }
	}

	bool Check_if_show_this_button()
	{

		bool my_check = true;

		switch(myStoreItem.itemType)
		{
		case StoreItem.ItemType.GameCoin:
			if ((myStoreItem.quantity > my_game_master.my_store_item_manager.virtual_money_cap)
				|| (!my_game_master.my_store_item_manager.show_virtual_money_even_if_cap_reached && (my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyInt() + myStoreItem.quantity > my_game_master.my_store_item_manager.virtual_money_cap)))
				{
				this.gameObject.SetActive(false);
				my_check = false;
				}

			break;

		case StoreItem.ItemType.NewLive://check if you risk to hit the live cap
			if ((my_game_master.infinite_lives)
				||((myStoreItem.quantity > my_game_master.live_cap)
				   || (!my_game_master.my_store_item_manager.show_lives_even_if_cap_reached && (my_game_master.GetCurrentProfile().GetCurrentLivesInt() + myStoreItem.quantity > my_game_master.live_cap))))
				{
				this.gameObject.SetActive(false);
				my_check = false;
				}

			break;

		case StoreItem.ItemType.UnlockWorld: //this button will be disable afther purchase
			if (my_game_master.this_world_is_unlocked_after_selected[myStoreItem.quantity] == game_master.this_world_is_unlocked_after.bui_it)
			{
				disable_me_after_purchased = true;
				if (my_game_master.GetCurrentProfile().WorldPurchased(myStoreItem.quantity))
				{
					this.gameObject.SetActive(false);
					purchased = true;
					my_check = false;
				}
			}
			else 
				my_check = false;
			break;

		case StoreItem.ItemType.ContinueToken:
			if (my_game_master.infinite_lives || (my_game_master.continue_rule_selected != game_master.continue_rule.continue_cost_a_continue_token) || (my_game_master.my_ads_master.whenContinueScreenAppear.thisAdIsEnabled) 
				|| (myStoreItem.quantity > my_game_master.continue_tokens_cap)
				    || (!my_game_master.my_store_item_manager.show_continue_tokens_even_if_cap_reached && (my_game_master.GetCurrentProfile().GetCurrentContinueTokesInt() + myStoreItem.quantity > my_game_master.continue_tokens_cap)))
			{
				this.gameObject.SetActive(false);
				my_check = false;
			}

			break;


        case StoreItem.ItemType.IncrementalItem:
			if 	(!my_game_master.my_store_item_manager.show_incremental_item_even_if_cap_reached 
			&& (my_game_master.GetCurrentProfile().GetIncrementalItemLevel(myStoreItem.incrementalItem) >= myStoreItem.myItem.Length))
			{
				this.gameObject.SetActive(false);
				my_check = false;
			}
			break;

            case StoreItem.ItemType.Custom:
                if (myStoreItem.productType == StoreItem.ProductType.Consumable)
                {
                    if 	(!my_game_master.my_store_item_manager.show_consumable_item_even_if_cap_reached 
				    && ((my_game_master.GetCurrentProfile().GetConsumableItemQuantity(myStoreItem.consumable) + myStoreItem.quantity) > myStoreItem.quantityCap))
			        {
				        this.gameObject.SetActive(false);
				        my_check = false;
			        }
                }
                else if (myStoreItem.productType == StoreItem.ProductType.NonConsumable)
                {
                }
                break;
            case StoreItem.ItemType.NoMoreAds:
                if (my_game_master.my_ads_master.enable_ads)
                {
                    if (my_game_master.my_ads_master.noAdsPurchased)
                        my_check = false;
                    else
                        my_check = true;
                }
                else
                    my_check = false;
                
                break;
        }

		return my_check;
	}

	void Check_if_you_have_enough_virtual_money()
	{
		if (myStoreItem.GetPrice(currentItemLevel) > my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyInt())
		{
			you_have_enough_money = false;
		}
		else
		{
			you_have_enough_money = true;
		}
	}

    [HideInInspector] bool selected; 
	public void Click_me () {

        if (selected)
            DeselectMe();
        else
            SelectMe();

    }

    public void DeselectMe()
    {
        selected = false;
        mySkin.ButtonStatus(UI_Skin.ButtonStatus.On);
        my_store_tabs.SelectThisButton(null);
    }

    void SelectMe()
    {
        my_store_tabs.DeselectAllButtonsInCurrentTab();
        selected = true;
        mySkin.ButtonStatus(UI_Skin.ButtonStatus.Selected);
        my_store_tabs.SelectThisButton(this);
    }

    public void PurchaseNow()
    {
        if (you_have_enough_money && !this_buy_hit_the_cap)
        {

            my_game_master.Gui_sfx(my_game_master.tap_sfx);
            if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.RealMoney)
                Pay_with_real_money();
            else if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.GameCoin)
                Pay_with_virtual_money();
        }
        else
        {
            my_game_master.Gui_sfx(my_game_master.tap_error_sfx);
        }

        my_store_tabs.SelectThisButton(null);
        mySkin.ButtonStatus(UI_Skin.ButtonStatus.On);
    }

    void Pay_with_real_money()
	{
    #if UNITY_PURCHASING
        string productID = myStoreItem.GetProductID();


        if (my_game_master.show_debug_messages)
			Debug.Log("Pay_with_real_money - id: " + productID);

            IAPManager.THIS.SetCurrentStoreButton(this);
            IAPManager.THIS.BuyProductID(productID);
    #endif
    }

	void Pay_with_virtual_money()
	{
		if (my_game_master.show_debug_messages)
			Debug.Log("Pay_with_virtual_money");

            my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(-Mathf.RoundToInt(myStoreItem.GetPrice(currentItemLevel)));
			Give_the_stuff();
			
	}

	public void Give_the_stuff()
	{
        myStoreItem.ExecuteThisCodeWhenPurchased(my_game_master);

        if (my_game_master.my_store_item_manager.show_purchase_feedback)
            my_feedback_window.Start_me(my_ico, myStoreItem.quantity, myStoreItem.GetName(currentItemLevel));

        DeselectMe();
        my_store_tabs.Update_buttons_in_windows();
        purchased = true;

    }
	

	public void Update_me()
	{
 
		if (myStoreItem.itemType == StoreItem.ItemType.IncrementalItem)
		{

            currentItemLevel = my_game_master.GetCurrentProfile().GetIncrementalItemLevel(myStoreItem.incrementalItem)+1;

            my_quantity_tx.text = myStoreItem.quantity.ToString();

            if (my_game_master.GetCurrentProfile().IncrementalItemCapReached(myStoreItem.incrementalItem))
			{
				Incremental_item_MAX();
			}
			else
			{
                my_buy_tx.text = "Buy";
                my_ico = myStoreItem.GetIcon(currentItemLevel);
				my_price_tx.text = myStoreItem.GetPrice(currentItemLevel).ToString();
			}
			my_ico_img.sprite = my_ico;
		}


		if (disable_me_after_purchased && purchased)
			{
			this.gameObject.SetActive(false);
			return;
			}

		if (myStoreItem.priceCurrency == StoreItem.PriceCurrency.GameCoin)
			{
			Check_if_you_have_enough_virtual_money();
			Check_if_this_purchase_dont_hit_the_cap();
			}
		else
			Check_if_this_purchase_dont_hit_the_cap();

		Check_if_show_this_button();
		Show_quantity();
		Show_buy_ico();


	}
}
