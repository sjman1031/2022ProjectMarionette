using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Info_bar : MonoBehaviour {

	public Transform my_stars;
	public Transform my_lives;
        public Transform lives_timer;
        public TextMeshProUGUI lives_timer_text;
    public Transform my_tokens;
	public Transform my_virtual_money;

	game_master my_game_master;
	public manage_menu_uGUI my_manage_menu_uGUI;
	public store_tabs my_store_tabs;

	// Use this for initialization
	void Start () {
		if (game_master.game_master_obj)
			my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
	}
	
	public void Show_info_bar(bool show)
	{
		if (show)
			{
			this.gameObject.SetActive(true);

			if (my_game_master == null)
				{
				if (game_master.game_master_obj)
					my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
				}

			//show star score
			if (my_game_master.show_star_score)
				{
				my_stars.gameObject.SetActive(true);
				my_stars.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().stars_total_score.ToString("N0");
				}
			else
				my_stars.gameObject.SetActive(false);

			//show lives
			if (my_game_master.infinite_lives)
				{
				my_lives.gameObject.SetActive(false);
				my_tokens.gameObject.SetActive(false);
				}
			else
				{
				my_lives.gameObject.SetActive(true);
                my_lives.GetChild(2).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentLivesString(); 

                if (my_game_master.gain_an_extra_live_each_x_minutes > 0 && my_game_master.GetCurrentProfile().GetCurrentLivesInt() > 0 && my_game_master.GetCurrentProfile().GetCurrentLivesInt() < my_game_master.live_cap_for_lives_gained_with_timer)
                    lives_timer.gameObject.SetActive(true);
                else
                    lives_timer.gameObject.SetActive(false);


                if ((my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token) && (!my_game_master.my_ads_master.whenContinueScreenAppear.thisAdIsEnabled))
					{
					my_tokens.gameObject.SetActive(true);
					my_tokens.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentContinueTokesString();
					}
				else
					my_tokens.gameObject.SetActive(false);
				}

			//show virtual money
			if (my_game_master.my_store_item_manager.store_enabled)
				{
				my_virtual_money.gameObject.SetActive(true);
                my_virtual_money.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyString();
				if (my_game_master.my_store_item_manager.can_buy_virtual_money_with_real_money)
					my_virtual_money.GetChild(1).gameObject.SetActive(true);
				else
					my_virtual_money.GetChild(1).gameObject.SetActive(false);
				}
			else
				my_virtual_money.gameObject.SetActive(false);
			}
		else
			{
			this.gameObject.SetActive(false);
			}
	}

	public void Update_me()
	{
		if (my_game_master== null)
		{
			if (game_master.game_master_obj)
				my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
		}

		if (my_game_master.show_star_score)
			my_stars.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().stars_total_score.ToString();

		if (!my_game_master.infinite_lives)
			{
            my_lives.GetChild(2).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentLivesString();

            lives_timer.gameObject.SetActive(my_game_master.CheckIfStatExtraliveCountdown());

            if ((my_game_master.continue_rule_selected == game_master.continue_rule.continue_cost_a_continue_token)&& (!my_game_master.my_ads_master.whenContinueScreenAppear.thisAdIsEnabled))
				my_tokens.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentContinueTokesString();
			}
        my_virtual_money.GetChild(0).GetComponent<TextMeshProUGUI>().text = my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyString(); 
	}
    
    private void Update()
    {
        //gain extra lives timer
        if (!lives_timer.gameObject.activeSelf)
            return;

        //if not exist a target time yet, note it now
        if (my_game_master.GetCurrentProfile().target_time.Year == 0001)
        {
            my_game_master.Set_date_countdown(true);
            my_game_master.Save(my_game_master.current_profile_selected);
        }

        lives_timer_text.text = my_game_master.Show_how_much_time_left();

        if (my_game_master.timerStatus == game_master.TimerStatus.done)
        {
            my_game_master.timerStatus = game_master.TimerStatus.Off;
            Update_me();
        }

    }

    public void Buy_virtual_money_with_real_money(int storeMoneyTab = 4)
	{
		//open store screen if not open yet
		if (my_manage_menu_uGUI.current_screen != my_manage_menu_uGUI.store_screen)
			{
			my_manage_menu_uGUI.Go_to_store_screen(storeMoneyTab);
			}
		else 
			my_store_tabs.Select_this_tab(storeMoneyTab);

	}
}
