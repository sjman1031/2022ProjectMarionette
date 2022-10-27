using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {

    game_master my_game_master;


    public string profile_name;
    public int profile_slot;


    public void InitiateMe(int _profile_slot, game_master _my_game_master)
    {
        my_game_master = _my_game_master;
        profile_slot = _profile_slot;

        InitiateMeOptions();

        InitiateMeGameProgress();


        //store/inventory
        if (my_game_master.my_store_item_manager)
        {
            customIncrementalItems = new List<MyIncremetalItem>();
            customNonConsumableItems = new List<MyNonConsumableItem>();
            customConsumableItems = new List<MyConsumableItem>();
        }

    }


    public void Create_new_profile(string my_name, game_master _my_game_master)
    {
        profile_name = my_name;
        my_game_master = _my_game_master;

        Create_new_profile_progress();

        Create_new_profile_lives();
        Create_new_profile_tokens();

        Create_new_profile_virtualMoney();

        my_game_master.Save(profile_slot);
    }


    public void SaveAll()
    {

        PlayerPrefs.SetString("profile_" + profile_slot.ToString() + "_name", profile_name);

        SaveGameProgress();

        SaveLives();
        SaveContinueTokens();

        SaveVirtualMoney();

        SaveCustomConsumableItems();
        SaveCustomNonConsumableItems();
        SaveIncrementalItems();

        Save_options();
        
    }


    public void Load()
    {
        profile_name = PlayerPrefs.GetString("profile_" + profile_slot.ToString() + "_name");

        LoadGameProgress();

        LoadLives();
        LoadContinueTokens();

        LoadVirtualMoney();

        LoadCustomConsumableItems();
        LoadCustomNonConsumableItems();
        LoadIncrementalItems();

        Load_options();
        LoadNoMoreAds();
    }



    public void Delete_this_profile()
    {
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_name");

        Delete_this_profile_VirtualMoney();

        Delete_this_profile_options();

        Delete_this_profile_lives();
        Delete_this_profile_continueTokens();

        Delete_this_profile_GameProgress();

        DeleteCustomNonConsumableItems();
        DeleteCustomConsumableItems();
        DeleteincrementalItems();

    }


}
