using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class game_master : MonoBehaviour {


	//editor
	public bool editor_show_worlds;
	public bool editor_show_lives;
	public bool editor_show_audio;
	public bool editor_show_debug;
		public bool show_debug_messages;
		public bool show_debug_warnings;
	public bool editor_show_pad;
	//public bool editor_show_store;
	public bool editor_show_score;

    public manage_menu_uGUI my_manage_menu_uGUI;

    //device button work like back button in every screen, except home screen. In home screen it close the app (this behavior is REQUIRED for winphone store)
    public bool allow_ESC;
	public bool show_loading_screen;

	public string home_scene_name;

	//score
	public string score_name;
	public string what_say_if_new_stage_record;
	public string what_say_if_new_personal_record;
	public string what_say_if_new_device_record;
	public bool show_star_score;
        public enum Star_score_rule
            {
            Classic3Stars,
            EachStageHaveHisOwnStarCap
            }
        public Star_score_rule star_score_rule = Star_score_rule.Classic3Stars;
        public bool show_progres_bar;
	public bool show_int_score;
	public bool show_int_score_rank;
	public bool show_int_score_stage_record_in_game_stage;
	public bool show_score_in_lose_screen_too;

	//ads
	public ads_master my_ads_master;
    //store
    public StoreManager my_store_item_manager;


    //pad input
    public bool use_pad;
	public Color normal_button_color;
	public Color highlighted_button_color;
	public KeyCode pad_start_button;
	public KeyCode pad_ok_button;
	public KeyCode pad_back_button;
	public KeyCode pad_next_button;
	public KeyCode pad_previous_button;
	public KeyCode pad_pause_button;

    public PlayerProfile[] playerProfiles;
    public PlayerProfile GetCurrentProfile()
    {
        return playerProfiles[current_profile_selected];
    }

    //lives
    public bool infinite_lives;
	public string lives_name;
	public int start_lives;
    public int live_cap;
    public int gain_an_extra_live_each_x_minutes;
    public int live_cap_for_lives_gained_with_timer;
	public enum lose_lives
	{
		in_game, //like platform games
		when_show_lose_screen //like puzzle games or 
	}
	public lose_lives lose_lives_selected;


	//continues
	public bool refresh_stage_and_world_screens;
	public int start_continue_tokens;
	public int continue_tokens_cap;
	public bool continue_menu_have_countdown;
	public int continue_menu_countdown_seconds;
		public enum continue_rule
		{
			never_continue,
			infinite_continues,
			continue_cost_a_continue_token
			//continue_cost_virtual_money
			
		}
		public continue_rule continue_rule_selected;
		public int continue_cost_virtual_money;
	
		public enum if_player_not_continue
		{
			restart_from_W1_Stage_1,
			restart_from_current_world_Stage_1,
			restart_from_current_world_and_current_stage

		}
		public if_player_not_continue if_player_not_continue_selected;

	public enum when_restart
	{
		give_immediately_new_lives,
		give_lives_after_countdown
		
	}
	public when_restart when_restart_selected;
	public int if_not_continue_restart_with_lives;

	public bool if_not_continue_lose_gained_stars;
		public int wait_for_days;
		public int wait_for_hours;
		public int wait_for_minutes;
		public int wait_for_seconds;
    public enum TimerStatus
    {
        Off,
        countdown,
        done
    }
    public TimerStatus timerStatus = TimerStatus.Off;

    public enum if_player_continue
	{
		restart_from_current_world_Stage_1,
		restart_from_current_world_and_current_stage,
		continue_playing_this_stage
		
	}
	public if_player_continue if_player_continue_selected;
	public int continue_give_new_lives;


    //worlds and stages
    public bool use_same_scene_for_all_stages_in_the_same_world;
    public int current_stage;
    public int max_stages_in_a_world;//this set the array capacity (it is set automatically, don't touch this!)
	[SerializeField]
	public int[] total_stages_in_world_n;//how many game scenes has your game in every world
		public int total_number_of_stages_in_the_game;
	public string[] world_name;
	public enum this_world_is_unlocked_after
	{
		start,
		previous_world_is_finished,
		reach_this_star_score,
		bui_it

	}
	public this_world_is_unlocked_after[] this_world_is_unlocked_after_selected;
	[SerializeField]
	public int[] star_score_required_to_unlock_this_world;//[world]
	//for map
	public int star_score_difference;

	//allow manual personalization
	public enum press_start_and_go_to
		{
		nested_world_stage_select_screen,
		map,
		single_screen_with_a_page_for_every_world,
		straight_to_the_next_game_stage
		}
	public press_start_and_go_to press_start_and_go_to_selected = press_start_and_go_to.nested_world_stage_select_screen;
	
	public enum world_screen_generation
		{
		automatic,
		manual
		}
	public world_screen_generation world_screen_generation_selected = world_screen_generation.automatic;
	public bool show_world_name_on_world_ico;
	public bool show_world_number_on_world_ico;

	public enum stage_screen_generation
	{
		automatic,
		manual
	}
	public stage_screen_generation stage_screen_generation_selected = stage_screen_generation.automatic;
	
	public bool use_world_screen_to_show_stages_too;

	//manage music
	public enum when_win_play
	{
		no,
		music,
		sfx
	}
	public when_win_play when_win_play_selected = when_win_play.music;
	public bool play_win_music_in_loop;

	public enum when_lose_play
	{
		no,
		music,
		sfx
	}
	public when_lose_play when_lose_play_selected = when_lose_play.music;
	public bool play_lose_music_in_loop;

	public AudioSource music_source;
		public float fade_music = 0.5f;//music fade time
	public AudioClip music_menu;
	public AudioClip music_stage_win;
	public AudioClip music_stage_lose;
	public AudioClip[] show_big_star_sfx;
	AudioClip music_playing_now;
	
	//manage sfx
	public AudioSource sfx_source;
	public AudioClip tap_sfx;
	public AudioClip tap_error_sfx;

	//manage savestates
	public bool show_new_profile_window;
	public int number_of_save_profile_slot_avaibles;
	public int current_profile_selected;
	public static bool exist_a_save_state;
		public bool[] this_profile_have_a_save_state_in_it;//[profile]
		public bool require_a_name_for_profiles;

	//int score
	public int best_int_score_on_this_device;//the best score among all profiles
	
	public static GameObject game_master_obj;

	public static bool game_is_started = false;
	public static bool logo_already_show = false;
	public enum this_screen
	{
		home_screen,
		stage_screen
	}
	public this_screen go_to_this_screen = this_screen.home_screen;
	public bool a_window_is_open;
	
	//avoid multiple istances of this prefab
	bool keep_me;
	

	// Use this for initialization
	void Awake () {

		if ( !game_is_started )
			{
			keep_me = true;
            home_scene_name = SceneManager.GetActiveScene().name;
            my_store_item_manager = this.gameObject.GetComponent<StoreManager>();

			my_ads_master = GetComponent<ads_master>();
			if (my_ads_master)
				{
				my_ads_master.my_game_master = this;
				my_ads_master.Initiate_ads();
				}
			}
		
		if (keep_me)
			{
			game_master_obj = this.gameObject;

            DontDestroyOnLoad(game_master_obj);//this prefab will be used as reference from the others, so don't destry it when load a new scene
			
			//sum the stages in every world to know the total number of stages in the game
				for(int w = 0; w < total_stages_in_world_n.Length; w++)
					{
					total_number_of_stages_in_the_game += total_stages_in_world_n[w];
					if (total_stages_in_world_n[w] > max_stages_in_a_world)
						max_stages_in_a_world = total_stages_in_world_n[w];
					}
				if (show_debug_messages)
					Debug.Log("total_number_of_stages_in_the_game = " + total_number_of_stages_in_the_game + " max_stages_in_a_world = " + max_stages_in_a_world );

			//create multy arrays for multy profile saves
				this_profile_have_a_save_state_in_it = new bool[number_of_save_profile_slot_avaibles];
                playerProfiles = new PlayerProfile[number_of_save_profile_slot_avaibles];
                for (int i = 0; i < playerProfiles.Length; i++)
                    {
                    playerProfiles[i] = new PlayerProfile();
                    playerProfiles[i].InitiateMe(i,this);
                    }

                for (int i = 0; i < number_of_save_profile_slot_avaibles; i++)
					{
					this_profile_have_a_save_state_in_it[i] = Convert.ToBoolean(PlayerPrefs.GetInt("profile_"+i.ToString()+"_have_a_save_state_in_it")) ;
                    }
					

			exist_a_save_state = Convert.ToBoolean(PlayerPrefs.GetInt("savestate")) ;
			current_profile_selected = PlayerPrefs.GetInt("last_profile_used");



			if (exist_a_save_state)//copy the saves in the arrays
				{
				if (PlayerPrefs.GetInt("total_number_of_stages_in_the_game") == total_number_of_stages_in_the_game) //if the _total_stages number not is the same of last time, erase save data to avoid broken array (the _total_stages don't cange in the game, so the player don't chance to lose his saves. This is useful when you decide to change the total stage number through the making of the game and avoid errors because previous save data refer to a old version)
					{
					if (show_debug_messages)
						{
						Debug.Log("same total_stages from the last time");
						Debug.Log("current_profile_selected "+ current_profile_selected);
						}
					//check if the last profile used have can be load
					if (this_profile_have_a_save_state_in_it[current_profile_selected])
						Load(current_profile_selected);
					else //I can't find the last profile used
						{
						//so ask to select/create a profile
						show_new_profile_window = true;
						}
					}
				else //you have changed the total_stages from the last time, sto the old save data have a different array_length. 
					{
					if (show_debug_messages)
						Debug.Log("different total_stages from the last time");
					if (this_profile_have_a_save_state_in_it[current_profile_selected])
						{
						if (total_number_of_stages_in_the_game > PlayerPrefs.GetInt("total_number_of_stages_in_the_game")) //you have add stages from last time
							{
							if (show_debug_messages)
								Debug.Log("there are more stages that in the previous save data");
							PlayerPrefs.SetInt("total_number_of_stages_in_the_game", total_number_of_stages_in_the_game);
							for (int i = 0; i < number_of_save_profile_slot_avaibles; i++)
								{
								playerProfiles[i].all_stages_solved = false;
								PlayerPrefs.SetInt("all_stages_solved",Convert.ToInt32(playerProfiles[i].all_stages_solved));
								}
							Load(current_profile_selected);
							}
						else //you have remove stages from last time
							{
							if (show_debug_messages)
								Debug.Log("there are less stages that in the previous save data");
							//delete the old data
							Erase_saves();
							for (int i = 0; i < number_of_save_profile_slot_avaibles; i++)
								{
                                playerProfiles[i].world_playable[0] = true;
                                playerProfiles[i].stage_playable[0,0] = true;
								Save(i);
								}

							}
						}
					else //I can't find the last profile used
						{
						//so ask to select/create a profile
						show_new_profile_window = true;
						}
					}
					
				}
			else //no save data, so start the game from zero
				{
				current_profile_selected = 0;
				if (number_of_save_profile_slot_avaibles == 1) //if no multiple profiles are allowed, start new game
					{
					if (show_debug_messages)
						Debug.Log("no save data and only one profile slot allowed");
					Create_new_profile("Player");
					}
				//else request to activate a new empty profile
				if (show_debug_messages)
					Debug.Log("no save data and multi profile slot allowed");
				show_new_profile_window = true;
				}

			}
		else
			{
			//this avoid duplication of this istance
			Destroy(this.gameObject);
			}
		
	}


    #region manage savestates
    public void Create_new_profile(string my_name)
    {
        show_new_profile_window = false;
        if (show_debug_messages)
            Debug.Log("Create_new_profile " + current_profile_selected + " = " + my_name);


        GetCurrentProfile().Create_new_profile(my_name, this);

    }
    

    public void Load(int profile_slot)
    {
        best_int_score_on_this_device = PlayerPrefs.GetInt("best_int_score_on_this_device");

        playerProfiles[profile_slot].Load();

        if (show_debug_messages)
            Debug.Log("Load savestate profile: " + profile_slot + " " + playerProfiles[profile_slot].profile_name);
    }
    
    public void Save(int profile_slot)
    {
        exist_a_save_state = true;
        PlayerPrefs.SetInt("savestate", Convert.ToInt32(exist_a_save_state));

        this_profile_have_a_save_state_in_it[profile_slot] = true;
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_have_a_save_state_in_it", Convert.ToInt32(this_profile_have_a_save_state_in_it[profile_slot]));

        PlayerPrefs.SetInt("last_profile_used", current_profile_selected);
        PlayerPrefs.SetInt("best_int_score_on_this_device", best_int_score_on_this_device);
        PlayerPrefs.SetInt("total_number_of_stages_in_the_game", total_number_of_stages_in_the_game);


        playerProfiles[profile_slot].SaveAll();


        if (show_debug_messages)
            Debug.Log("Save savestate profile: " + profile_slot);
    }
    
    public void Erase_saves()
		{
		if (show_debug_messages)
			Debug.Log("erase data");
		PlayerPrefs.DeleteAll();
		exist_a_save_state = false;
		}



	public void Reset_current_world(int world_n, bool can_be_played_from_first_stage)
	{
        GetCurrentProfile().world_playable[world_n] = false;

		if (if_not_continue_lose_gained_stars)
			{
            GetCurrentProfile().star_score_in_this_world[world_n] = 0;
			}

		for (int i = 0; i < total_stages_in_world_n[world_n]; i++)
			{
            GetCurrentProfile().stage_playable[world_n,i] = false;
			if (if_not_continue_lose_gained_stars)
				{
                GetCurrentProfile().stars_total_score -= GetCurrentProfile().stage_stars_score[world_n,i];
                GetCurrentProfile().stage_stars_score[world_n,i]=0;

                if (star_score_rule == Star_score_rule.EachStageHaveHisOwnStarCap)
                    GetCurrentProfile().stage_stars_cap_score[world_n, i] = 0;

                }
			}

		if (can_be_played_from_first_stage)
            GetCurrentProfile().UnlockWorld(world_n);
		else
			{
			if (this_world_is_unlocked_after_selected[world_n] == this_world_is_unlocked_after.start)
				{
                GetCurrentProfile().UnlockWorld(world_n);
				}
			else if (this_world_is_unlocked_after_selected[world_n] == this_world_is_unlocked_after.bui_it)
				{
				if (GetCurrentProfile().WorldPurchased(world_n))
					{
                    GetCurrentProfile().UnlockWorld(world_n);
					}
				}
			else if (this_world_is_unlocked_after_selected[world_n] == this_world_is_unlocked_after.reach_this_star_score)
				{
				if (GetCurrentProfile().stars_total_score >=  star_score_required_to_unlock_this_world[world_n])
					{
                    GetCurrentProfile().UnlockWorld(world_n);
					}
				}
			}
	}

    public int GetMaxWorldReached()
    {
        return GetCurrentProfile().play_this_stage_to_progress_in_the_game_world;
    }

    public int GetMaxStageReached()
    {
        return GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage;
    }

    public void Reset_all_worlds()
	{
		for(int world = 0; world < total_stages_in_world_n.Length; world++)
			Reset_current_world(world,false);

        GetCurrentProfile().play_this_stage_to_progress_in_the_game_world = 0;
        GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage = 0;
	}

    public void Delete_this_profile(int profile_slot)
    {
        playerProfiles[profile_slot].profile_name = "";

        playerProfiles[profile_slot].Delete_this_profile();

        this_profile_have_a_save_state_in_it[profile_slot] = false;
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_have_a_save_state_in_it", Convert.ToInt32(this_profile_have_a_save_state_in_it[profile_slot]));
    }
    
    public void All_stages_solved()//what happen when the player finish the game
		{
		GetCurrentProfile().all_stages_solved = true;
		if (show_debug_messages)
			Debug.Log("All stages solved");
        SceneManager.LoadScene("End_screen");
        }
	


	#endregion


	#region countdown
    public bool CheckIfStatExtraliveCountdown()
    {
        if (GetCurrentProfile().recharge_live_countdown_active)
            return false;

        if (GetCurrentProfile().extra_live_countdown_active && timerStatus != TimerStatus.countdown)
            {
            Set_date_countdown(true);
            return true;
            }

        if (gain_an_extra_live_each_x_minutes > 0 && GetCurrentProfile().GetCurrentLivesInt() > 0 && GetCurrentProfile().GetCurrentLivesInt() < live_cap_for_lives_gained_with_timer)
            return true;

        return false;
    }


    public void Set_date_countdown(bool isAnExtraLive)
	{
        if (!isAnExtraLive)
            GetCurrentProfile().recharge_live_countdown_active = true;//you are at zero lives and must wait to play
        else
            GetCurrentProfile().extra_live_countdown_active = true;
        
        
        GetCurrentProfile().target_time = DateTime.Now;

        if (show_debug_messages)
			Debug.Log("now: " + GetCurrentProfile().target_time);

        if (isAnExtraLive)//you have lives to play and in the meantime you get more lives
            GetCurrentProfile().target_time = GetCurrentProfile().target_time.AddMinutes((double)gain_an_extra_live_each_x_minutes);
        else
            {
            GetCurrentProfile().target_time = GetCurrentProfile().target_time.AddDays((double)wait_for_days);
		    GetCurrentProfile().target_time = GetCurrentProfile().target_time.AddHours((double)wait_for_hours);
		    GetCurrentProfile().target_time = GetCurrentProfile().target_time.AddMinutes((double)wait_for_minutes);
		    GetCurrentProfile().target_time = GetCurrentProfile().target_time.AddSeconds((double)wait_for_seconds);
            }

        if (show_debug_messages)
			Debug.Log("target time: " + GetCurrentProfile().target_time);

        Check_countdown();

        timerStatus = TimerStatus.countdown;
    }

    public void Check_countdown()
	{
		TimeSpan span = GetCurrentProfile().target_time.Subtract(DateTime.Now);
		int days = span.Days;
		int hours = span.Hours;
		int minutes = span.Minutes;
		int seconds = span.Seconds;

		if (show_debug_messages)
			Debug.Log("Check_countdown: " + days + "," + hours + "," + minutes + "," + seconds);

		int total_seconds_to_wait = 0;
		if (days > 0)
			total_seconds_to_wait += days*86400;
		if (hours > 0)
			total_seconds_to_wait += hours*3600;
		if (minutes > 0)
			total_seconds_to_wait += minutes*60;
		if (seconds > 0)
			total_seconds_to_wait += seconds;

		if (show_debug_messages)
			Debug.Log("Total seconds to wait = " + total_seconds_to_wait);

		//Invoke("Countdown_end",total_seconds_to_wait);
	}

    private void FixedUpdate()
    {
        if (timerStatus != TimerStatus.countdown)
            return;

        int targetDiffetence = DateTime.Compare(DateTime.Now, GetCurrentProfile().target_time);

        if (targetDiffetence >= 0)
            Countdown_end();
    }

    public string Show_how_much_time_left()
	{
		string my_text = "";

		TimeSpan span = GetCurrentProfile().target_time.Subtract(DateTime.Now);
		int days = span.Days;
		int hours = span.Hours;
		int minutes = span.Minutes;
		int seconds = span.Seconds;

		if (days > 0)
			{
			if (days == 1)
				my_text = "1 day and: ";
			else
				my_text = days.ToString()+ " days and: ";	
			}

		if (hours > 0)
			{
			if (hours < 10)
				my_text += "0";

			my_text += hours.ToString()+ ":";	
			}
		else
			my_text += "00:";

		if (minutes > 0)
			{
			if (minutes < 10)
				my_text += "0";

			my_text += minutes.ToString()+ ":";	
			}
		else
			my_text += "00:";

		if (seconds > 0)
			{
			if (seconds < 10)
				my_text += "0";

			my_text += seconds.ToString();	
			}
		else
			my_text += "00";

		return my_text;
	}

	public void Check_if_interrupt_countdown()
	{
        if (GetCurrentProfile().recharge_live_countdown_active)
            InterrupCurrentCountDown();

    }

    public void InterrupCurrentCountDown()
    {
        if (show_debug_messages)
            Debug.Log("interrupt_countdown");

        //CancelInvoke("Countdown_end");

        timerStatus = TimerStatus.Off;
        GetCurrentProfile().target_time = DateTime.MinValue;
    }

    void Countdown_end()
	{
        if (show_debug_warnings)
            Debug.LogWarning("Countdown_end");

        if (GetCurrentProfile().recharge_live_countdown_active)//you get enough lives to play again
            {
            GetCurrentProfile().recharge_live_countdown_active = false;
            GetCurrentProfile().UpdateLives(if_not_continue_restart_with_lives);
            }
        else //you get extra lives
            {
            GetCurrentProfile().UpdateLives(1);
            GetCurrentProfile().extra_live_countdown_active = false;
            if (GetCurrentProfile().GetCurrentLivesInt() > 0 && GetCurrentProfile().GetCurrentLivesInt() < live_cap_for_lives_gained_with_timer)
                Set_date_countdown(true);

            }

        Save(current_profile_selected);

        timerStatus = TimerStatus.done;
    }
	#endregion
	
	#region manage cameras
	/*
	public static void Link_me_to_camera(Camera camera_target)
		{
		if (camera_target)
			{
			//this put the AudioSource on the active camera
			game_master_obj.transform.parent = camera_target.transform;
				game_master_obj.transform.localPosition = Vector3.zero;
				game_master_obj.transform.localRotation = Quaternion.identity;
			}
		else
			{
				Debug.LogError("camera_target not exist!");
			}
		}
	
	public void Unlink_me_to_camera()
		{
		game_master_obj.transform.parent = null;
		}
		*/
	#endregion
	
	#region manage music
	
	public void Music_on_off(bool enabled)
		{
        GetCurrentProfile().music_on = enabled;
		if (GetCurrentProfile().music_on)
			{
			music_source.volume = GetCurrentProfile().music_volume;
			}
		else
			music_source.volume = 0;

		PlayerPrefs.SetInt("profile_"+current_profile_selected.ToString()+"_music_on_off",Convert.ToInt32(GetCurrentProfile().music_on));
		PlayerPrefs.SetFloat("profile_"+current_profile_selected.ToString()+"_music_volume", GetCurrentProfile().music_volume);
		}

	public void Sfx_on_off(bool enabled)
		{
        GetCurrentProfile().sfx_on = enabled;
		if (GetCurrentProfile().sfx_on)
			sfx_source.volume = GetCurrentProfile().sfx_volume;
		else
			sfx_source.volume = 0;

		PlayerPrefs.SetInt("profile_"+current_profile_selected.ToString()+"_sfx_on_off",Convert.ToInt32(GetCurrentProfile().sfx_on));
		PlayerPrefs.SetFloat("profile_"+current_profile_selected.ToString()+"_sfx_volume", GetCurrentProfile().sfx_volume);
		}

	public void Voice_on_off(bool enabled)
	{
        GetCurrentProfile().voice_on = enabled;
		//no source because it must be within the game elements and not here
		/*
		if (voice_on[current_profile_selected])
			voice_source.volume = voice_volume[current_profile_selected];
		else
			voice_source.volume = 0;
		 */

		PlayerPrefs.SetInt("profile_"+current_profile_selected.ToString()+"_voice_on_off",Convert.ToInt32(GetCurrentProfile().voice_on));
		PlayerPrefs.SetFloat("profile_"+current_profile_selected.ToString()+"_voice_volume", GetCurrentProfile().voice_volume);
	}


		
	
	public void Start_music(AudioClip my_music,bool loop)
		{
		if (show_debug_messages)
			Debug.Log("call start music");
		//if you not are playing anything, start play
		if (music_playing_now == null)
			{
			music_playing_now = my_music;
			Music(loop);
			}
		else //if you aready play a music, change it if different
			{
			if (music_source.clip != my_music)
				{
				StartCoroutine(Change_music(my_music,loop));
				}
			}
		}
	
	void Music(bool loop)
		{
		if (show_debug_messages)
			Debug.Log("call music");
		if (music_source)//if there is an AudioSource
			{
			music_source.clip = music_playing_now;//what music play

				if (!music_source.isPlaying)//if you don't play the music yet, play it!
					{
					if (GetCurrentProfile().music_on)//if music is on
						music_source.volume = GetCurrentProfile().music_volume;
					else
						music_source.volume = 0;
					
					music_source.Play();
					music_source.loop = loop;
					}
			}
			
		}


	IEnumerator Change_music(AudioClip new_music, bool loop)
	{

			float fade_duration = fade_music*0.5f;

			if (GetCurrentProfile().music_on)
			{
				if (show_debug_messages)
					Debug.Log("fade down");
				float passed_fade_down_time = 0;

				while (passed_fade_down_time < fade_duration)
					{
					music_source.volume = Mathf.Lerp(GetCurrentProfile().music_volume,0,(passed_fade_down_time / fade_duration));
					passed_fade_down_time += Time.deltaTime;
					yield return new WaitForEndOfFrame();
					}
			}
		
			music_playing_now = new_music;//change music
			Music(loop);

			if (GetCurrentProfile().music_on)
			{
				if (show_debug_messages)
					Debug.Log("fade up");
				float passed_fade_up_time = 0;
				while (passed_fade_up_time < fade_duration)
					{
					music_source.volume = Mathf.Lerp(0, GetCurrentProfile().music_volume,(passed_fade_up_time / fade_duration));
					passed_fade_up_time += Time.deltaTime;
					yield return new WaitForEndOfFrame();
					}
			}
		
	}

	public void Gui_sfx(AudioClip gui_sound)
	{
		if (GetCurrentProfile().sfx_on && gui_sound)
		{
			if(!sfx_source.isPlaying)
			{
				sfx_source.PlayOneShot(gui_sound);
				sfx_source.loop = false;
			}
			else
			{
				sfx_source.Stop();
				sfx_source.PlayOneShot(gui_sound);
				sfx_source.loop = false;
			}
		}
	}
	#endregion


}
