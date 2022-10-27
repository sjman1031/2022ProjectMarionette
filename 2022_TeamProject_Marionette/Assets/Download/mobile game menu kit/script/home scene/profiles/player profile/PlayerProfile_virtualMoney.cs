using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {


    int current_virtual_money;


    public int GetCurrentVirtualMoneyInt()
    {
        return current_virtual_money;
    }

    public string GetCurrentVirtualMoneyString()
    {
        return current_virtual_money.ToString("n0");
    }

    public void SetCurrentVirtualMoney(int money)
    {
        current_virtual_money = money;
        SaveVirtualMoney();
    }

    public void UpdateCurrentVirtualMoney(int money)
    {
        current_virtual_money += money;

        if (current_virtual_money > my_game_master.my_store_item_manager.virtual_money_cap)
            current_virtual_money = my_game_master.my_store_item_manager.virtual_money_cap;

        if (current_virtual_money < 0)
            current_virtual_money = 0;

        SaveVirtualMoney();

        if (my_game_master.show_debug_messages)
            Debug.Log("virtual money = " + current_virtual_money);
    }


    public void Create_new_profile_virtualMoney()
    {

        current_virtual_money = my_game_master.my_store_item_manager.start_virtual_money;
    }


    public void SaveVirtualMoney()
    {
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_virtual_money", current_virtual_money);
    }


    public void LoadVirtualMoney()
    {
        current_virtual_money = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_virtual_money");

    }



    public void Delete_this_profile_VirtualMoney()
    {

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_virtual_money");
        current_virtual_money = 0;

    }


}
