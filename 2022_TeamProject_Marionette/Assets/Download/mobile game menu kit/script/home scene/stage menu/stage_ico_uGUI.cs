using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
using UnityEngine.SceneManagement;
using TMPro;


public class stage_ico_uGUI : MonoBehaviour {


	[HideInInspector]public int world_number;//the world_that contain this stage
	[HideInInspector]public int stage_number;//the number of this stage, this is important to load the correct stage scene
	[HideInInspector]public int number_to_show;
	[HideInInspector]public bool padlock;//if false, this stage is playable
	[HideInInspector]public int star_number;//the score of this stage
    int star_cap = 0;//the custom star cap of this stage if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap) 

    public GameObject[] star_on;
	public GameObject[] star_off;
	public GameObject padlock_ico;
		[HideInInspector]public bool show_padlock;
	public TextMeshProUGUI number_text;
    public TextMeshProUGUI starCountText;


	//map elements
	public Image my_image_ico;
	public Image[] my_tail_dot;
	
	[HideInInspector]public manage_menu_uGUI my_manage_menu_uGUI;
	[HideInInspector]public game_master my_game_master;
	[HideInInspector]public stage_ico_uGUI next_stage_ico;

	// Use this for initialization
	public void My_start() {

		Read_savestate();
		
		//show the number of this stage
		if (my_game_master.press_start_and_go_to_selected != game_master.press_start_and_go_to.map)
			number_to_show = stage_number;
		number_text.text = number_to_show.ToString();
		
		if (!padlock)//if this level il playable
			{

			if (my_game_master.GetCurrentProfile().latest_world_played == world_number //if this is the icon of the lastest stage played
			    && my_game_master.GetCurrentProfile().latest_stage_played == stage_number)
				{
				Just_show_tail_on();

				if (my_game_master.show_star_score)
					{
					if (my_game_master.star_score_difference > 0) //if better score, show animation
						{

						Remove_padlock();

                        if (my_game_master.star_score_rule == game_master.Star_score_rule.Classic3Stars)
                            {
                            
                            for (int i = 0; i < star_off.Length; i++)
							    {
							    star_on[i].SetActive(false);
							    star_off[i].SetActive(true);
							    }

						    if (this.gameObject.activeInHierarchy)
							    {
							    if (my_game_master.star_score_difference == 3)
								    StartCoroutine(Star_animation(true,true,true));
							    else if (my_game_master.star_score_difference == 2)
								    {
								    if (star_number == 2)
									    StartCoroutine(Star_animation(true,true,false));
								    else if (star_number == 3)
									    {
									    star_on[0].SetActive(true);
									    StartCoroutine(Star_animation(false,true,true));
									    }
								    }
							    else if (my_game_master.star_score_difference == 1)
								    {
								    if (star_number == 1)
									    StartCoroutine(Star_animation(true,false,false));
								    else if (star_number == 2)
									    {
									    star_on[0].SetActive(true);
									    StartCoroutine(Star_animation(false,true,false));
									    }
								    else if (star_number == 3)
									    {
									    star_on[0].SetActive(true);
									    star_on[1].SetActive(true);
									    StartCoroutine(Star_animation(false,false,true));
									    }
								    }
                                }
						    else
							    Just_show_the_score();
                            }
                        else if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
                            Just_show_the_score();
                        }
					else
						{
						Just_show_the_score();
						}
					}
				else
					{
					Just_show_the_score();
					}
				}
			else if (my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_world == world_number-1
			         && my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage == stage_number-1)//this is the next stage to play to_progress_in_the_game
				{	
				if (my_tail_dot.Length > 0)
					{	
					if (my_game_master.GetCurrentProfile().dot_tail_turn_on[world_number-1,stage_number-1])
						Just_show_the_score();
					else
						{
						if (my_manage_menu_uGUI.stage_ico_update_animation_is_running)
							Put_padlock();
						else
							{
                            my_game_master.GetCurrentProfile().dot_tail_turn_on[world_number-1,stage_number-1] = true;
							Remove_padlock();
							Just_show_the_score();
							}
						}
					}
				else
					Just_show_the_score();
				}
			else //just show the score
				{
				Just_show_the_score();
				}
		




		}
		else//you can't play this stage yet
		{
			Reset_tail();
			Put_padlock();
		}

	}


	void Turn_off_stars()
	{
		star_number = 0;
		for(int i = 0; i < star_on.Length; i++)
			star_on[i].SetActive(false);
	}

