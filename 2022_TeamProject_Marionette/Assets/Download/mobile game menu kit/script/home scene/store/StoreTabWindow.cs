using UnityEngine;
using System.Collections;

public class StoreTabWindow : MonoBehaviour {

    public Transform targetContainer;

    int myTabId;

	Info_bar my_info_bar;
	store_button[] my_buttons;
	int total_buttons;


	GameObject store_button_obj;
	bool store_button_generated;
	game_master my_game_master;
	store_tabs my_store_tabs;
	manage_menu_uGUI my_manage_menu_uGUI;
	feedback_window my_feedback_window;

	public void StartMe (int _myTabId, game_master _my_game_master, store_tabs _my_store_tabs) {

        myTabId = _myTabId;
        my_game_master = _my_game_master;
        my_store_tabs = _my_store_tabs;

        store_button_obj = _my_store_tabs.storeButton;
        my_manage_menu_uGUI = my_store_tabs.my_manage_menu_uGUI;
        my_info_bar = my_store_tabs.infoBar;
        my_feedback_window = my_store_tabs.my_feedback_window;



        Generate_items(my_game_master.my_store_item_manager.storeContainers[myTabId].myItems);

        total_buttons = targetContainer.childCount;
		my_buttons = new store_button[total_buttons];
		for (int i = 0; i < total_buttons; i++)
			{
			my_buttons[i] = targetContainer.GetChild(i).GetComponent<store_button>();
			my_buttons[i].my_store_tabs = my_store_tabs;
			my_buttons[i].my_manage_menu_uGUI = my_manage_menu_uGUI;
			my_buttons[i].my_feedback_window = my_feedback_window;
			}

	}

    public void DeselectAllMyButtons()
    {
        for (int i = 0; i < total_buttons; i++)
            my_buttons[i].DeselectMe();
    }

    public void Update_buttons()
		{
		my_info_bar.Update_me();
		for (int i = 0; i < total_buttons; i++)
			{
			my_buttons[i].Update_me();
			}
		}
   
    void Generate_items(StoreItem[] myItems)
    {
        if (game_master.game_master_obj && !store_button_generated)
        {
            for (int i = 0; i < myItems.Length; i++)
            {
                //check if this item is avaible at this phase of the game
                if (myItems[i].AvaibleFromThisWorldStage(my_game_master.GetMaxWorldReached(), my_game_master.GetMaxStageReached()))
                {
                    GameObject temp_button = (GameObject)Instantiate(store_button_obj);
                    temp_button.transform.SetParent(targetContainer, false);

                    store_button button_script = temp_button.GetComponent<store_button>();

                    //button_script.my_item_ID = i;
                    button_script.my_game_master = my_game_master;
                    //button_script.show_quantity = true;

                    int currentItemLevel = 0;
                    if (myItems[i].itemType == StoreItem.ItemType.IncrementalItem)
                    {
                        currentItemLevel = my_game_master.GetCurrentProfile().GetIncrementalItemLevel(myItems[i].incrementalItem)+1;
                        //if this item not is at the maximum
                        /*
                        if (currentItemLevel < myItems[i].incrementalItem.itemLeves.Length)
                        {
                            //level
                            button_script.show_quantity = true;
                        }
                        else // max level
                        {
                            button_script.show_quantity = false;
                            print(myItems[i].name +  " max level");
                            //button_script.Incremental_item_MAX();
                        }*/
                    }

                    button_script.My_start(myItems[i], currentItemLevel/*, myTabId, i*/);

                }
            }
        }

        store_button_generated = true;
    }

}
