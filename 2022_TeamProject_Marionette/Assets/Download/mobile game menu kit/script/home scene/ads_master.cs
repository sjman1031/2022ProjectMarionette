using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ADS
using UnityEngine.Advertisements;//this allow you to use unity-ads
#endif
using System;

public class ads_master : MonoBehaviour {

    [HideInInspector] public bool noAdsPurchased;

    //Unity Ads
    public string rewardedVideoZone = "rewardedVideo";
	public bool enable_ads;
	public bool ads_test_mode;
	public string iOS_ads_app_id;
	public string android_ads_app_id;
	string app_id_selected;
    public float minimum_time_from_app_start_before_show_the_first_ad;
    public float minimum_time_interval_between_ads;
	float time_of_latest_ad_showed;
	public bool reward_feedback_after_ad;

	[HideInInspector] public feedback_window my_feedback_window;
    [HideInInspector] public checkpoint target_checkpoint;
    [HideInInspector] public game_uGUI my_game_uGUI;
    [HideInInspector] public Info_bar my_info_bar;
    //show reward ad gui
    [HideInInspector] public gift_manager my_gift_manager;
    [HideInInspector] public game_master my_game_master;

    //debug
    public bool show_debug_warnings;
    public bool show_debug_messages;


    public enum allow_double_int_score_ads_if
	{
		no,
		new_stage_record,
		new_personal_record,
		new_device_record,
		new_online_record
	}
	
	public enum ad_main_rule
	{
        rewardedButCantSkip,
        skipableButRewardOnlyIfDontSkip
	}
	
	public DateTime start_app_ad_target_time;
	
	[System.Serializable]
	public class AdOptions
	{
        public bool thisAdIsEnabled;
        public bool UseCustomSettings = false;
        public bool showCustomSettingsInInspector;

        public bool ignoreMinimumTimeIntervalBetweenAds;

        /*
        public int startAppAdWaitForDays;
		public int startAppAdWaitForHours;
		public int startAppAdWaitForMinutes;
        */

	
        [Range(1,100)]
		public int chanceToOpenAnAdHere = 50;//automatically or by asking
		
		public bool askToPlayerIfHeWantToWatchAnAdBeforeStartIt;
		public string askingText;
        public int virtualMoneyCostToGetTheRewardWithoutWatchTheAd;

        [Range(0, 100)]
        public int chanceToRewardIfTheAdIsSkipped;

        [Range(1, 4)]
        public int numberOfAvaiblesChoices = 1;

        public List<AdReward> myRewards;//rewards that this add can give
    


    }

    [System.Serializable]
    public class AdReward
    {
        public enum AdRewardType
        {
            consumable,
            inGameUnlockCheckpoint,

            doubleScore,
            winScreen_doubleVirtualMoneyGainedInThisStage,
            winScreenGetOneMoreStar,//exaple: palyer win with 2 stars and get the third as ad reward

            allowContinueAfterGameover,
            live,
            continueToken,
            virtualMoney
        }
        public AdRewardType adRewardTypeSelected;

        public ConsumableItem myConsumable;
        public int minQuantity;//for consumables, lives, continueToken, and virtualMoney
        public int maxQuantity;//for consumables, lives, continueToken, and virtualMoney

        public string myName;
        public string myDescription;
    }

    [Space()]
    [Space()]
    public AdOptions adsDefaultSettings;