	void Reset_tail()
	{
		if (my_tail_dot.Length > 0)
		{
			for(int i = 0; i < my_tail_dot.Length; i++)
				my_tail_dot[i].sprite = SkinMaster.THIS.currentUISkin.stageButtonTailDotOff;
		}
	}

	void Tail_animation()
	{
		//show map tail dots if need
		if (my_tail_dot.Length > 0)
		{
				if (this.gameObject.activeInHierarchy)
					StartCoroutine(Turn_on_this_dot(0,my_game_master.star_score_difference*0.5f));
				else
					{
					Just_show_tail_on();
                my_game_master.GetCurrentProfile().dot_tail_turn_on[world_number-1,stage_number-1] = true;
					my_game_master.Save(my_game_master.current_profile_selected);
					}
		}
		else
			Remove_padlock();
	}

	void Just_show_tail_on()
		{
		if (my_tail_dot.Length > 0)
			{
			if (my_game_master.GetCurrentProfile().dot_tail_turn_on[world_number-1,stage_number-1])//show the tail on
				{
				Remove_padlock();
				for(int i = 0; i < my_tail_dot.Length; i++)
					my_tail_dot[i].sprite = SkinMaster.THIS.currentUISkin.stageButtonTailDotOn;
				}
			}
		else
			Remove_padlock();
		}
	
	void Put_padlock()
	{
		if (show_padlock)
			padlock_ico.SetActive(true);
		

	        my_image_ico.sprite = SkinMaster.THIS.currentUISkin.stageButtonOff;
		
		Not_show_star_score();
	}

	void Not_show_star_score()
		{
		for(int i = 0; i < star_off.Length; i++)
			{
			star_on[i].SetActive(false);
			star_off[i].SetActive(false);
            }
        starCountText.gameObject.SetActive(false);
        }

	void Remove_padlock()
	{
		padlock_ico.SetActive(false);
	
		//update ico if need
		if (my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_world == world_number-1
		    && my_game_master.GetCurrentProfile().play_this_stage_to_progress_in_the_game_stage == stage_number-1)
			{
			my_image_ico.sprite = SkinMaster.THIS.currentUISkin.stageButtonNextStageToPlay;
			}
		else
			{
            my_image_ico.sprite = SkinMaster.THIS.currentUISkin.stageButtonOn;
			}

		if (my_game_master.show_star_score)
			{
            if (my_game_master.star_score_rule == game_master.Star_score_rule.Classic3Stars)
            {
                for (int i = 0; i < star_off.Length; i++)//show star score
                    star_off[i].SetActive(true);
                starCountText.gameObject.SetActive(false);
            }
           // else if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
                //Just_show_the_score();
        }
	}
	
	void Just_show_the_score()
	{
		if (my_game_master.show_star_score)
			{
            if (my_game_master.star_score_rule == game_master.Star_score_rule.Classic3Stars)
            {
                for (int i = 0; i < star_off.Length; i++)//show star score
				{

				if ( (star_number-1) >= i )
					{
					star_on[i].SetActive(true);
					star_off[i].SetActive(false);
					}
				else
					{
					star_on[i].SetActive(false);
					star_off[i].SetActive(true);
					}
				}
            }
            else if (my_game_master.star_score_rule == game_master.Star_score_rule.EachStageHaveHisOwnStarCap)
            {
                star_off[0].SetActive(false);
                star_off[1].SetActive(false);
                star_off[2].SetActive(false);

                star_cap = my_game_master.GetCurrentProfile().stage_stars_cap_score[world_number-1, stage_number - 1];

                if (star_cap > 0)
                    {
                    star_on[2].SetActive(true);

                    starCountText. text = star_number + "/" + star_cap.ToString();

                    starCountText.gameObject.SetActive(true);
                    }
                else
                    {
                    star_on[2].SetActive(false);
                    starCountText.gameObject.SetActive(false);
                    }
            }
        }
		else
			Not_show_star_score();
			

		Just_show_tail_on();
		}
/*
	IEnumerator Wait(float seconds)
	{
		Debug.Log(stage_number + " - start wait");
		yield return new WaitForSeconds(seconds);
		Debug.Log(stage_number + " - end wait");
	}*/

