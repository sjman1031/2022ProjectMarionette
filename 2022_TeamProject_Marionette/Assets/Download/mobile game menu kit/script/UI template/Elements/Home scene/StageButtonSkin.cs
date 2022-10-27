using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButtonSkin : UI_Template
{

    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected Image[] starsOff = new Image[3];
    [SerializeField]
    protected Image[] starsOn = new Image[3];
    [SerializeField]
    protected TextMeshProUGUI myStageNumberText;
    [SerializeField]
    protected TextMeshProUGUI myStarCountText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();


        //myImage.sprite = currentUISkin.stageButtonOn;
        for (int i = 0; i < 3; i++)
        {
            starsOff[i].sprite = currentUISkin.GetIcon(UI_Skin.Icon.StarOff);
            starsOn[i].sprite = currentUISkin.GetIcon(UI_Skin.Icon.StarOn);
        }

        UpdateText(myStageNumberText, currentUISkin.stageButtonNumber);


        if (myStarCountText)
            UpdateText(myStarCountText, currentUISkin.stageButtonStarCount);

    }
}
