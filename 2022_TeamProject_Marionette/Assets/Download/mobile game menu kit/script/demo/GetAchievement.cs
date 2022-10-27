using UnityEngine;
using System.Collections;

public class GetAchievement : MonoBehaviour {

    public int achievementId;


    void OnMouseDown ()
	{
        if (AchievementManager.THIS)
            AchievementManager.THIS.UnlockAchievement(achievementId);

    }
}