	IEnumerator Star_animation(bool start_1,bool start_2,bool start_3)
	{
		//Debug.Log(stage_number + " - Star_animation: "+start_1+","+start_2+","+start_3 + " *** " + Time.timeScale);
		my_manage_menu_uGUI.stage_ico_update_animation_is_running = true;
		yield return new WaitForSeconds(0.5f);
		if (start_1)
			{
			star_on[0].SetActive(true);
			star_on[0].GetComponent<Animation>().Play();
			my_game_master.Gui_sfx(my_manage_menu_uGUI.show_mini_star[0]);

			if (start_2)
				StartCoroutine(Star_animation(false,start_2,start_3));
			else //end animation
				Check_tail_in_next_icon();
			}
		else if (start_2)
			{
			star_on[1].SetActive(true);
			star_on[1].GetComponent<Animation>().Play();
			my_game_master.Gui_sfx(my_manage_menu_uGUI.show_mini_star[1]);
			if (start_3)
				StartCoroutine(Star_animation(false,false,start_3));
			else //end animation
				Check_tail_in_next_icon();
			}
		else if (start_3)
			{
			star_on[2].SetActive(start_3);
			star_on[2].GetComponent<Animation>().Play();
			my_game_master.Gui_sfx(my_manage_menu_uGUI.show_mini_star[2]);
			//end animation
			Check_tail_in_next_icon();
			}
	}

	void Check_tail_in_next_icon()
	{
		if (next_stage_ico && (next_stage_ico.my_tail_dot.Length > 0) ) //if there is another stage ico with a tail
		{
			next_stage_ico.Tail_animation();
		}
		else
		{
			my_game_master.star_score_difference = 0;
			my_game_master.Save(my_game_master.current_profile_selected);
			my_manage_menu_uGUI.stage_ico_update_animation_is_running = false;
		}
	}

	IEnumerator Turn_on_this_dot(int current_dot, float start_delay)
	{
		if (start_delay > 0)
			yield return new WaitForSeconds(start_delay);

		yield return new WaitForSeconds(0.25f);
		my_tail_dot[current_dot].sprite = SkinMaster.THIS.currentUISkin.stageButtonTailDotOn;

		if(current_dot+1 < my_tail_dot.Length)
			StartCoroutine(Turn_on_this_dot(current_dot+1,0));
		else //end the animation
			{
			yield return new WaitForSeconds(0.25f);
			Remove_padlock();
            my_game_master.GetCurrentProfile().dot_tail_turn_on[world_number-1,stage_number-1] = true;
			my_game_master.Save(my_game_master.current_profile_selected);
			my_manage_menu_uGUI.stage_ico_update_animation_is_running = false;
			}
	}
	
	public void You_tap_me()
	{

			if (padlock)
			{
				if (!GetComponent<Animation>().isPlaying)
				{
				my_game_master.Gui_sfx(my_game_master.tap_error_sfx);
				GetComponent<Animation>().Play("tap_icon_error");
				}
			}
			else
			{
			if (my_image_ico.sprite != SkinMaster.THIS.currentUISkin.stageButtonOff)//be sure that unlock animation is done
				{
				my_game_master.Gui_sfx(my_game_master.tap_sfx);
				my_game_master.my_ads_master.Call_ad(my_game_master.my_ads_master.beforeStartToPlayAStage);
				Invoke("Load_stage",0.5f);
				}
			}

	}

	void Load_stage()
	{
		//my_game_master.Unlink_me_to_camera();
		if (my_game_master.show_debug_messages)
			Debug.Log("Load: " + "W"+world_number.ToString()+ "_Stage_" + stage_number.ToString());
		if (my_game_master.show_loading_screen)
			my_manage_menu_uGUI.loading_screen.gameObject.SetActive(true);

        if (my_game_master.use_same_scene_for_all_stages_in_the_same_world)
        {
            my_game_master.current_stage = stage_number;
            SceneManager.LoadScene("W" + world_number.ToString() + "_Stage_" + 1);//for each world use only one scene W1_stage_1 , W2_stage_1 , and so on...
        }
        else
            SceneManager.LoadScene("W" + world_number.ToString() + "_Stage_" + stage_number.ToString()); //put your scenes in "Scenes in build" with names like: W1_Stage_1, W1_Stage_2 and so on... 
    }
	
	void Read_savestate()	
	{
		//reset
		Turn_off_stars();
		padlock_ico.SetActive(true);

		if (my_game_master.GetCurrentProfile().stage_playable[world_number,stage_number-1])
			padlock = false;
		else
			padlock = true;
		
		if (my_game_master.GetCurrentProfile().stage_solved[world_number,stage_number-1])
            {
            star_number = my_game_master.GetCurrentProfile().stage_stars_score[world_number,stage_number-1];
            //star_cap = my_game_master.stage_stars_cap_score[my_game_master.current_profile_selected][world_number, stage_number - 1];
            }

        world_number++;//count world 0 as W1 and so on...
	}
}
