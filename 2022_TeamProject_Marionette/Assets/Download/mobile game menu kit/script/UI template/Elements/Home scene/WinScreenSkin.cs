using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenSkin : UI_Template
{

    [SerializeField]
    protected Image[] starsOn;
    [SerializeField]
    protected Image[] starsOff;
    [SerializeField]
    protected Image myBk;
    [SerializeField]
    protected Image emoticon;

    [Space()]
    public game_uGUI my_game_uGUI;
    game_master my_game_master;
    bool show_int_score;

    public float delay_start_star_score_animation = 1;
    public float delay_star_creation = 1; // recommend value = 1
    public GameObject star_container;
    public GameObject[] stars_on;
    int invoke_count = 0;
    int total_collectable_stars_in_this_stage;

    protected override void LoadUISkin()
    {

        base.LoadUISkin();

        UpdateImage(myBk, currentUISkin.winScreenBK);

        for (int i = 0; i < 3; i ++)
        {
            starsOn[i].sprite = currentUISkin.bigStarOn;
            starsOff[i].sprite = currentUISkin.bigStarOff;
        }

        currentUISkin.GetEmoticon(UI_Skin.EndScreenEmotion.Normal);
    }


    public void ResetWinScreen(game_master _my_game_master, int _total_collectable_stars_in_this_stage, bool _show_int_score)
    {
        this.gameObject.SetActive(false);

        total_collectable_stars_in_this_stage = _total_collectable_stars_in_this_stage;
        my_game_master = _my_game_master;
        show_int_score = _show_int_score;

        currentUISkin.GetEmoticon(UI_Skin.EndScreenEmotion.Normal);
		for (int i = 0; i< 3; i++)
			{
			stars_on[i].transform.localScale = Vector3.zero;
			stars_on[i].SetActive(false);
        }
    }

    public void ShowWinsScreen(bool show_star_score, int star_number)
    {
        this.gameObject.SetActive(true);

        if (show_star_score)
            StartCoroutine(Show_star_score(star_number));
    }

    IEnumerator Show_star_score(int stars_number)
    {

        if (stars_number == total_collectable_stars_in_this_stage)
            currentUISkin.GetEmoticon(UI_Skin.EndScreenEmotion.Happy);

        yield return new WaitForSeconds(delay_start_star_score_animation);

        invoke_count = 0;

        for (int i = 0; i <= total_collectable_stars_in_this_stage - 1; i++)
        {
            if (i < stars_number)
                Show_star(i);
        }


        if (show_int_score)
            StartCoroutine(my_game_uGUI.Int_score_animation(delay_star_creation * stars_number, 0));

    }

    void Show_star(int n_star)
    {
        stars_on[n_star].SetActive(true);
        Invoke("Star_sfx", delay_star_creation * n_star);
    }

    void Star_sfx()
    {
        if (invoke_count < total_collectable_stars_in_this_stage)
        {
            stars_on[invoke_count].GetComponent<Animation>().Play("star");
            if (my_game_master)
            {
                int sfx_count = invoke_count;
                if (invoke_count > my_game_master.show_big_star_sfx.Length - 1)
                    sfx_count = my_game_master.show_big_star_sfx.Length - 1;

                if (my_game_master.show_big_star_sfx.Length < sfx_count)
                    my_game_master.Gui_sfx(my_game_master.show_big_star_sfx[sfx_count]);
            }

            invoke_count++;
        }
    }




}