    [Space()]
    [Space()]
    [Header("Specific ads settings for these events:")]
    //special settings
    //home scene:
    [Header("- home scene:")]
    [Tooltip("Thise require the logo screen in order to hide the time needed to connect to the ads server")]
	public AdOptions justAfterLogo;
	public AdOptions beforeStartToPlayAStage;
	public AdOptions whenReturnToHomeSceneFromAStage;
    //game scene:
    [Header("- game scene:")]
    //win screen:
    [Header("   - win screen:")]
	public AdOptions whenShowWinScreen;
	//public AdOptions whenGainVirtualMoneyInTheCurrentStage;
    [Tooltip("if you win the stage with 2 stars score")]
    public AdOptions winWith2Stars;
    public AdOptions winWith3Stars;
    //lose screen
    [Header("   - lose screen:")]
    public AdOptions whenLoseScreen;
    public AdOptions whenContinueScreenAppear;
    //in game
    [Header("   - in game:")]
	public AdOptions whenStageStart;
	public AdOptions whenReachACheckpoint;
	public AdOptions whenPlayerOpenAGiftPackage;
	//score
	public AdOptions askIfDoubleIntScore;
	
	//the ad and the reward selected
    [HideInInspector]
	public AdOptions current_ad = null;

    [HideInInspector]
    public int buy_button_cost;//only when you whant allow to pay instead of watch the video ad
	
    [System.Serializable]
    public class AvaibleReward //the reward 
    {
        public AdReward reward;
        public int quantity;
    }
    AvaibleReward[] avaibleRewards;//the reward to show in the gif window (if more than one, the player can select which he/she wants)
    AvaibleReward selectedReard;//the reward selected

    public void Initiate_ads()
	{
    #if UNITY_ADS

        if (enable_ads && !Advertisement.isInitialized)
		{
			current_ad = null;
			app_id_selected = "";
			#if UNITY_IPHONE
			app_id_selected = iOS_ads_app_id;
			#elif UNITY_ANDROID
			app_id_selected = android_ads_app_id;
			#endif
			if (app_id_selected == "" && show_debug_warnings)
				Debug.LogWarning("No app_id found");
			
		}
    #endif
    }
	
    public AvaibleReward[] GetAvaibleRewards()
    {
        return avaibleRewards;
    }

    public void Show_ad(bool rewarded)
	{
		if (show_debug_messages)
			Debug.Log("Show_ad("+rewarded+") - id = " + app_id_selected);
		
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd());
        #endif

        #if UNITY_ADS
        string zone = null;
		if (rewarded)
			zone = rewardedVideoZone;
		
		ShowOptions options = new ShowOptions();
		options.resultCallback = AdCallbackhandler;
		
