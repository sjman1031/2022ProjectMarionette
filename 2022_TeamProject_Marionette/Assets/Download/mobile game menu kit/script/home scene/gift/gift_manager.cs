using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class gift_manager : MonoBehaviour {

    StoreManager.ItemContainer myConsumableItemContainer;

    //window
    public TextMeshProUGUI window_text_heading;
	public TextMeshProUGUI window_text_message;
	public TextMeshProUGUI window_text_item_name;
	public TextMeshProUGUI window_text_item_description;
	//window rect transform
		public RectTransform my_window_rect;
		float normal_height;
		public float small_height;

	//buy button
	public GameObject buy_button;
	public TextMeshProUGUI buy_button_text;
	game_uGUI my_game_uGUI;

	//ads
	public Image ads_button_ico;
    public gift_button[] giftButtons;
    int selected_button;
    int activeButtons;

	public GameObject gift_screen;

	[HideInInspector]public game_master my_game_master;
	public GameObject EventSystem_obj;

	float currentTimeScale;

	void Awake()
	{
        if (SceneManager.GetActiveScene().name != "Home")
            my_game_uGUI = GameObject.FindGameObjectWithTag("_gui_").GetComponent<game_uGUI>();
	}
	
	public void Start_me (string window_message) {

		if (normal_height == 0)
			normal_height = my_window_rect.rect.height;

		//start pause
		currentTimeScale = Time.timeScale;
		Time.timeScale = 0; 

		if (my_game_master.use_pad)
			EventSystem_obj.SetActive(false);//in order to avoid pad input out the of the window


		window_text_message.text = window_message;
		my_game_master.a_window_is_open = true;

        //reset variables
        activeButtons = 0;

        for (int i = 0; i < giftButtons.Length; i++)
            giftButtons[i].gameObject.SetActive(false);

        for (int i = 0; i < my_game_master.my_ads_master.GetAvaibleRewards().Length; i++)
        {

            activeButtons++;
            giftButtons[i].Start_me(i, //id
                my_game_master.my_ads_master.GetRewardIcon(my_game_master.my_ads_master.GetAvaibleRewards()[i].reward),//icon
                my_game_master.my_ads_master.GetRewardName(my_game_master.my_ads_master.GetAvaibleRewards()[i].reward), //name
                my_game_master.my_ads_master.GetRewardDescription(my_game_master.my_ads_master.GetAvaibleRewards()[i].reward), //description
                my_game_master.my_ads_master.GetAvaibleRewards()[i].quantity);//quantity
        }

        selected_button = 0;
        giftButtons[selected_button].SelectMe();

        if (my_game_master.my_ads_master.current_ad.virtualMoneyCostToGetTheRewardWithoutWatchTheAd > 0)
        {
            buy_button_text.text = my_game_master.my_ads_master.current_ad.virtualMoneyCostToGetTheRewardWithoutWatchTheAd.ToString("n0");
            buy_button.SetActive(true);
        }
        else
            buy_button.SetActive(false);


        gift_screen.SetActive(true);
        
        Check_internet();

	}

	void Check_internet()
	{
		Debug.Log("Check_internet()");
		if (gift_screen.activeSelf)
		{
			if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork && my_game_master.my_ads_master.Advertisement_isInitialized())
				{
				//ads_button_ico.sprite = ads_button_internet_on;
				}
			else
				{
				//ads_button_ico.sprite = ads_button_internet_off;//if ads can't work because no internet connection
				//Invoke("Check_internet",1);
				}
		}
	}


    public void UpdateSelection()
    {
        for (int i = 0; i < giftButtons.Length; i++)
        {
            if (!giftButtons[i].gameObject.activeSelf)
                continue;

            if (giftButtons[i].myToggle.isOn && i != selected_button)
            {
                giftButtons[i].SelectMe();
            }
            else if (!giftButtons[i].myToggle.isOn)
            {
                giftButtons[i].selectionImage.sprite = SkinMaster.THIS.currentUISkin.profileItemOn;
            }
        }
    }

    public void Select_this_button(int button_id, string _name, string _desctription)
	{
        selected_button = button_id;
        window_text_item_name.text = _name;
        window_text_item_description.text = _desctription;
        my_game_master.my_ads_master.SeletReward(selected_button);
        
    }

	public void Watch_the_video_ad()
	{
		if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork && my_game_master.my_ads_master.Advertisement_isInitialized()) 
			{
			my_game_master.Gui_sfx(my_game_master.tap_sfx);

			//if (my_game_master.my_ads_master.current_ad == my_game_master.my_ads_master.justAfterLogo)
				//my_game_master.my_ads_master.Set_app_start_ad_countdown();


			//close gift window
			gift_screen.SetActive(false);
			my_game_master.a_window_is_open = false;
			Debug.Log("Watch_the_video_ad() " + Time.timeScale + currentTimeScale);
			Time.timeScale = currentTimeScale; 
			if (my_game_master.use_pad)
				EventSystem_obj.SetActive(true);

			//star ad
			my_game_master.my_ads_master.Show_ad(true);//true = rewarded
			}
		else
			my_game_master.Gui_sfx(my_game_master.tap_error_sfx);
	}

	public void PayInsteadOfWatch()
	{
		if (my_game_master.my_ads_master.buy_button_cost <= my_game_master.GetCurrentProfile().GetCurrentVirtualMoneyInt())
		{
			//you have enough money
			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			//pay
			my_game_uGUI.Update_virtual_money(-my_game_master.my_ads_master.buy_button_cost);
			//gain
			my_game_master.my_ads_master.Give_reward();
			//close gift window
			gift_screen.SetActive(false);
			my_game_master.a_window_is_open = false;
			Time.timeScale = currentTimeScale; 
			if (my_game_master.use_pad)
				EventSystem_obj.SetActive(true);

		}
		else//you can't effort this purchase
		{
			my_game_master.Gui_sfx(my_game_master.tap_error_sfx);
		}
	}

	public void Close_me()
	{		

		//end pause
		Time.timeScale = currentTimeScale; 

		my_game_master.Gui_sfx(my_game_master.tap_sfx);
		my_game_master.my_ads_master.Reset_reward();
		my_game_master.a_window_is_open = false;
		if (my_game_master.use_pad)
			EventSystem_obj.SetActive(true);
		gift_screen.SetActive(false);
	}

	//pad input
	void Update()
	{
		if (my_game_master.use_pad)
		{
			if (Input.GetKeyDown(my_game_master.pad_next_button))
				Next();
			else if (Input.GetKeyDown(my_game_master.pad_previous_button))
				Previous();
			
			if (Input.GetButtonDown("Submit"))
				Watch_the_video_ad();
			else if (Input.GetKeyDown(my_game_master.pad_back_button))
				Close_me();
		}
		
		if (Input.GetKeyDown (KeyCode.Escape) && my_game_master.allow_ESC)
			Close_me();
		
		
	}
	
	void Next()
	{
		if (selected_button+1 < activeButtons)
            giftButtons[selected_button + 1].SelectMe();
		else
            giftButtons[0].SelectMe();
		
		
	}
	
	void Previous()
	{
		if (selected_button > 0)
            giftButtons[selected_button -1].SelectMe();
        else
            giftButtons[activeButtons - 1].SelectMe();
	}

}
