using UnityEngine;
using System.Collections;

public class StoreManager: MonoBehaviour {

    //virtual money
    public int start_virtual_money;
    public int virtual_money_cap;
    public bool can_buy_virtual_money_with_real_money;

    //store
    //public bool editor_show_store;
    public bool store_enabled;

    [System.Serializable]
    public class ItemContainer
    {
        public string tabName;
        public Sprite tabIcon;
        public bool storeTab = true;

        public StoreItem[] myItems;
    }
    public ItemContainer[] storeContainers;


    public bool show_purchase_feedback;
    public bool show_lives_even_if_cap_reached;
    public bool show_continue_tokens_even_if_cap_reached;
    public bool show_consumable_item_even_if_cap_reached;
    public bool show_incremental_item_even_if_cap_reached;
    public bool show_virtual_money_even_if_cap_reached;
    public string virtual_money_name;

}
