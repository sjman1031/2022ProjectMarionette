using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class game_uGUI : MonoBehaviour {

	[SerializeField]private EventSystem my_eventSystem = null;

	public int n_world;//the current world. It is need to save and load in the corret slot
	public int n_stage;//the number of this stage. It is need to save and load in the corret slot
	public AudioClip stage_music;

	public bool ignore_game_master_preferences;

	//ads
	public gift_manager my_gift_manager;
	public feedback_window my_feedback_window;
	[HideInInspector]public GameObject double_score;
	bool score_doubled;

	[HideInInspector]public Transform play_screen;
	[HideInInspector]public Transform pause_screen;
	[HideInInspector]public Transform loading_screen;
	/*[HideInInspector]*/public Transform options_screen;
		options_menu my_options;
	[HideInInspector]public Transform win_screen;
	[HideInInspector]public Transform lose_screen;
	[HideInInspector]public GameObject retry_button;
	[HideInInspector]public GameObject stage_button;
	[HideInInspector]public continue_window my_continue_window;

	//what button select with the pad when open this screen
	[HideInInspector]public GameObject options_screen_target_button;
	[HideInInspector]public GameObject pause_screen_target_button;
	[HideInInspector]public GameObject win_screen_target_button;
	[HideInInspector]public GameObject lose_screen_target_button;
	[HideInInspector]public GameObject continue_window_target_button;

	[HideInInspector]public GameObject lives_ico;
	[HideInInspector]public TextMeshProUGUI lives_count;
    [HideInInspector] public GameObject lives_timer;
    TextMeshProUGUI lives_timer_text;

    public bool show_virtual_money;
	public bool keep_money_taken_in_this_stage_only_if_you_win;
	int temp_money_count;
    [HideInInspector] public bool doubleVirtualMoney;
    [HideInInspector]public GameObject virtual_money_ico;
	[HideInInspector]public TextMeshProUGUI virtual_money_count;

	[HideInInspector]public GameObject int_score_ico;
	[HideInInspector]public TextMeshProUGUI int_score_count;
	[HideInInspector]public GameObject int_score_record_ico;
	[HideInInspector]public TextMeshProUGUI int_score_record;
	[HideInInspector]public TextMeshProUGUI win_screen_int_score_title;
	[HideInInspector]public GameObject int_score_record_anim;
	string temp_score_name;
	[HideInInspector]public TextMeshProUGUI win_screen_int_score_count;
	[HideInInspector]public TextMeshProUGUI win_screen_int_score_record;
	bool new_record;

	public bool show_star_score;
		public bool show_star_count;
	public bool show_progress_bar;
		public progress_bar my_progress_bar;
	public bool progress_bar_use_score;

	public bool show_int_score;
	public bool show_stage_record;
	[HideInInspector]public GameObject stars_ico;
	[HideInInspector]public TextMeshProUGUI stars_count;
	
	[HideInInspector]public GameObject lose_screen_lives_ico;
	[HideInInspector]public TextMeshProUGUI lose_screen_lives_count;

	[HideInInspector]public GameObject next_stage_ico;

    //win screen
    public WinScreenSkin winScreenSkin;
    [HideInInspector]public int star_number;
    [HideInInspector]public int total_collectable_stars_in_this_stage;
    [HideInInspector]public int int_score;

	public static bool allow_game_input;//this is false when a menu is open
	public static bool in_pause;
	public static bool stage_end;

	game_master my_game_master;

	public bool restart_without_reload_the_stage;
	[HideInInspector]public bool restarting;

	public bool show_debug_messages;
	public bool show_debug_warnings;

    private void Awake()
    {
        total_collectable_stars_in_this_stage = 0;
    }

    // Use this for initialization
    void Start () {

		my_options = options_screen.GetComponent<options_menu>();
        lives_timer_text = lives_timer.transform.GetChild(0).GetComponent<TextMeshProUGUI>();


        if (game_master.game_master_obj)
			{
			my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
			my_game_master.my_ads_master.my_game_uGUI = this;
			}

		if (my_game_master)
			{
            //set ads gui
            my_game_master.my_ads_master.Initiate_ads();
			my_game_master.my_ads_master.my_feedback_window = my_feedback_window;
			my_game_master.my_ads_master.my_gift_manager = my_gift_manager;
			my_gift_manager.my_game_master = my_game_master;

			//star score
			if (!ignore_game_master_preferences)
				{
				show_star_score = my_game_master.show_star_score;
				show_progress_bar = my_game_master.show_progres_bar;
				//int score
				show_int_score = my_game_master.show_int_score;
				}
			show_stage_record = my_game_master.show_int_score_stage_record_in_game_stage;


			if (my_game_master.score_name != "")
				int_score_ico.GetComponent<TextMeshProUGUI>().text = my_game_master.score_name;

			if (!ignore_game_master_preferences)
				{
				show_debug_messages = my_game_master.show_debug_messages;
				show_debug_warnings = my_game_master.show_debug_warnings;
				}
				
			if (my_game_master.use_same_scene_for_all_stages_in_the_same_world)
                n_stage = my_game_master.current_stage;
			
			my_game_master.GetCurrentProfile().latest_world_played = n_world;
            my_game_master.GetCurrentProfile().latest_stage_played = n_stage;


            my_game_master.GetCurrentProfile().current_world = n_world-1;
			}
		else
			{
			temp_score_name = win_screen_int_score_title.text;
			if (show_debug_warnings)
				Debug.LogWarning("In order to allow saves and play music and menu sfx, you must star from Home scene and open this stage using the in game menu");
			}

        //star score
        if (total_collectable_stars_in_this_stage == 0)
            total_collectable_stars_in_this_stage = 3; //defalut value

        if (my_game_master && my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
        {
            my_game_master.GetCurrentProfile().stage_stars_cap_score[n_world - 1, n_stage - 1] = total_collectable_stars_in_this_stage;

        }

        if (show_star_score)
            winScreenSkin.star_container.SetActive(true);
		else
		{
			show_star_count = false;
            winScreenSkin.star_container.SetActive(false);
		}
		if (show_progress_bar)
			{
			my_progress_bar.Start_me(this);
			my_progress_bar.gameObject.SetActive (true);
			}
		else
			my_progress_bar.gameObject.SetActive (false);

		//int score
		if (show_int_score)
		{
			int_score_ico.SetActive(true);
		}
		else
			int_score_ico.SetActive(false);

		Reset_me();
	}

	public void Reset_me()
	{
		if (show_debug_messages)
			Debug.Log("reset stage game gui");

		Time.timeScale = 1;

		if (my_game_master)
		{
			//music
			my_game_master.Start_music(stage_music,true);

			//lives
			if (my_game_master.infinite_lives)
				lives_ico.SetActive(false);
			else
				Update_lives(0);

			my_game_master.star_score_difference = 0;

			if (!keep_money_taken_in_this_stage_only_if_you_win)
				virtual_money_count.text = my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyString();


		}


		//reset int score
		double_score.SetActive(false);
		score_doubled = false;
		int_score = 0;
		int_score_count.text = (0).ToString("N0");
		win_screen_int_score_title.gameObject.SetActive(false);
		int_score_record_anim.SetActive (false);
		if (my_game_master)
			win_screen_int_score_title.text = my_game_master.score_name;
		else
			win_screen_int_score_title.text = temp_score_name;
		win_screen_int_score_count.text = (0).ToString("N0");
		new_record = false;
		win_screen_int_score_record.gameObject.SetActive(new_record);
		win_screen_int_score_record.text = "";
		if (show_stage_record && my_game_master && !ignore_game_master_preferences)
			{
			int_score_record.text = (my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world - 1, n_stage - 1]).ToString ("N0");
			int_score_record_ico.SetActive (true);
			}
		else
			int_score_record_ico.SetActive (false);

		//virtual money
		temp_money_count = 0;
		if (keep_money_taken_in_this_stage_only_if_you_win || !my_game_master)
			virtual_money_count.text = temp_money_count.ToString();

		//reset star score
		star_number = 0;
		if (show_star_count)
			{
			stars_count.text = (0).ToString() + "/" + total_collectable_stars_in_this_stage.ToString();
			stars_ico.gameObject.SetActive(true);
			}
		else
			stars_ico.gameObject.SetActive(false);

        //reset win screen
        winScreenSkin.ResetWinScreen(my_game_master, total_collectable_stars_in_this_stage, show_int_score);

		//reset lose screen
		lose_screen.gameObject.SetActive(false);


		loading_screen.gameObject.SetActive(false);
		pause_screen.gameObject.SetActive(false);

		//start
		allow_game_input = true;
		in_pause = false;
		stage_end = false;
		play_screen.gameObject.SetActive(true);

		if(my_game_master)
			my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.whenStageStart);

	}
	

	
	void Update()
	{


		if (my_game_master)
			{
			if ( Input.GetKeyDown(my_game_master.pad_pause_button) && !my_continue_window.gameObject.activeSelf
			    && (play_screen.gameObject.activeSelf || pause_screen.gameObject.activeSelf) )
			    Pause();

			Manage_ESC();
			Manage_pad_back();
            Manage_LivesTimer();
            }

	}

    void Manage_LivesTimer()
    {
        if (!lives_timer.activeSelf)
            return;

        if (my_game_master.GetCurrentProfile().target_time.Year == 0001)
        {
            my_game_master.Set_date_countdown(true);
            my_game_master.Save(my_game_master.current_profile_selected);
        }

        lives_timer_text.text = my_game_master.Show_how_much_time_left();

        if (my_game_master.timerStatus == game_master.TimerStatus.done)
        {
            my_game_master.timerStatus = game_master.TimerStatus.Off;
            Update_lives(0);
        }

    }

    void Manage_pad_back()
	{
		if ((Input.GetKeyDown(my_game_master.pad_back_button) && my_game_master.use_pad))
			{
			if (!play_screen.gameObject.activeSelf)
				{
				if (play_screen.gameObject.activeSelf || pause_screen.gameObject.activeSelf)
					{
					Pause();
					}
				else 
					{
					if (options_screen.gameObject.activeSelf)
						Close_options_menu(true);
					else
						Go_to_stage_screen();
					}
				}
			}
	}

	void Manage_ESC()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && my_game_master.allow_ESC)
			{
			if (!my_continue_window.gameObject.activeSelf)
				{
				if ((play_screen.gameObject.activeSelf || pause_screen.gameObject.activeSelf))
					Pause();
				else if (options_screen.gameObject.activeSelf)
					Close_options_menu(true);
				else
					Go_to_stage_screen();
				}
			}
	}

	public void Open_options_menu(bool from_pause_screen)
	{
		if (my_game_master)
		{
			if (from_pause_screen)
				pause_screen.gameObject.SetActive(false);
			else
				{
				in_pause = true;
				allow_game_input = false;
				play_screen.gameObject.SetActive(false);
				Time.timeScale = 0;
				}

			options_screen.gameObject.SetActive(true);
			my_options.Start_me();
			Mark_this_button(options_screen_target_button);
		}
		else
			{
			if (show_debug_warnings)
				Debug.LogWarning("In order to allow saves and play music and menu sfx, you must star from Home scene and open this stage using the in game menu");
			}
	}

	public void Close_options_menu(bool back_to_pause_screen)
	{
		options_screen.gameObject.SetActive(false);
		if (back_to_pause_screen)
			{
			pause_screen.gameObject.SetActive(true);
			Mark_this_button(pause_screen_target_button);
			}
		else
			{
			in_pause = false;
			allow_game_input = true;
			play_screen.gameObject.SetActive(true);
			Time.timeScale = 1;
			}
	}

	public void Pause()
	{
		if (my_game_master)
			my_game_master.Gui_sfx(my_game_master.tap_sfx);

		if (in_pause)
			{
			in_pause = false;
			allow_game_input = true;
			play_screen.gameObject.SetActive(true);
			Time.timeScale = 1;
			pause_screen.gameObject.SetActive(false);
			Mark_this_button(null);
			}
		else
			{
			in_pause = true;
			allow_game_input = false;
			play_screen.gameObject.SetActive(false);
			pause_screen.gameObject.SetActive(true);
			Time.timeScale = 0;

			Mark_this_button(pause_screen_target_button);
			}

	}

	public void Mark_this_button(GameObject target_button)
	{
		if (show_debug_messages)
			{
			if (target_button)
				Debug.Log("Mark_this_button: " + target_button.name);
			else
				if (show_debug_messages)
				Debug.Log("NULL");
			}

		if(my_game_master && my_game_master.use_pad)
		{
			my_eventSystem.SetSelectedGameObject(target_button);
		}
	}

	public void Mark_continue()
	{
		if (show_debug_messages)
			Debug.Log("Mark_continue()");
		Mark_this_button(continue_window_target_button);
	}



	public void Retry()
	{
		if (my_game_master)
			{
			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			//my_game_master.Unlink_me_to_camera();
			if (my_game_master.show_loading_screen)
				loading_screen.gameObject.SetActive(true);
			}
		//reload this stage
		if (restart_without_reload_the_stage)
			{
			restarting = true;
			Reset_me();
			}
		else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void Next()
	{
		if (my_game_master)
			{
			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			//my_game_master.Unlink_me_to_camera();

			if(n_stage < my_game_master.total_stages_in_world_n[n_world-1])//there are more stages in this world to play
				{
                int next_world = n_world;

                if (my_game_master.use_same_scene_for_all_stages_in_the_same_world)
                {
                    my_game_master.current_stage++;

                    if (my_game_master.show_loading_screen)
                        loading_screen.gameObject.SetActive(true);

                    SceneManager.LoadScene("W" + next_world.ToString() + "_Stage_" + 1);
                }
                else
                {
                    int next_stage = n_stage + 1;
                    if (show_debug_messages)
                        Debug.Log("there are more stage in this world, so go to " + "W" + next_world.ToString() + "_Stage_" + next_stage.ToString());
                    if (my_game_master.show_loading_screen)
                        loading_screen.gameObject.SetActive(true);
                    SceneManager.LoadScene("W" + next_world.ToString() + "_Stage_" + next_stage.ToString());
                }
            }
			else //go to next word if exist
				{
				if (n_world < my_game_master.total_stages_in_world_n.Length)
					{
					if (my_game_master.GetCurrentProfile().world_playable[n_world] && my_game_master.GetCurrentProfile().stage_playable[n_world,0])
						{
						int next_world = n_world+1;
						if (show_debug_messages)
							Debug.Log("go to next world " + ("W"+next_world.ToString()+"_Stage_1"));
						if (my_game_master.show_loading_screen)
							loading_screen.gameObject.SetActive(true);
                        SceneManager.LoadScene("W" + next_world.ToString() + "_Stage_1");
                        }
					else 
						Go_to_stage_screen();
					}
				else //this was the last stage, so...
					my_game_master.All_stages_solved();
				}
			}
		else
			{
			if (show_debug_warnings)
				Debug.LogWarning("You must start the game from Home scene in order to use this button");
			}
	}

	public void Go_to_stage_screen()
	{
		if (my_game_master)
			{
			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			//my_game_master.Unlink_me_to_camera();
			my_game_master.go_to_this_screen = game_master.this_screen.stage_screen;
			Time.timeScale = 1;
			if (my_game_master.show_loading_screen)
				loading_screen.gameObject.SetActive(true);
            SceneManager.LoadScene(my_game_master.home_scene_name);
        }
		else
			{
			if (show_debug_warnings)
				Debug.LogWarning("You must start the game from Home scene in order to use this button");
			}
	}

	public void Go_to_Home_screen()
	{
		if (my_game_master)
		{
			my_game_master.refresh_stage_and_world_screens = true;
			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			//my_game_master.Unlink_me_to_camera();
			my_game_master.go_to_this_screen = game_master.this_screen.home_screen;
			Time.timeScale = 1;
			if (my_game_master.show_loading_screen)
				loading_screen.gameObject.SetActive(true);
            SceneManager.LoadScene(my_game_master.home_scene_name);
        }
		else
			{
			if (show_debug_warnings)
				Debug.LogWarning("You must start the game from Home scene in order to use this button");
			}
	}

	public void Update_virtual_money(int money)
	{

		if (keep_money_taken_in_this_stage_only_if_you_win)
			{
			temp_money_count += money;
			virtual_money_count.text = temp_money_count.ToString();
			}
		else
			{
			if (my_game_master)
				{
                my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(money);
                virtual_money_count.text = my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyString();
				}
			else
				{
				if (show_debug_warnings)
					Debug.LogWarning("You must start the game from Home scene in order to save virtual money in a game profile");
				}
			}
	}

	public void Update_lives(int live_variation)
	{
		bool dead = false;
		if (my_game_master)
			{
			if (!my_game_master.infinite_lives)
				{
                my_game_master.GetCurrentProfile().UpdateLives(live_variation);
				

                if (my_game_master.GetCurrentProfile().GetCurrentLivesInt() <= 0)
                    dead = true;
                else
                    dead = false;


                lives_timer.gameObject.SetActive(my_game_master.CheckIfStatExtraliveCountdown());

                my_game_master.Save(my_game_master.current_profile_selected);

				if (dead)
					{
                    my_game_master.InterrupCurrentCountDown();

                    if (my_game_master.lose_lives_selected == game_master.lose_lives.in_game)
						{
						if (my_game_master.continue_rule_selected != game_master.continue_rule.never_continue)
							{//ask if you want continue
							if (my_game_master.my_ads_master.whenContinueScreenAppear.thisAdIsEnabled)
								my_continue_window.Start_me_with_ad(my_game_master.my_ads_master.whenContinueScreenAppear);
							else
								my_continue_window.Start_me();
							}
						else
							{//ultimate lose
							Defeat();
							}
						}
					else if (my_game_master.lose_lives_selected == game_master.lose_lives.when_show_lose_screen)
						{
						if (my_game_master.continue_rule_selected != game_master.continue_rule.never_continue)
							{//ask if you want continue
							if (my_game_master.my_ads_master.whenContinueScreenAppear.thisAdIsEnabled)
								my_continue_window.Start_me_with_ad(my_game_master.my_ads_master.whenContinueScreenAppear);
							else
								my_continue_window.Start_me();
							}
						}
					}
				else 
					{
					//player continue to play from check point in this state or after new live animation, etcetera...
					//these behavior are managed from yours scripts and not from this generic gui system
					}
				
				lives_count.text = my_game_master.GetCurrentProfile().GetCurrentLivesString();
				lose_screen_lives_count.text = my_game_master.GetCurrentProfile().GetCurrentLivesString();
				}
			}
		else
			{
			if (show_debug_warnings)
				Debug.LogWarning("You must start the game from Home scene in order to keep track of lives");
			}
	}

	public void Update_int_score(int points)
	{
		int_score += points;
		int_score_count.text = int_score.ToString("N0");
		if (show_progress_bar && progress_bar_use_score)
			my_progress_bar.Update_fill (int_score);
	}

	public void Update_int_score()
	{
		int_score_count.text = int_score.ToString("N0");
		if (show_progress_bar && progress_bar_use_score)
			my_progress_bar.Update_fill (int_score);
	}

	public void Add_stars(int quantity)
	{
		star_number += quantity;//add star
		stars_count.text = star_number.ToString() + "/" + total_collectable_stars_in_this_stage.ToString(); ;//update gui
	}

	public void New_star_score(int star_total)
	{
		star_number = star_total;//add star
		stars_count.text = star_number.ToString() + "/" + total_collectable_stars_in_this_stage.ToString(); ;//update gui
	}

	void Update_int_score_record()
		{
		if (int_score > 0)
			{
			new_record = true;

			if (show_debug_messages)
				Debug.Log("new stage int record!");

			if (my_game_master.what_say_if_new_stage_record != "")
				win_screen_int_score_record.text = my_game_master.what_say_if_new_stage_record;

			my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world-1,n_stage-1] = int_score;
			PlayerPrefs.SetInt("profile_"+my_game_master.current_profile_selected.ToString()+"_array_W"+(n_world-1).ToString()+"S"+(n_stage-1).ToString()+"_"+"stage_int_score",my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world-1,n_stage-1]);

			
			if (int_score > my_game_master.GetCurrentProfile().best_int_score_for_current_player)
				{
				if (show_debug_messages)
					Debug.Log("new personal record!");

				if (my_game_master.what_say_if_new_personal_record != "")
					win_screen_int_score_record.text = my_game_master.what_say_if_new_personal_record;

				my_game_master.GetCurrentProfile().best_int_score_for_current_player = int_score;
				PlayerPrefs.SetInt("profile_"+my_game_master.current_profile_selected.ToString()+"_best_int_score_for_this_profile",my_game_master.GetCurrentProfile().best_int_score_for_current_player);



				if (my_game_master.number_of_save_profile_slot_avaibles > 1)
					{
					if (int_score > my_game_master.best_int_score_on_this_device)
						{
						if (show_debug_messages)
							Debug.Log("new device record!");

						if (my_game_master.what_say_if_new_device_record != "")
							win_screen_int_score_record.text = my_game_master.what_say_if_new_device_record;

						my_game_master.best_int_score_on_this_device = int_score;
						PlayerPrefs.SetInt("best_int_score_on_this_device", my_game_master.best_int_score_on_this_device);
						my_game_master.GetCurrentProfile().best_int_score_for_current_player = int_score;
						PlayerPrefs.SetInt("profile_"+my_game_master.current_profile_selected.ToString()+"_best_int_score_for_this_profile",my_game_master.GetCurrentProfile().best_int_score_for_current_player);

					}
					}
				}
			}
		}

	public void Victory()
	{
		if (!stage_end)
			{	
			stage_end = true;
            doubleVirtualMoney = false;


            if (show_debug_messages)
				Debug.Log("you win " + "W"+(n_world)+"_Stage_"+(n_stage));
			allow_game_input = false;
			in_pause = true;

			//go to win screen
			play_screen.gameObject.SetActive(false);
            winScreenSkin.ShowWinsScreen(show_star_score, star_number);

            if (my_game_master)
			{
                //ads
                if (show_star_score)
                {
                    if (star_number == 2 && my_game_master.my_ads_master.winWith2Stars.thisAdIsEnabled)
                        my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.winWith2Stars);
                    else if (star_number == 3 && my_game_master.my_ads_master.winWith3Stars.thisAdIsEnabled)
                        my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.winWith3Stars);
                    else
                        my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.whenShowWinScreen);
                }
                else
                    my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.whenShowWinScreen);

                //music
                if (my_game_master.when_win_play_selected == game_master.when_win_play.music)
					my_game_master.Start_music(my_game_master.music_stage_win,my_game_master.play_win_music_in_loop);
				else if (my_game_master.when_win_play_selected == game_master.when_win_play.sfx)
					my_game_master.Gui_sfx(my_game_master.music_stage_win);

				if (my_game_master.press_start_and_go_to_selected == game_master.press_start_and_go_to.map)
					next_stage_ico.SetActive(false);
				else
					next_stage_ico.SetActive(true);

				//virtual money
				if (keep_money_taken_in_this_stage_only_if_you_win)
                {
                    if (doubleVirtualMoney)
                        temp_money_count *= 2;


                    my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(temp_money_count);
                }


                //if you have solved this stage for the first time
                if (!my_game_master.GetCurrentProfile().stage_solved[n_world-1,n_stage-1])
					{
						if (show_debug_messages)
							Debug.Log("first time win");
						//update stage count
						my_game_master.GetCurrentProfile().total_number_of_stages_in_the_game_solved++;
						my_game_master.GetCurrentProfile().stage_solved[n_world-1,n_stage-1] = true;
						//update star score
						my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1] = star_number;
						my_game_master.GetCurrentProfile().star_score_in_this_world[n_world-1] += star_number;
						my_game_master.GetCurrentProfile().stars_total_score += star_number;
						my_game_master.star_score_difference = star_number;
						//update int score
						Update_int_score_record();

					}
				else //you have solved this level more than once
					{
					if (show_debug_messages)
						Debug.Log("rewin same stage: " + star_number + " - " + my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1] + " = " + (star_number - my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1])
						          + " *** int score = " + int_score);
						
						//if your star score is better than the previous
						if (star_number > my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1])
							{
							//update star score
							my_game_master.star_score_difference = (star_number - my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1]);

							my_game_master.GetCurrentProfile().stars_total_score += (star_number-my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1]);
							my_game_master.GetCurrentProfile().stage_stars_score[n_world-1,n_stage-1] = star_number;
							my_game_master.GetCurrentProfile().star_score_in_this_world[n_world-1] += my_game_master.star_score_difference;
							if (show_debug_messages)
								Debug.Log("...with better score = " + my_game_master.star_score_difference);
							}
						else
							{
							if (show_debug_messages)
								Debug.Log("...but without better star score");
							my_game_master.star_score_difference = 0;
							}

						//if your int score is better than the previous
						if (int_score > my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world-1,n_stage-1])
							{
							//update int score
							Update_int_score_record();
							}
						else
							{
							if (show_debug_messages)
								Debug.Log("no new int_score record");
							}

					}

				//unlock the next stage if it exist
				if (n_stage < my_game_master.total_stages_in_world_n[n_world-1])
					{
					if (!my_game_master.GetCurrentProfile().stage_playable[n_world-1,n_stage])
						{
						my_game_master.GetCurrentProfile().stage_playable[n_world-1,n_stage] = true;
						my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_world = n_world-1;
                        my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage = n_stage;
						}
					}
				//unlock next world if it exist
				else if (n_world < my_game_master.total_stages_in_world_n.Length)
				{
                    my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_world = n_world;
                    my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage = 0;

					if (my_game_master.this_world_is_unlocked_after_selected[n_world] == game_master.this_world_is_unlocked_after.previous_world_is_finished)
						{
						my_game_master.GetCurrentProfile().world_playable[n_world] = true;
						my_game_master.GetCurrentProfile().stage_playable[n_world,0] = true;
						}
					else if (my_game_master.this_world_is_unlocked_after_selected[n_world] == game_master.this_world_is_unlocked_after.reach_this_star_score)
						{
						if (my_game_master.GetCurrentProfile().stars_total_score >= my_game_master.star_score_required_to_unlock_this_world[n_world])
							{
							my_game_master.GetCurrentProfile().world_playable[n_world] = true;
							my_game_master.GetCurrentProfile().stage_playable[n_world,0] = true;
							}
						}


				}
				my_game_master.Save(my_game_master.current_profile_selected);
				if (show_debug_messages)
					Debug.Log("stage score: " + star_number + " *** total score: " + my_game_master.GetCurrentProfile().stars_total_score);
				}

			if (show_int_score && !show_star_score)
				StartCoroutine(Int_score_animation(0.5f,0));

			Invoke("Mark_win",0.1f);
		}
	}

    
	public IEnumerator Int_score_animation(float wait, int start_from)
	{
		yield return new WaitForSeconds(wait);

		win_screen_int_score_title.gameObject.SetActive(true);

		//animation
		if (int_score > 0)
			{
			int temp_score = start_from;
			int add_this = int_score/100;
			float seconds = int_score/(10*int_score);

			if (add_this < 1)
				add_this = 1;

			if (seconds == 0)
				seconds = 0.01f;

			while (temp_score < int_score)
				{
				if ((temp_score+add_this) < int_score)
					temp_score += add_this;
				else
					temp_score = int_score;

				win_screen_int_score_count.text = (temp_score).ToString("N0");
				yield return new WaitForSeconds(seconds);
				}
			}

		//end animation
		win_screen_int_score_count.text = (int_score).ToString("N0");
		win_screen_int_score_record.gameObject.SetActive(new_record);
		if (new_record)
			int_score_record_anim.SetActive(true);
		//ads
		if (my_game_master)
			{
			if (my_game_master.my_ads_master.askIfDoubleIntScore.thisAdIsEnabled && !score_doubled && int_score > 0 && my_game_master.my_ads_master.Advertisement_isInitialized())
				{
					if (UnityEngine.Random.Range(1,100) <= my_game_master.my_ads_master.askIfDoubleIntScore.chanceToOpenAnAdHere)
						double_score.SetActive(true);
					
				}
			}
		
		if (win_screen.gameObject.activeSelf)
			Invoke("Mark_win",0.1f);
		else if (lose_screen.gameObject.activeSelf)
			Invoke("Mark_lose",0.1f);


	}



    public void Double_score_button()
	{
		my_game_master.Gui_sfx(my_game_master.tap_sfx);
		double_score.SetActive(false);
        my_game_master.my_ads_master.current_ad = my_game_master.my_ads_master.askIfDoubleIntScore;
		//star ad
		my_game_master.my_ads_master.Show_ad(true);//true = rewarded
		
	}

	public void Score_doubled()
	{
		double_score.SetActive(false);
		score_doubled = true;
		new_record = false;
		int old_score = int_score;
		int_score = int_score*2;

		//check if new record
		if (int_score > my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world-1,n_stage-1])
			{
			Update_int_score_record();
			my_game_master.Save(my_game_master.current_profile_selected);
			}
		
		StartCoroutine(Int_score_animation(0.25f,old_score));
	}
	
		
	void Mark_win()
	{
		if (show_debug_messages)
			Debug.Log("Mark_win()");
		Mark_this_button(win_screen_target_button);

	}
	
	public void Defeat()
	{		
		if (!stage_end)
			{	
			stage_end = true;
			if (show_debug_messages)
				Debug.Log("you lose");

			allow_game_input = false;
			in_pause = true;

			if (my_game_master)
			{
			if (my_game_master.infinite_lives)
				{	
				lose_screen_lives_ico.SetActive(false);
				retry_button.SetActive(true);
				stage_button.SetActive(true);
				}
			else
				{
				lose_screen_lives_ico.SetActive(true);
				if (my_game_master.lose_lives_selected == game_master.lose_lives.when_show_lose_screen)
					Update_lives(-1);

				if (my_game_master.GetCurrentProfile().GetCurrentLivesInt() > 0)
					{
					retry_button.SetActive(true);
					stage_button.SetActive(true);
					}
				else
					{
					retry_button.SetActive(false);
					stage_button.SetActive(false);
						if (my_game_master.continue_rule_selected == game_master.continue_rule.never_continue)
							{
							my_continue_window.my_game_master = my_game_master;
							my_continue_window.Continue_no(false);
							}
					}
				}

			}

			//go to lose screen
			play_screen.gameObject.SetActive(false);
			lose_screen.gameObject.SetActive(true);
            my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.whenLoseScreen);



            if (my_game_master)
				{
				if (my_game_master.when_lose_play_selected == game_master.when_lose_play.music)
					my_game_master.Start_music(my_game_master.music_stage_lose,my_game_master.play_lose_music_in_loop);
				else if (my_game_master.when_lose_play_selected == game_master.when_lose_play.sfx)
					my_game_master.Gui_sfx(my_game_master.music_stage_lose);

				if (my_game_master.show_score_in_lose_screen_too && show_int_score)
					{
					StartCoroutine(Int_score_animation(0.5f,0));

					//if your int score is better than the previous
					if (int_score > my_game_master.GetCurrentProfile().best_int_score_in_this_stage[n_world-1,n_stage-1])
						{
						//update int score
						Update_int_score_record();
						}
					}
				}
			Invoke("Mark_lose",0.1f);
		}
	}


	
	void Mark_lose()
	{
		Mark_this_button(lose_screen_target_button);
	}

	void Show_defeat_ad()
	{
		my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.askIfDoubleIntScore);
	}

	




}