		if (Advertisement.IsReady(zone))
		{
			Advertisement.Show(zone, options); //this work ONLY if "File" > "build setting" is set on iOS or Android
		}
        #endif

    }

    #if UNITY_ADS
    void AdCallbackhandler(ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished: //player had watched the whole video
			if (show_debug_messages)
				Debug.Log("Ad Finished: give reward?");

                Give_reward();

			time_of_latest_ad_showed = Time.realtimeSinceStartup;
			Empty_ad();
			break;
			
		case ShowResult.Skipped: //player had skipped the video (only not rewarded ad can be skipped)
			if (show_debug_messages)
				Debug.Log("Ad skipped. Give penalty?");

                if (UnityEngine.Random.Range(1, 100) <= current_ad.chanceToRewardIfTheAdIsSkipped)
                    Give_reward();

                time_of_latest_ad_showed = Time.realtimeSinceStartup;
			Empty_ad();
			break;
			
		case ShowResult.Failed: //for some reason the video not start
			if (show_debug_messages)
				Debug.LogWarning("Ad video Failed");
			Empty_ad();
			break;
		}
	}
    #endif

    void Empty_ad()
	{
		current_ad = null;
        selectedReard = null;
        buy_button_cost = 0;
        
	}

    public Sprite GetRewardIcon(AdReward reward)
    {
        if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.consumable)
            return reward.myConsumable.icon;
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.live)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.Lives);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.virtualMoney)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.winScreen_doubleVirtualMoneyGainedInThisStage)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.VirtualMoney);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.continueToken)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.ContinueToken);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.allowContinueAfterGameover)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.ContinueToken);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.winScreenGetOneMoreStar)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.StarOn);
        else if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.inGameUnlockCheckpoint)
            return SkinMaster.THIS.currentUISkin.GetIcon(UI_Skin.Icon.PadlockSmall);
        

        return null;
    }

    public string GetRewardName(AdReward reward)
    {
        if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.consumable)
            return reward.myConsumable.myName;

        return reward.myName;
    }

    public string GetRewardDescription(AdReward reward)
    {
        if (reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.consumable)
            return reward.myConsumable.description;

        return reward.myDescription;
    }

    public void Give_reward()
	{
        switch (selectedReard.reward.adRewardTypeSelected)
        {
            case AdReward.AdRewardType.consumable:
                my_game_master.GetCurrentProfile().AddCustomConsumableItem(selectedReard.reward.myConsumable, selectedReard.quantity);
                break;

            case AdReward.AdRewardType.doubleScore:
                my_game_uGUI.Score_doubled();
                break;

            case AdReward.AdRewardType.inGameUnlockCheckpoint:
                target_checkpoint.Enable_this_checkpoint();
                break;

            case AdReward.AdRewardType.allowContinueAfterGameover:
                my_game_uGUI.Update_lives(my_game_master.continue_give_new_lives);
                break;

            case AdReward.AdRewardType.winScreen_doubleVirtualMoneyGainedInThisStage:
                my_game_uGUI.doubleVirtualMoney = true;
                break;

            case AdReward.AdRewardType.winScreenGetOneMoreStar:
                my_game_uGUI.Add_stars(1);
                break;

            case AdReward.AdRewardType.continueToken:
                my_game_master.GetCurrentProfile().UpdateContinueTokens(selectedReard.quantity);
                break;

            case AdReward.AdRewardType.live:
                my_game_master.GetCurrentProfile().UpdateLives(selectedReard.quantity);
                break;

            case AdReward.AdRewardType.virtualMoney:
                my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(selectedReard.quantity);
                break;
        }

        if (reward_feedback_after_ad)
            my_feedback_window.Start_me(GetRewardIcon(selectedReard.reward), selectedReard.quantity, GetRewardName(selectedReard.reward));

        if (my_info_bar)
			my_info_bar.Update_me();
		
		my_game_master.Save(my_game_master.current_profile_selected);
		Reset_reward();
		
	}
	
	public void Reset_reward()
	{
		current_ad = null;
        target_checkpoint = null;
        selectedReard = null;
        buy_button_cost = 0;
        
    }
	
	
	public float Choose (float[] probs) {
		
		float total = 0;
		
		foreach (float elem in probs) {
			total += elem;
		}
		
		float randomPoint = UnityEngine.Random.value * total;
		
		for (int i= 0; i < probs.Length; i++) {
			if (randomPoint < probs[i]) {
				return i;
			}
			else {
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
	
	
	IEnumerator WaitForAd()
	{
		//pause the game when the ad is showing
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0; 
		yield return null;

    #if UNITY_ADS
        while (Advertisement.isShowing)
			yield return null;
    #endif

        Time.timeScale = currentTimeScale; 
	}
	
	public bool Advertisement_isInitialized()
	{
    #if UNITY_ADS
        return Advertisement.isInitialized;
    #endif
        return false;
	}

    public void SeletReward(int id)
    {
        selectedReard = avaibleRewards[id];
    }

    public bool TimeCheckOK(AdOptions target_ad)
    {
        if (Time.realtimeSinceStartup < minimum_time_from_app_start_before_show_the_first_ad)
            return false;

        if (target_ad.ignoreMinimumTimeIntervalBetweenAds)
            return true;

        if (time_of_latest_ad_showed == 0)
            return true;

        if ((minimum_time_interval_between_ads + time_of_latest_ad_showed) < Time.realtimeSinceStartup)
            return true;

        return false;
    }

    public void Call_ad(AdOptions target_ad)
	{
        if (noAdsPurchased)
            return;

		if (enable_ads && target_ad.thisAdIsEnabled)
		{
            if (!target_ad.UseCustomSettings)
                target_ad = adsDefaultSettings;

            current_ad = target_ad;

            if (TimeCheckOK(target_ad))
            {
				if (show_debug_messages)
					Debug.Log("ad pass time check");
				
				if (UnityEngine.Random.Range(1,100) <= target_ad.chanceToOpenAnAdHere)
				{
					if (show_debug_messages)
						Debug.Log("ads_just_after_logo_when_game_start_as_daily_reward" + " - random ok");
					
					if (target_ad.askToPlayerIfHeWantToWatchAnAdBeforeStartIt)
					{

                            //pick some radom reward from the reward array (its will be show as choices for the player)
                            //never pick the same reward again
                            if (target_ad.numberOfAvaiblesChoices > target_ad.myRewards.Count)
                                target_ad.numberOfAvaiblesChoices = target_ad.myRewards.Count;

                            avaibleRewards = new AvaibleReward[target_ad.numberOfAvaiblesChoices];
                            List<int> availableRewardIds = new List<int>();
                            for (int i = 0; i < target_ad.myRewards.Count; i++)
                                availableRewardIds.Add(i);

                            for (int i = 0; i < target_ad.numberOfAvaiblesChoices; i++)
                                {
                                int randomPick = UnityEngine.Random.Range(0, availableRewardIds.Count);

                                avaibleRewards[i] = new AvaibleReward();
                                avaibleRewards[i].reward = target_ad.myRewards[availableRewardIds[randomPick]];
                                avaibleRewards[i].quantity = UnityEngine.Random.Range(avaibleRewards[i].reward.minQuantity, avaibleRewards[i].reward.maxQuantity + 1);
                                availableRewardIds.RemoveAt(randomPick);
                                }
                            
                        
                        
						my_gift_manager.Start_me(target_ad.askingText);
					}
					else
					{
						if (show_debug_messages)
							Debug.Log("ad start automatically");

							//Set_app_start_ad_countdown();
						
						//star ad
						Show_ad(false);//false = not rewarded
					}
				}
				else
				{
					if (show_debug_messages)
						Debug.Log("ad - random chance test fail");
				}
			}
			else
			{
				if (show_debug_messages)
					Debug.Log("ad don't pass time check");
			}
		}
	}

	#region start ad countdown
    /*
	public void Set_app_start_ad_countdown()
	{
		start_app_ad_target_time = DateTime.Now;
		start_app_ad_target_time = start_app_ad_target_time.AddDays((double)justAfterLogo.startAppAdWaitForDays);
		start_app_ad_target_time = start_app_ad_target_time.AddHours((double)justAfterLogo.startAppAdWaitForHours);
		start_app_ad_target_time = start_app_ad_target_time.AddMinutes((double)justAfterLogo.startAppAdWaitForMinutes);
		
		//save
		PlayerPrefs.SetString("start_app_ad_target_time",start_app_ad_target_time.ToString());
		
		if (show_debug_messages)
			Debug.Log("start app ad target time: " + start_app_ad_target_time);
		
	}
	*/
    /*
	public bool Check_app_start_ad_countdown()
	{
		//load
		string temp_string = PlayerPrefs.GetString("start_app_ad_target_time");
		
		Debug.Log("Check_app_start_ad_countdown: " + temp_string);
		
		if (temp_string != "")
		{
			start_app_ad_target_time = DateTime.Parse(temp_string);
			
			if (show_debug_messages)
				Debug.Log("start app ad target time: " + start_app_ad_target_time + " *** DateTime.Now = " + DateTime.Now);
			
			if (start_app_ad_target_time != null)
			{
				if (start_app_ad_target_time <= DateTime.Now)
					return true;
				else
					return false;
			}
			else
				return true;
		}
		else
			return true;
		
		
	}
	*/
	#endregion
}

