using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {



    int current_continue_tokens;

    public int GetCurrentContinueTokesInt()
    {
        return current_continue_tokens;
    }

    public string GetCurrentContinueTokesString()
    {
        return current_continue_tokens.ToString("n0");
    }

    public void SetCurrentContinueTokens(int continues)
    {
        current_continue_tokens = continues;
        SaveContinueTokens();
    }

    public void UpdateContinueTokens(int continues)
    {
        current_continue_tokens += continues;

        if (current_continue_tokens > my_game_master.continue_tokens_cap)
            current_continue_tokens = my_game_master.continue_tokens_cap;

        if (current_continue_tokens < 0)
            current_continue_tokens = 0;

        SaveContinueTokens();

        if (my_game_master.show_debug_messages)
            Debug.Log("continue tokens = " + current_continue_tokens);
    }





    void Create_new_profile_tokens()
    {
        if (my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token)
            current_continue_tokens = my_game_master.start_continue_tokens;
    }


    public void SaveContinueTokens()
    {

        if (my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token)
            PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_current_continue_tokens", current_continue_tokens);

    }




 
    public void LoadContinueTokens()
    {

        if (my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token)
            current_continue_tokens = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_current_continue_tokens");
    }
    


    public void Delete_this_profile_continueTokens()
    {
            PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_current_continue_tokens");
            current_continue_tokens = 0;

    }
    
   
}
