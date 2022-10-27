using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class store_tabs : MonoBehaviour {

    public GameObject tabButton;
    public Transform tabButtonContainer;
    public GameObject tabWindow;
    public Transform tabWindowContainer;

    class MyTab
    {
        public Image buttonAppearance;
        public GameObject window;
        public StoreTabWindow storeTabWindow;
    }
    MyTab[] myTabs;
    int totalActiveTabs;
    int firstActiveTab;
    int lastActiveTab;


    public GameObject storeButton;
    public Info_bar infoBar;
    public feedback_window my_feedback_window;

    public TextMeshProUGUI itemDescriptionText;
    store_button selectedButton;
    public Button confirmPurchaseButton;

    [HideInInspector]public int tab_selected = -1;

	public Sprite selected_tab;
	public Sprite not_selected_tab;

	game_master my_game_master;
	public manage_menu_uGUI my_manage_menu_uGUI;

	// Use this for initialization
	void Start () {

        if (!game_master.game_master_obj)
            return;

            my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
            myTabs = new MyTab[my_game_master.my_store_item_manager.storeContainers.Length];

            confirmPurchaseButton.interactable = false;
            itemDescriptionText.text = "";

            totalActiveTabs = 0;
            firstActiveTab = -1;
            lastActiveTab = 0;
            for (int i = 0; i < my_game_master.my_store_item_manager.storeContainers.Length; i++)
                {
                if (!my_game_master.my_store_item_manager.storeContainers[i].storeTab)
                    continue;

                if (firstActiveTab < 0)
                    firstActiveTab = i;

                totalActiveTabs++;

                lastActiveTab = i;

                MyTab tempTab = new MyTab();

                GameObject tempTabButton = Instantiate(tabButton);
                tempTabButton.transform.SetParent(tabButtonContainer, false);
                tempTabButton.GetComponent<StoreTabButton>().StartMe(i,this, my_game_master.my_store_item_manager.storeContainers[i].tabName, my_game_master.my_store_item_manager.storeContainers[i].tabIcon);
                tempTab.buttonAppearance = tempTabButton.GetComponent<Image>();

                GameObject tempTabWindow = Instantiate(tabWindow);
                tempTabWindow.transform.SetParent(tabWindowContainer, false);
                tempTab.storeTabWindow = tempTabWindow.GetComponent<StoreTabWindow>(); 
                tempTab.storeTabWindow.StartMe(i, my_game_master, this);
                tempTab.window = tempTabWindow;

                myTabs[i] = tempTab;
                }


        Update_buttons_in_windows();
        Select_this_tab(firstActiveTab);
    }



	void Update()
	{
		if (my_game_master.use_pad)
		{
			if (Input.GetKeyDown(my_game_master.pad_next_button))
				Next();
			else if (Input.GetKeyDown(my_game_master.pad_previous_button))
				Previous();

		}

	}
	
	void Next(int numberOfTabsToSkip = 0)
	{
        if (totalActiveTabs < 1)
            return;

        int nextTab = tab_selected + 1 + numberOfTabsToSkip;

        if (nextTab < myTabs.Length)
            {
            if (myTabs[nextTab] != null)
                Select_this_tab(nextTab);
            else
                Next(numberOfTabsToSkip+1);
            }
        else
			Select_this_tab(firstActiveTab);
		
		
	}
	
	void Previous(int numberOfTabsToSkip = 0)
	{
        if (totalActiveTabs < 1)
            return;

        int previousTab = tab_selected - 1 - numberOfTabsToSkip;

        if (previousTab >= 0)
            {
            if (myTabs[previousTab] != null)
                Select_this_tab(previousTab);
            else
                Previous(numberOfTabsToSkip + 1);
            }
        else
            Select_this_tab(lastActiveTab);
            
    }
	

	public void Select_this_tab(int tab_id)
	{
        if (myTabs == null)
            return;

		if (tab_selected != tab_id)
			{

            DeselectAllButtonsInCurrentTab();

			if (tab_selected >= 0)
				my_game_master.Gui_sfx(my_game_master.tap_sfx);

			for (int i = 0; i < myTabs.Length; i++)
				{
                if (myTabs[i] == null)
                    continue;

                myTabs[i].window.SetActive(false);
                myTabs[i].buttonAppearance.sprite = not_selected_tab;
				}

			myTabs[tab_id].window.SetActive(true);
			myTabs[tab_id].buttonAppearance.sprite = selected_tab;
			tab_selected = tab_id;

			if (!my_game_master)
				my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");

			if (my_game_master.use_pad)
				Invoke("Focus_on_first_button_of_this_window",0.1f);

			}
	}

    public void DeselectAllButtonsInCurrentTab()
        {
        if (tab_selected < 0)
            return;

        myTabs[tab_selected].storeTabWindow.DeselectAllMyButtons();
        }

    public void SelectThisButton(store_button thisButton)
    {
        if (thisButton == null)
        {
            itemDescriptionText.text = "";
            confirmPurchaseButton.interactable = false;
            return;
        }

        selectedButton = thisButton;
        itemDescriptionText.text = selectedButton.myStoreItem.GetDescription();

        confirmPurchaseButton.interactable = true;
    }

    public void ConfirmPurchase()
    {
        selectedButton.PurchaseNow();
    }

    void Focus_on_first_button_of_this_window()
	{
        if (myTabs[tab_selected].window.transform.GetChild(0).transform.GetChild(0).childCount < 1)
            return;

		GameObject target = myTabs[tab_selected].window.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
		if (target)
			my_manage_menu_uGUI.Mark_this_button(target);
	}

	public void Update_buttons_in_windows()
	{
        if (myTabs == null)
            return;

		for (int i = 0; i < myTabs.Length; i++)
        {
            if (myTabs[i] == null)
                continue;

            myTabs[i].storeTabWindow.Update_buttons();
        }
    }
}
