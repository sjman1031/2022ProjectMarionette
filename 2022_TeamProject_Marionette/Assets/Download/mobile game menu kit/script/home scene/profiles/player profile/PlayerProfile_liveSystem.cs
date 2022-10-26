using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {


    //live system
    int current_lives;
    public DateTime target_time;
    public bool recharge_live_countdown_active;// for get lives when player have zero lives
    public bool extra_live_countdown_active;//for get extra lives when player have already some live


    public int GetCurrentLivesInt()
    {
        return current_lives;
    }

    public string GetCurrentLivesString()
    {
        return current_lives.ToString("n0");
    }

    public void SetCurrentLives(int lives)
    {
        current_lives = lives;
        SaveLives();
    }

    public void UpdateLives(int lives)
    {
        current_lives += lives;

        if (current_lives > my_game_master.live_cap)
            current_lives = my_game_master.live_cap;

        if (current_lives < 0)
            current_lives = 0;

        SaveLives();

        if (my_game_master.show_debug_messages)
            Debug.Log("lives = " + current_lives);

    }


    void Create_new_profile_lives()
    {
        if (!my_game_master.infinite_lives)
            current_lives = my_game_master.start_lives;

        if (my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token)
            current_continue_tokens = my_game_master.start_continue_tokens;
    }


    public void SaveLives()
    {
        if (my_game_master.infinite_lives)
            return;
        
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_current_lives", current_lives);
        PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_" + "recharge_live_countdown_active"), Convert.ToInt32(recharge_live_countdown_active));

        if (recharge_live_countdown_active)
            PlayerPrefs.SetString("profile_" + profile_slot.ToString() + "_target_time", target_time.ToString());
            
        PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_" + "extra_live_countdown_active"), Convert.ToInt32(extra_live_countdown_active));

    }




 
    public void LoadLives()
    {

        if (!my_game_master.infinite_lives)
        {
            current_lives = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_current_lives");
            recharge_live_countdown_active = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_" + "recharge_live_countdown_active"));
            if (recharge_live_countdown_active)
            {
                string temp_string = PlayerPrefs.GetString("profile_" + profile_slot.ToString() + "_target_time");
                target_time = DateTime.Parse(temp_string);
                my_game_master.Check_countdown();
            }
            extra_live_countdown_active = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_" + "extra_live_countdown_active"));
        }

    }
    


    public void Delete_this_profile_lives()
    {
            PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_current_lives");
            current_lives = 0;
            PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_" + "recharge_live_countdown_active");
            recharge_live_countdown_active = false;
            extra_live_countdown_active = false;

            PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_target_time");


    }
    
   
}
