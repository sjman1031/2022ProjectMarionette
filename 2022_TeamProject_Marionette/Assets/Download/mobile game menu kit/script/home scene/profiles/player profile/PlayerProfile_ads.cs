using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {


    bool noAdsPurchased;

    public void SetNoMoreAds(bool purchased)
    {
        noAdsPurchased = purchased;
        my_game_master.my_ads_master.noAdsPurchased = noAdsPurchased;
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_NoMoreAds_purchased", Convert.ToInt32(noAdsPurchased));
    }

    void LoadNoMoreAds()
    {
        noAdsPurchased = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_NoMoreAds_purchased"));
        my_game_master.my_ads_master.noAdsPurchased = noAdsPurchased;
    }

    void Delete_this_profile_NoMoreAds()
    {
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_NoMoreAds_purchased");
        noAdsPurchased = false;
        my_game_master.my_ads_master.noAdsPurchased = noAdsPurchased;
    }
}
