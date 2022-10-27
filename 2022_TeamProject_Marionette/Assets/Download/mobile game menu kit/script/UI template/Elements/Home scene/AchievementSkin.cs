using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSkin : UI_Template
{


    [SerializeField]
    protected TextMeshProUGUI mainText;
    [SerializeField]
    protected TextMeshProUGUI[] minorText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();


        if (mainText)
            UpdateText(mainText, currentUISkin.buttonText);

        for (int i = 0; i < minorText.Length; i++ )
        {
            if (minorText[i])
                UpdateText(minorText[i], currentUISkin.buttonCountText);
        }

    }


}
