using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementScreen : MonoBehaviour {

    public Transform container;
    public GameObject achievementItemObj;
    public Sprite achievementLocked;
    public Sprite achievementUnlocked;
    AchievementItem[] achievements;
    bool initiated = false;

	// Use this for initialization
	void OnEnable () {
        AchievementManager.THIS.Load();
        InitiateMe();
        UpdateMe();


    }



    void InitiateMe() {

        if (initiated)
            return;

        if (AchievementManager.THIS == null)
            return;


        achievements = new AchievementItem[AchievementManager.THIS.achievements.Length];

        for (int i = 0; i < achievements.Length; i++)
        {
            GameObject temp = Instantiate(achievementItemObj);
            temp.transform.SetParent(container,false);

            achievements[i] = temp.GetComponent<AchievementItem>();
        }

        initiated = true;
    }

    public void UpdateMe()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (AchievementManager.THIS.achievements[i].unlocked)
                achievements[i].InitiateMe(achievementUnlocked,
                                            AchievementManager.THIS.achievements[i].icon,
                                            AchievementManager.THIS.achievements[i].name,
                                            AchievementManager.THIS.achievements[i].Requirement,
                                            AchievementManager.THIS.achievements[i].starRevard,
                                            AchievementManager.THIS.achievements[i].virtualMoneyReward);
            else
                {
                if (AchievementManager.THIS.achievements[i].RequirementIsSecret)
                    achievements[i].InitiateMe(achievementLocked,
                                            AchievementManager.THIS.offIcon,
                                            AchievementManager.THIS.achievements[i].name,
                                            "???",
                                            AchievementManager.THIS.achievements[i].starRevard,
                                            AchievementManager.THIS.achievements[i].virtualMoneyReward);
                else
                    achievements[i].InitiateMe(achievementLocked,
                                            AchievementManager.THIS.offIcon,
                                            AchievementManager.THIS.achievements[i].name,
                                            AchievementManager.THIS.achievements[i].Requirement,
                                            AchievementManager.THIS.achievements[i].starRevard,
                                            AchievementManager.THIS.achievements[i].virtualMoneyReward);
                }
        }
    }
}
