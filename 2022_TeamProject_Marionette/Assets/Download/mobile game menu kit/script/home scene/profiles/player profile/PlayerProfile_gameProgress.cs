using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {

    
    public int current_world;//what is the last world played by the player 
    public int total_number_of_stages_in_the_game_solved;
    public bool[,] dot_tail_turn_on;//[world,stage]//for map
    public int latest_world_played;
    public int latest_stage_played;
    public int play_this_stage_to_progress_in_the_game_world;
    public int play_this_stage_to_progress_in_the_game_stage;
    public bool all_stages_solved;
    public bool[] world_playable;//[world]
    public bool[,] stage_playable; //[world,stage]
    public bool[,] stage_solved; //[world,stage]
    bool[] world_purchased;//[world]

    //star score
    public int[,] stage_stars_score; //[world,stage]                      
    public int[,] stage_stars_cap_score; //[world,stage]
    public int[] star_score_in_this_world;//[world]
    public int stars_total_score;

    //int score
    public int[,] best_int_score_in_this_stage; //[world,stage]
    public int best_int_score_for_current_player;
    


    public void InitiateMeGameProgress()
    {

        //game progress
        int totalWorlds = my_game_master.total_stages_in_world_n.Length;
        world_playable = new bool[totalWorlds];
        dot_tail_turn_on = new bool[totalWorlds, my_game_master.max_stages_in_a_world];
        stage_playable = new bool[totalWorlds, my_game_master.max_stages_in_a_world];
        stage_solved = new bool[totalWorlds, my_game_master.max_stages_in_a_world];

        if (my_game_master.my_store_item_manager)
            world_purchased = new bool[totalWorlds];

        //score
        stage_stars_score = new int[totalWorlds, my_game_master.max_stages_in_a_world];
        star_score_in_this_world = new int[totalWorlds];
        stage_stars_cap_score = new int[totalWorlds, my_game_master.max_stages_in_a_world];
        best_int_score_in_this_stage = new int[totalWorlds, my_game_master.max_stages_in_a_world];

    }

    void Create_new_profile_progress()
    {
        stars_total_score = 0;
        total_number_of_stages_in_the_game_solved = 0;
        all_stages_solved = false;


        for (int i = 0; i < my_game_master.total_stages_in_world_n.Length; i++)
        {
            if (my_game_master.this_world_is_unlocked_after_selected[i] == game_master.this_world_is_unlocked_after.start)
            {
                world_playable[i] = true;
                world_purchased[i] = false;
                stage_playable[i, 0] = true;
            }

        }

        play_this_stage_to_progress_in_the_game_world = 0;
        play_this_stage_to_progress_in_the_game_stage = 0;

    }

    public bool WorldPurchased(int world_n)
    {
        return world_purchased[world_n];
    }

    public void PurchaseWorld(int world_n)
    {
        world_purchased[world_n] = true;
        PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_array_W" + world_n.ToString() + "_" + "world_purchased"), Convert.ToInt32(world_purchased[world_n]));
        UnlockWorld(world_n);
    }

    public void UnlockWorld(int world_n)
    {
        int stage = 0;
        world_playable[world_n] = true;
        stage_playable[world_n, stage] = true;

        PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_array_W" + world_n.ToString() + "_" + "world_unlocked"), Convert.ToInt32(world_playable[world_n]));
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world_n.ToString() + "S" + stage.ToString() + "_" + "stages_unlocked", Convert.ToInt32(stage_playable[world_n, stage]));

        if (my_game_master.my_manage_menu_uGUI)
            my_game_master.my_manage_menu_uGUI.Update_profile_name(true);//this update also world and stage screen to show the new world unlock
    }


    void SaveGameProgress()
    {
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_all_stages_solved", Convert.ToInt32(all_stages_solved));
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_latest_stage_played_world", latest_world_played);
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_latest_stage_played_stage", latest_stage_played);

        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_total_number_of_stages_in_the_game_solved", total_number_of_stages_in_the_game_solved);

        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_play_this_world_to_progress", play_this_stage_to_progress_in_the_game_world);
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_play_this_stage_to_progress", play_this_stage_to_progress_in_the_game_stage);

        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_total_stars", stars_total_score);
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_best_int_score_for_this_profile", best_int_score_for_current_player);

        for (int world = 0; world < my_game_master.total_stages_in_world_n.Length; world++)
        {
            PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_unlocked"), Convert.ToInt32(world_playable[world]));
            PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_purchased"), Convert.ToInt32(world_purchased[world]));
            PlayerPrefs.SetInt(("profile_" + profile_slot.ToString() + "_star_score_in_this_world"), star_score_in_this_world[world]);

            for (int stage = 0; stage < my_game_master.total_stages_in_world_n[world]; stage++)
            {
                //bool arrays
                PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stages_unlocked", Convert.ToInt32(stage_playable[world, stage]));
                PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_solved", Convert.ToInt32(stage_solved[world, stage]));
                PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "dots", Convert.ToInt32(dot_tail_turn_on[world, stage]));

                //in array
                PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_score", stage_stars_score[world, stage]);
                PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_int_score", best_int_score_in_this_stage[world, stage]);

                if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
                    PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_cap_score", stage_stars_cap_score[world, stage]);

            }
        }
    }



    public void LoadGameProgress()
    {

        best_int_score_for_current_player = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_best_int_score_for_this_profile");
        stars_total_score = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_total_stars");

        total_number_of_stages_in_the_game_solved = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_total_number_of_stages_in_the_game_solved");
        all_stages_solved = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_all_stages_solved"));
        latest_world_played = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_latest_stage_played_world");
        latest_stage_played = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_latest_stage_played_stage");


        play_this_stage_to_progress_in_the_game_world = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_play_this_world_to_progress");
        play_this_stage_to_progress_in_the_game_stage = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_play_this_stage_to_progress");

        for (int world = 0; world < my_game_master.total_stages_in_world_n.Length; world++)
        {

            world_purchased[world] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_purchased"));
            star_score_in_this_world[world] = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_star_score_in_this_world");

            for (int stage = 0; stage < my_game_master.total_stages_in_world_n[world]; stage++)
            {
                //array bool
                stage_playable[world, stage] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stages_unlocked"));
                stage_solved[world, stage] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_solved"));
                dot_tail_turn_on[world, stage] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "dots"));

                //array int
                stage_stars_score[world, stage] = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_score");
                best_int_score_in_this_stage[world, stage] = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_int_score");

                if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
                    stage_stars_cap_score[world, stage] = PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_cap_score");

            }

            if (PlayerPrefs.HasKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_unlocked"))
                world_playable[world] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_unlocked"));
            else
            {
                if (my_game_master.this_world_is_unlocked_after_selected[world] == game_master.this_world_is_unlocked_after.start)
                {
                    world_playable[world] = true;
                    stage_playable[world, 0] = true;
                }
            }
        }


    }




    public void Delete_this_profile_GameProgress()
    {

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_total_stars");
        stars_total_score = 0;
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_total_number_of_stages_in_the_game_solved");
        total_number_of_stages_in_the_game_solved = 0;

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_all_stages_solved");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_latest_stage_played_world");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_latest_stage_played_stage");
        all_stages_solved = false;

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_play_this_world_to_progress");
        play_this_stage_to_progress_in_the_game_world = 0;

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_play_this_stage_to_progress");
        play_this_stage_to_progress_in_the_game_stage = 0;


        for (int world = 0; world < my_game_master.total_stages_in_world_n.Length; world++)
        {
            PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "_" + "world_unlocked");
            world_playable[world] = false;
            for (int stage = 0; stage < my_game_master.total_stages_in_world_n[world]; stage++)
            {
                //array bool
                PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stages_unlocked");
                stage_playable[world, stage] = false;
                PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_solved");
                stage_solved[world, stage] = false;

                dot_tail_turn_on[world, stage] = false;


                //array int
                PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_score");
                stage_stars_score[world, stage] = 0;

                if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
                {
                    PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_array_W" + world.ToString() + "S" + stage.ToString() + "_" + "stage_stars_cap_score");
                    stage_stars_cap_score[world, stage] = 0;
                }

            }
        }


    }

}
