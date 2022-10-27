using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//for convert int to bool
using TMPro;

public class AchievementManager : MonoBehaviour {

    public static AchievementManager THIS;

    [System.Serializable]
    public class Achievement
    {

        public string name;
        [TextArea()]
        public string Requirement;
        public bool RequirementIsSecret;//show it only after the archievement is unlocked
        public Sprite icon;
        public int starRevard;
        public int virtualMoneyReward;

        [HideInInspector]
        public bool unlocked;

	}

    public Sprite offIcon;
    public AudioClip unlockSfx;
    public Achievement[] achievements;

    [Space()]
    [Header("unlock animation canvas")]
    public Image icon;
    public TextMeshProUGUI myName;
    public Animator animator;

    game_master my_game_master;

    // Use this for initialization
    void Start()
    {
        THIS = this;
        my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
        animator.speed = 0;

    }

    public void PlayMe(Sprite _icon, string _name)
    {
        icon.sprite = _icon;
        myName.text = _name;
        animator.Play("ArchievementUnlocked", -1, 0f);
        animator.speed = 1;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void UnlockAchievement(int id)
    {
        if (id >= achievements.Length)
            {
            Debug.LogWarning("Wrong Archievement id");
            return;
            }

        if (achievements[id].unlocked)//you have already unlock this;
            return;

        my_game_master.Gui_sfx(unlockSfx);

        achievements[id].unlocked = true;
        PlayerPrefs.SetInt("profile_" + my_game_master.current_profile_selected.ToString() + "_Achievements_" + id, Convert.ToInt32(achievements[id].unlocked));

        //reward
        my_game_master.GetCurrentProfile().stars_total_score += achievements[id].starRevard;
        PlayerPrefs.SetInt("profile_" + my_game_master.current_profile_selected.ToString() + "_total_stars", my_game_master.GetCurrentProfile().stars_total_score);
        my_game_master.GetCurrentProfile().UpdateCurrentVirtualMoney(achievements[id].virtualMoneyReward);


        PlayMe(achievements[id].icon, achievements[id].name);
    }

    public void Load()
    {
        for (int i = 0; i < achievements.Length; i++)
            achievements[i].unlocked = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + my_game_master.current_profile_selected.ToString() + "_Achievements_" + i));
    }
}
